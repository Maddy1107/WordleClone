using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [HideInInspector]
    public AudioSource source;
    public bool loop;
    [Range(0f, 1f)]
    public float volume;
    public bool playAwake;
}