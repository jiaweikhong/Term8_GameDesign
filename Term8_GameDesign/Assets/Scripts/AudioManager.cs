using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip titleScreen;
    public AudioClip characterSelection;
    public AudioClip battle;
    public AudioClip matchConclude;

    void Start()
    {
        audioSource.loop = true;
        audioSource.clip = titleScreen;
        audioSource.Play();
    }

    public void ChangeTrack(string track)
    {
        if (track == "characterSelect")
        {
            if (audioSource.clip == characterSelection)
            {
                return;
            }
            audioSource.Stop();
            audioSource.loop = true;
            audioSource.clip = characterSelection;
            audioSource.Play();
        }
        if (track == "toGamePlay")
        {
            audioSource.Stop();
            audioSource.loop = true;
            audioSource.clip = battle;
            audioSource.Play();
        }
        if (track == "toAfterMatch")
        {
            audioSource.Stop();
            audioSource.loop = true;
            audioSource.clip = matchConclude;
            audioSource.Play();
        }
        if (track == "toTitle")
        {
            if (audioSource.clip == titleScreen)
            {
                return;
            }
            audioSource.Stop();
            audioSource.loop = true;
            audioSource.clip = titleScreen;
            audioSource.Play();
        }
    }
}
