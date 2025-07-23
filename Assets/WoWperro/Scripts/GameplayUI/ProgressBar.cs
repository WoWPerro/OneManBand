using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    private float tapsToCompleteSong;
    private float taps;
    private float delay;
    private float SongDuration;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        taps = 1;
        slider = GetComponent<Slider>();
        delay = GameManager.Instance.startDelay;
        SongDuration = GameManager.Instance.gameObject.GetComponent<SongGenerator>().songDuration;
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        slider.value = timer/(SongDuration+delay);
    }

    public void GoodScore()
    {
        taps++;
        // UpdateSlider();
    }

    public void BadScore()
    {
        taps--;
        // if(taps < 1)
        // {
        //     taps = 1;
        // }
        // UpdateSlider();
    }

    private void UpdateSlider()
    {
        slider.value = taps/(tapsToCompleteSong+1);
        //Debug.Log(slider.value);
    }

    public void SetTapsToCompleteSong(int _taps)
    {
        tapsToCompleteSong = _taps;
    }
}
