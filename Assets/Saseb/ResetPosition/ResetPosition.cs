using UnityEngine;


public class ResetPosition : MonoBehaviour
{
    [SerializeField] private Transform respawnPos;
    [SerializeField] private Rigidbody frogPos;

    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") {
            frogPos.position = respawnPos.position;
        }
    }
}
