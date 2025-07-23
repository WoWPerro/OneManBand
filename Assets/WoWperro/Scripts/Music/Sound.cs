using UnityEngine.Audio;
using UnityEngine;

public enum SoundType
{
    Song = 0,
    AFX = 1
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public float volume;
    public float pitch;
    public bool loop;
    public SoundType soundType;

    [HideInInspector]
    public AudioSource source;
}
