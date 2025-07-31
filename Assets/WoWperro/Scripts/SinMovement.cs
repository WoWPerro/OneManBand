using UnityEngine;

public class SinMovement : MonoBehaviour
{
    private Vector3 start;
    private float elapsedtime = 0;

    public float intensity = 5;
    public float speed = 5;
    public float desfase = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        start = transform.position;
        elapsedtime += desfase + 1;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedtime += Time.deltaTime;
        Debug.Log(elapsedtime);
        transform.position = new Vector3(start.x , start.y  + (Mathf.Sin(elapsedtime* speed)* intensity), start.z );
    }
}
