using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    private AudioManager AM;
    bool found;

    private void Start() 
    {
        AM = GetComponent<AudioManager>();
        if(AM != null)
        {
            found = true;
        }
        else
        {
            Debug.LogError("AudioManager not Found!");
        }
    }

    public void LoadScene(int scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void ReproducePlaySound()
    {
        if(found)
        {
            AM.Play("Play");
        }
    }

    public void ReproduceButtonASound()
    {
        if(found)
        {
            AM.Play("ButtonA");           
        }
    }

    public void ReproduceButtonBSound()
    {
        if(found)
        {
            AM.Play("ButtonB");           
        }
    }

    public void ReproduceScrollSound()
    {
        if(found)
        {
            AM.Play("SwipePantalla");           
        }
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void DiscardChanges()
    {
        AM.ChangeVolumeAFX(1);
        AM.ChangeVolumeAFX(1);
    }
    
}