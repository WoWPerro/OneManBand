using UnityEngine;

public class ResetPos : MonoBehaviour
{
    
    [SerializeField] private Rigidbody frogPos;
    private Vector3 respawnPos;
    void Start()
    {
        respawnPos = frogPos.transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            frogPos.linearVelocity = Vector3.zero;
            frogPos.position = respawnPos;
        }
    }
}
