using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObscureTexture : MonoBehaviour
{
    private SpriteRenderer tex;

    public float duration;
    public Color startColor;
    public Color endColor;
    public bool DeactivateOnFinish;
    public bool returnToOriginalColor;

    // Start is called before the first frame update
    void Start()
    {
        tex = this.GetComponent<SpriteRenderer>();
        StartCoroutine(Darken());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Darken()
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            tex.color = Color.Lerp(startColor, endColor, t/duration);
            yield return null;
        }
        if(returnToOriginalColor)
        {
            StartCoroutine(Clear());
        }
        else if(DeactivateOnFinish)
        {
            this.gameObject.SetActive(false);
        }
    }

    public IEnumerator Clear()
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            tex.color = Color.Lerp(endColor, startColor, t/duration);
            yield return null;
        }
        if(DeactivateOnFinish)
        {
            this.gameObject.SetActive(false);
        }
    }
}
