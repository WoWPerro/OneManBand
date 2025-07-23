using UnityEngine;

public class InputManager : MonoBehaviour
{
    public LayerMask hittableLayer; // Asigna en el inspector la layer de tus NoteTargets
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        DetectInput();
    }

    private void DetectInput()
    {
        // Input para PC (Click izquierdo)
        if (Input.GetMouseButtonDown(0))
        {
            CheckRaycast(Input.mousePosition);
        }

        // Input para móviles
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                CheckRaycast(touch.position);
            }
        }
    }

    private void CheckRaycast(Vector3 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        // Lanzamos un rayo desde la cámara a través del punto presionado
        if (Physics.Raycast(ray, out hit, 100f, hittableLayer))
        {
            // Si el rayo golpea un objeto en la layer "hittable"
            NoteTarget target = hit.collider.GetComponent<NoteTarget>();
            if (target != null)
            {
                // Le decimos al objetivo que intente "golpear" una nota
                target.HitNote();
            }
        }
    }
}