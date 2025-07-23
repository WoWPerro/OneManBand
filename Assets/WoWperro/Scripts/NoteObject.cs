using System.Collections;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    [Header("Configuración")]
    public Transform target; // El objetivo (personaje/instrumento) al que se dirige
    public bool isTutorial = false;

    [Header("Tiempos de Acierto")]
    [Tooltip("Margen de tiempo total (en segundos) para un acierto.")]
    private float tapWindow = 0.5f; // 0.25 para cada lado
    [Tooltip("Margen de tiempo (en segundos) para un acierto 'Perfecto'.")]
    private float perfectTapWindow = 0.18f; // 0.09 para cada lado

    private Note noteData;
    private float spawnTime;
    private float journeyDuration;
    private bool isTapped = false;
    private bool wasCorrect = false;
    private bool inUse = false;

    // Referencia al GameManager o al TutorialSequence
    private IGameManager gameManager;

    void Start()
    {
            gameManager = GameManager.Instance;
    }

    public void Activate(Note note, Transform startPosition, Transform endPosition)
    {
        this.noteData = note;
        this.transform.position = startPosition.position;
        this.target = endPosition;
        this.spawnTime = Time.time;
        this.inUse = true;
        this.isTapped = false;
        this.wasCorrect = false;
        this.gameObject.SetActive(true);

        // Inicia el movimiento de la nota
        StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        float delayBeforeMoving = (noteData.GetTime() / 1000f) - noteData.GetTimeToTap();
        yield return new WaitForSeconds(delayBeforeMoving + gameManager.GetDelay());

        float timeToMove = noteData.GetTimeToTap();
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(startPos, target.position, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target.position;

        // Si la nota llega al final y no fue tocada, es un fallo
        if (!isTapped)
        {
            gameManager.GenerateMissParticle(transform.position);
            gameManager.BadScore();
            StartCoroutine(FadeOut(0.2f));
        }
    }

    // Este método será llamado desde el script de input cuando el jugador interactúe
    // Este método será llamado desde el script de input cuando el jugador interactúe
    public void CheckHit()
    {
        if (isTapped) return; 
        isTapped = true;

        float expectedHitTime = spawnTime + (noteData.GetTime() / 1000f) + gameManager.GetDelay();
        float timeDifference = Time.time - expectedHitTime;
        float absDifference = Mathf.Abs(timeDifference);

        if (absDifference <= tapWindow / 2f)
        {
            // --- ACIERTO ---
            gameManager.GoodScore();
            StartCoroutine(FadeOut(0.1f));
            
            // Novedad: Reproducir sonido de acierto
            AudioManager.Instance.Play("SonidoAcierto"); 

            if (absDifference <= perfectTapWindow / 2f)
            {
                gameManager.GeneratePerfectParticle(transform.position);
            }
            else
            {
                gameManager.GenerateHitParticle(transform.position);
            }
        }
        else
        {
            // --- FALLO ---
            gameManager.BadScore();
            StartCoroutine(FadeOut(0.2f));

            // Novedad: Reproducir sonido de fallo
            AudioManager.Instance.Play("SonidoFallo");
            
            gameManager.GenerateMissParticle(transform.position);
        }
    }

    
    
    private IEnumerator FadeOut(float duration)
    {
        // Aquí puedes poner una animación de desaparición (scaling, alpha, etc.)
        // Por ahora, solo la desactivamos.
        yield return new WaitForSeconds(duration);
        this.gameObject.SetActive(false);
        inUse = false;
    }

    public bool IsInUse() => inUse;
}

// Interfaz para unificar GameManager y TutorialSequence
public interface IGameManager
{
    void GoodScore();
    void BadScore();
    void GeneratePerfectParticle(Vector3 position);
    void GenerateHitParticle(Vector3 position);
    void GenerateMissParticle(Vector3 position);
    float GetDelay();
}