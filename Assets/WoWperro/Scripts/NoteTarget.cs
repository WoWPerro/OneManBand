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

    // Se activa cuando un objeto con Rigidbody entra en el trigger
    void OnTriggerEnter(Collider other)
    {
        NoteObject note = other.GetComponent<NoteObject>();
        if (note != null && !hittableNotes.Contains(note))
        {
            hittableNotes.Add(note);
        }
    }

    // Se activa cuando un objeto sale del trigger
    void OnTriggerExit(Collider other)
    {
        NoteObject note = other.GetComponent<NoteObject>();
        if (note != null)
        {
            hittableNotes.Remove(note);
        }
    }

    // Método para que el InputManager le pida que "toque" la nota más cercana/antigua
    public void HitNote()
    {
        if (hittableNotes.Count > 0)
        {
            // Llama a la primera nota en la lista (la que llegó primero)
            hittableNotes[0].CheckHit();
            // La removemos para no volver a pegarle
            hittableNotes.RemoveAt(0);
        }
    }
}
