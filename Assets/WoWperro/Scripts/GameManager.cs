using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

// Novedad: Implementamos la interfaz IGameManager que definimos en NoteObject
public class GameManager : MonoBehaviour, IGameManager
{
    public static GameManager Instance { get; private set; }

    [Header("Dependencias")]
    public SongGenerator songGenerator;
    // Novedad: Activamos la referencia al AudioManager.
    public AudioManager audioManager;

    [Header("Configuración del Juego")]
    public string mainSongName = "NombreDeTuCancionPrincipal"; // Novedad: Campo para el nombre de la canción
    public float startDelay = 3f;
    public int score = 0;

    // Novedad: Sistema de umbrales de puntuación dinámico.
    [System.Serializable]
    public class ScoreThreshold
    {
        public int scoreRequired;
        public int instrumentLaneToActivate; // Índice del carril en SongGenerator
    }
    public List<ScoreThreshold> scoreThresholds;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text countdownText;
    // public ProgressBar slider;

    // --- Implementación de la Interfaz IGameManager ---

    public float GetDelay() => startDelay;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        if (countdownText) countdownText.gameObject.SetActive(true);
        for (int i = (int)startDelay; i > 0; i--)
        {
            if (countdownText) countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        
        if (countdownText) countdownText.gameObject.SetActive(false);
        if (audioManager) audioManager.Play(mainSongName);
        if (songGenerator) songGenerator.PlaySong();
        
    }

    public void GoodScore()
    {
        score++;
        if (scoreText) scoreText.text = score.ToString();

        // Novedad: Revisa los umbrales de forma dinámica y activa el audio
        foreach (var threshold in scoreThresholds)
        {
            if (score == threshold.scoreRequired) // Usamos '==' para que se active una sola vez
            {
                int laneIndex = threshold.instrumentLaneToActivate;
                if (songGenerator && laneIndex < songGenerator.instrumentLanes.Count)
                {
                    // Obtenemos el nombre de la pista del carril correspondiente
                    string trackToPlay = songGenerator.instrumentLanes[laneIndex].audioTrackName;

                    if (!string.IsNullOrEmpty(trackToPlay))
                    {
                        Debug.Log($"Activando pista de audio: {trackToPlay}");
                        audioManager.Play(trackToPlay);
                    }
                }
            }
        }
    }

    public void BadScore()
    {
        // Decide qué hacer en un fallo. ¿Resetear score? ¿Quitar un instrumento?
        score = 0;
        if (scoreText) scoreText.text = score.ToString();
        // if (slider) slider.BadScore();
    }

    public void GenerateCorrectParticle(Vector3 pos) { /* Lógica de partículas aquí */ }
    public void GeneratePerfectParticle(Vector3 pos) { /* Lógica de partículas aquí */ }
    public void GenerateMissParticle(Vector3 pos) { /* Lógica de partículas aquí */ }
    public void GenerateHitParticle(Vector3 pos) { /* Lógica de partículas aquí */ }
}