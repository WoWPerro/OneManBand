using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateTimer : MonoBehaviour
{
    public float seconds;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeactivateWithTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DeactivateWithTimer()
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
}
