using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSound : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    public void PlayKeyBoardSound(){
        audioSource.PlayOneShot(audioClips[Random.Range(0, 5)]);
    }
}
