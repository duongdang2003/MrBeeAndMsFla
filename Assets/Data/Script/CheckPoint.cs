using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject vfx;
    public Light orbLight;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player")){
            vfx.SetActive(true);
            StartCoroutine(LightUp());
        }
    }
    IEnumerator LightUp(){
        if (orbLight.intensity < 0.5f){
            yield return null;
            orbLight.intensity += 0.01f;
            StartCoroutine(LightUp());
        }
    }
}
