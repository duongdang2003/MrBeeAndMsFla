using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    // private void OnCollisionEnter(Collision other) {
    //     if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
    //         Debug.Log("Enter");
    //         other.gameObject.GetComponent<Animator>().SetTrigger("InteractBox");
    //     }
    // }
    // private void OnCollisionExit(Collision other) {
    //     if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
    //         other.gameObject.GetComponent<Animator>().SetTrigger("Disengage");
    //         Debug.Log("Leave");

    //     }
    // }
    private void OnTriggerEnter(Collider other) {
        GameObject player = other.gameObject;
        if (player.layer == LayerMask.NameToLayer("Player")){
            Debug.Log("Enter");
            player.GetComponent<PlayerMovement>().SetBox(gameObject);
            // player.GetComponent<Animator>().SetTrigger("InteractBox");
            // FixedJoint fixedJoint = player.AddComponent<FixedJoint>();
            // fixedJoint.massScale = 10;
            // fixedJoint.connectedBody = this.GetComponent<Rigidbody>();
        }
    }
    private void OnTriggerExit(Collider other) {
        GameObject player = other.gameObject;
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            Debug.Log("Leave");
            // other.gameObject.GetComponent<Animator>().SetTrigger("Disengage");
            player.GetComponent<PlayerMovement>().SetBox(null);
            StartCoroutine(ClearTrigger(other.gameObject.GetComponent<Animator>()));
        }
    }
    IEnumerator ClearTrigger(Animator animator){
        yield return null;
        animator.ResetTrigger("Disengage");
    }
}
