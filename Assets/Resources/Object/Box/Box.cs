using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        GameObject player = other.gameObject;
        if (player.layer == LayerMask.NameToLayer("Player")){
            player.GetComponent<PlayerMovement>().SetBox(gameObject);
        }
    }
    private void OnTriggerExit(Collider other) {
        GameObject player = other.gameObject;
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            player.GetComponent<PlayerMovement>().SetBox(null);
            StartCoroutine(ClearTrigger(other.gameObject.GetComponent<Animator>()));
        }
    }
    IEnumerator ClearTrigger(Animator animator){
        yield return null;
        animator.ResetTrigger("Disengage");
    }
}
