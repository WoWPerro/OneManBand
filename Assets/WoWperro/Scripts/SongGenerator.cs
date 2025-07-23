using System.Collections.Generic;
using UnityEngine;

// Dentro de SongGenerator.cs
[System.Serializable]
public class InstrumentLane
{
    public string songDataFileName;
    public GameObject notePrefab;
    public Transform startPosition;
    public Transform targetPosition;
    
    // Novedad: Nombre de la pista de audio para este carril
    public string audioTrackName;

    [HideInInspector] public List<Note> notes;
    [HideInInspector] public int noteIndex = 0;
}


public class SongGenerator : MonoBehaviour
{
    [Header("Configuración de la Canción")]
    public List<InstrumentLane> instrumentLanes = new List<InstrumentLane>();
    public float songDuration; // Duración total de la canción en segundos

    [Header("Pool de Objetos")]
    [Tooltip("Cuántas notas de cada tipo crear al inicio para reutilizar.")]
    public int poolSize = 15;

    // Novedad: El pool de objetos para no estar creando y destruyendo notas constantemente.
    private Dictionary<GameObject, List<NoteObject>> notePools = new Dictionary<GameObject, List<NoteObject>>();
    private float songStartTime;
    private bool songIsPlaying = false;

    void Start()
    {
        PrepareSong();
    }

    void Update()
    {
        if (!songIsPlaying) return;

        float currentSongTime = Time.time - songStartTime;

        // Recorremos cada carril de instrumento para ver si debemos lanzar una nota
        foreach (var lane in instrumentLanes)
        {
            // Si ya no quedan notas en este carril, lo ignoramos
            if (lane.noteIndex >= lane.notes.Count) continue;

            // Comprobamos si es tiempo de activar la siguiente nota
            Note nextNote = lane.notes[lane.noteIndex];
            if (currentSongTime * 1000 >= nextNote.GetTime())
            {
                SpawnNoteFromPool(lane, nextNote);
                lane.noteIndex++;
            }
        }
    }

    /// <summary>
    /// Carga todos los datos de las canciones y crea los pools de objetos.
    /// </summary>
    void PrepareSong()
    {
        notePools.Clear();
        foreach (var lane in instrumentLanes)
        {
            // Cargar los datos de la canción usando nuestro lector estático
            lane.notes = FileReader.ReadNotesFromCSV(lane.songDataFileName);
            lane.noteIndex = 0;

            // Crear el pool de objetos para este carril
            if (!notePools.ContainsKey(lane.notePrefab))
            {
                notePools[lane.notePrefab] = new List<NoteObject>();
                for (int i = 0; i < poolSize; i++)
                {
                    GameObject obj = Instantiate(lane.notePrefab, Vector3.zero, Quaternion.identity);
                    obj.SetActive(false);
                    notePools[lane.notePrefab].Add(obj.GetComponent<NoteObject>());
                }
            }
        }
    }

    /// <summary>
    /// Inicia la reproducción de la canción.
    /// </summary>
    public void PlaySong()
    {
        songStartTime = Time.time;
        songIsPlaying = true;
    }

    /// <summary>
    /// Busca una nota inactiva en el pool y la activa.
    /// </summary>
    private void SpawnNoteFromPool(InstrumentLane lane, Note noteData)
    {
        foreach (var noteObject in notePools[lane.notePrefab])
        {
            if (!noteObject.gameObject.activeInHierarchy)
            {
                noteObject.Activate(noteData, lane.startPosition, lane.targetPosition);
                return;
            }
        }

        // Opcional: Si el pool se queda corto, puedes crear una nueva nota.
        Debug.LogWarning($"[SongGenerator] El pool para el prefab '{lane.notePrefab.name}' se quedó corto. Considera aumentar el poolSize.");
    }
}

