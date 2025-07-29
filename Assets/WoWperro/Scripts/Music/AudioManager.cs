using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    // Novedad: Patrón Singleton para acceso fácil y global.
    public static AudioManager Instance { get; private set; }

    public Sound[] sounds;
    private float volumeSongs;
    private float volumeAFX;

    private void Awake()
    {
        // Implementación del Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opcional: para que no se destruya al cambiar de escena
        }
        
        volumeSongs = 1;
        volumeAFX = 1;
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogError("Sound with name: " + name + " not be found!");
        }
        else
        {
            s.source.Play();
            if(s.soundType == SoundType.Song)
            {
                s.source.volume = volumeSongs;
            }
            else
            {
                s.source.volume = volumeAFX;
            }
        }
        
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogError("Sound with name: " + name + " not be found!");
        }
        else
        {
            s.source.Pause();
             if(s.soundType == SoundType.Song)
            {
            s.source.volume = volumeSongs;
            }
            else
            {
                s.source.volume = volumeAFX;
            }
        }

       
    }

    public void UnPause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogError("Sound with name: " + name + " not be found!");
        }
        else
        {
            s.source.UnPause();
            if(s.soundType == SoundType.Song)
            {
                s.source.volume = volumeSongs;
            }
            else
            {
                s.source.volume = volumeAFX;
            }
        }
    }

    public IEnumerator Play(string name, float Fadein, float increaseAmount)
    {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogError("Sound with name: " + name + " not be found!");
            }
            s.source.Play();
            s.source.volume = 0;
            while (s.source.volume <= Fadein)
            {
                s.source.volume += increaseAmount;
                yield return new WaitForSeconds(.1f);
            }
    }

    public void StopAll()
    {
        AudioSource[] sources;
        sources = GameObject.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach(AudioSource audioSource in sources)
        {
        	if(audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    public void StopSong(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void ChangeVolumeAFX(float _volume)
    {
        volumeAFX = _volume;
        foreach (Sound s in sounds)
        {
            if(s.soundType == SoundType.AFX)
            {
                s.source.volume = volumeAFX;
                Debug.Log(s.name);
            }
        }
    }

    public void ChangeVolumeSongs(float _volume)
    {
        volumeSongs = _volume;
        foreach (Sound s in sounds)
        {
            if(s.soundType == SoundType.Song)
            {
                s.source.volume = volumeSongs;
                Debug.Log(s.name);
            }
        }
    }

    public void ChangeVolumeInSong(float _volume, string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = _volume;
    }
    
    public float GetClipLenght(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        Debug.Log("Audio clip length : " + s.clip.length);
        return s.clip.length;
    }
    
}
