using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] Sound[] sounds;
    void Awake()
    {
        instance = this;
        foreach (Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();

            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.volume = sound.volume;
        }
    }

    public void PlaySound(int soundNumber)
    {
        if (soundNumber <= sounds.Length - 1)
        {
            sounds[soundNumber].audioSource.Play();
        }
    }
}
