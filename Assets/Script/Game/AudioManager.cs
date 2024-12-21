using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource; //GLOBAL AUDIO SOURCE

    public AudioSource bgmAudioSource;
    public AudioClip bgmMusic;
    [SerializeField] public AudioClip[] footstepClips;
    [SerializeField] public AudioClip dashSoundEffect;

    public AudioClip pickupObjectSFX;
    public AudioClip putdownObjectSFX;

    public AudioClip panCookingSFX;
    public AudioClip boilingCookingSFX;
    void Start() {
        audioSource = GetComponent<AudioSource>();

        if (bgmMusic && bgmAudioSource) {
            bgmAudioSource.clip = bgmMusic;
            bgmAudioSource.loop = true;
            bgmAudioSource.playOnAwake = true;
            bgmAudioSource.volume = 0.5f;
            bgmAudioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudioOnce(AudioClip soundToPlay, float volume = 1.0f, AudioSource customAudioSource = null) {
        if (customAudioSource == null) customAudioSource = audioSource;
        customAudioSource.PlayOneShot(soundToPlay, volume);
        
    }
}
