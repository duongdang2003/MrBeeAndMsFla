using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSound : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioClip typing;
    public AudioSource audioSource;
    public void PlayKeyBoardSound(){
        audioSource.Stop();
        audioSource.PlayOneShot(audioClips[Random.Range(0, 5)]);
    }
    public void PlayTypingSound(){
        audioSource.Stop();
        audioSource.PlayOneShot(typing);
    }
    public void StopPlaySound(){
        audioSource.Stop();
    }
}
