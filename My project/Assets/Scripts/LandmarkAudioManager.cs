using UnityEngine;

public class LandmarkAudioManager : MonoBehaviour
{
    public enum SoundType { Water, Music, Urban }
    public SoundType soundType;
    public bool isCongruent = true;

    private AudioSource audioSource;
    public AudioClip waterSound;
    public AudioClip musicSound;
    public AudioClip urbanSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetAudio();
    }

    void SetAudio()
    {
        switch (soundType)
        {
            case SoundType.Water:
                audioSource.clip = waterSound;
                break;
            case SoundType.Music:
                audioSource.clip = musicSound;
                break;
            case SoundType.Urban:
                audioSource.clip = urbanSound;
                break;
        }
        audioSource.loop = true;
        audioSource.Play();
    }
}
