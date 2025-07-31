using UnityEngine;

public class CharacterController : MonoBehaviour {

    [SerializeField] GameObject frogModel;
    [SerializeField] Animator animator;
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool readyToJump;
    public KeyCode jumpKey = KeyCode.Space;
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded;
    public Transform orientation;
    private float horizontalInput;
    private float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    private NoteTarget currentNoteTarget;
    
    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    private void Update() {
        grounded = Physics.CheckSphere(orientation.position, 0.1f, whatIsGround);
        MyInput();
        SpeedControl();
        if (grounded == true) {
            rb.linearDamping = groundDrag;
        } else {
            rb.linearDamping = 0;
            
        }
        // Check jump animation
        if (readyToJump && grounded == true)
        {
            animator.SetBool("isJumping", false);
        } else {
            animator.SetBool("isJumping", true);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Al entrar a un trigger, intentamos obtener el componente NoteTarget
        NoteTarget target = other.GetComponent<NoteTarget>();
        if (target != null)
        {
            // Si lo encontramos, lo guardamos como nuestra zona actual
            currentNoteTarget = target;
            Debug.Log("Personaje entró a la zona: " + other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Al salir, comprobamos si estamos saliendo de la zona que teníamos guardada
        if (currentNoteTarget != null && other.gameObject == currentNoteTarget.gameObject)
        {
            // Si es así, limpiamos la referencia
            currentNoteTarget = null;
            Debug.Log("Personaje salió de la zona: " + other.name);
        }
    }

    private void MyInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(jumpKey) && readyToJump && grounded == true) {
            animator.SetBool("isJumping", true);
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Si el personaje está dentro de una zona de notas...
            if (currentNoteTarget != null)
            {
                // ...le decimos a esa zona que intente validar el acierto.
                currentNoteTarget.HitNote();
            }
        }
    }

    private void MovePlayer() {
        // Calc movement dir
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        // rotate player and animate it
        if (moveDirection != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            frogModel.transform.rotation = Quaternion.Slerp(frogModel.transform.rotation, targetRotation, 10 * Time.deltaTime);
        }
        if (grounded == true) {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded) {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl() {
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVelocity.magnitude > moveSpeed) {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
        }
    }

    private void Jump() {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump() {
        readyToJump = true;
    }
    
    private void OnDrawGizmosSelected() {
    if (orientation != null) {
        Gizmos.color = grounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(orientation.position, 0.1f);
    }
    
    
}
}