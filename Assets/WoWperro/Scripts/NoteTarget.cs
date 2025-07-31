using System.Collections.Generic;
using UnityEngine;

// Requiere un Collider 3D configurado como Trigger
[RequireComponent(typeof(Collider))]
public class NoteTarget : MonoBehaviour
{
    // Lista de notas que están actualmente en la zona de trigger
    private List<NoteObject> hittableNotes = new List<NoteObject>();

    void Awake()
    {
        // Asegurarse de que el collider es un trigger
        GetComponent<Collider>().isTrigger = true;
    }
    
    void OnTriggerEnter(Collider other)
    {
        NoteObject note = other.GetComponent<NoteObject>();
        if (note != null && !hittableNotes.Contains(note))
        {
            hittableNotes.Add(note);
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        NoteObject note = other.GetComponent<NoteObject>();
        if (note != null)
        {
            hittableNotes.Remove(note);
        }
    }

    public void HitNote()
    {
        if (hittableNotes.Count > 0)
        {
            hittableNotes[0].CheckHit();
            hittableNotes.RemoveAt(0);
        }
    }
}
