using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseobj;
    public GameObject congratulations;
    public GameObject congratsParticle;
    private bool timerFinish = false;

    public IEnumerator TimerBool()
    {
        yield return new WaitForSeconds(5f);
        timerFinish = true;
    }
    public void PauseGame()
    {
        if (timerFinish)
        {
            pauseobj.SetActive(true);
            Time.timeScale = 0;
            //this.GetComponent<AudioSource>().Pause();
            AudioManager.Instance.Pause("Song");
        }

    }

    public void Continue()
    {
        if (timerFinish)
        {
            pauseobj.SetActive(false);
            Time.timeScale = 1;
            AudioManager.Instance.UnPause("Song");
            //this.GetComponent<AudioSource>().UnPause();
        }

    }
    
    public void ContinueTutorial2()
    {
        if (timerFinish)
        {
            pauseobj.SetActive(false);
            Time.timeScale = 1;
            Time.timeScale = 1;
            AudioSource[] audioSources = this.GetComponents<AudioSource>();
            foreach(AudioSource a in audioSources)
            {
                a.UnPause();
            }
        }

    }
    

    public void PauseGameCongratularions()
    {
        if (timerFinish)
        {
            Time.timeScale = 0;
            congratulations.SetActive(true);
            congratsParticle.SetActive(true);
            AudioManager.Instance.Pause("Song");

        }


        //StartCoroutine(DeactivateCongrats(2));
    }
    
    public void PauseGameCongratularionsTutorial()
    {
        if (timerFinish)
        {
            Time.timeScale = 0;
            congratulations.SetActive(true);
            congratsParticle.SetActive(true);
            AudioManager.Instance.Pause("Song");
        }


        //StartCoroutine(DeactivateCongrats(2));
    }

    private IEnumerator DeactivateCongrats(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        ContinueCongratularions();
    }

    public void ContinueCongratularions()
    {
        if (timerFinish)
        {
            congratulations.SetActive(false);
            congratsParticle.SetActive(false);
            Time.timeScale = 1;
            AudioManager.Instance.UnPause("Song");

        }

    }

    public void PauseTutorial()
    {
        if (timerFinish)
        {
            Time.timeScale = 0;
            AudioSource[] audioSources = this.GetComponents<AudioSource>();
            foreach(AudioSource a in audioSources)
            {
                a.Pause();
            }
        }

    }
    
    public void PauseTutorialCanvas()
    {
        if (timerFinish)
        {
            pauseobj.SetActive(true);
            Time.timeScale = 0;
            AudioSource[] audioSources = this.GetComponents<AudioSource>();
            foreach(AudioSource a in audioSources)
            {
                a.Pause();
            }
        }

    }

    public void ContinueTutorial()
    {
        if (timerFinish)
        {
            Time.timeScale = 1;
            AudioSource[] audioSources = this.GetComponents<AudioSource>();
            foreach(AudioSource a in audioSources)
            {
                a.UnPause();
            }
        }
        
    }

    private void Start()
    {
        StartCoroutine(TimerBool());
    }
}
