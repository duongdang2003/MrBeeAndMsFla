using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip dash, highJump, lowGravity;
    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayDash(){
        audioSource.PlayOneShot(dash);
    }
    public void PlayHighJump(){
        audioSource.PlayOneShot(highJump);
    }
    public void PlayLowGravity(){
        audioSource.PlayOneShot(lowGravity);
    }
}
