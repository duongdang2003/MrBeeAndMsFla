using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : Player
{
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Slime")) {
            other.gameObject.transform.parent.GetComponent<Slime>().Death();
        } else if(other.gameObject.CompareTag("Glub")) {
            Debug.Log("Glub");
            other.gameObject.transform.parent.GetComponent<Glub>().Death();
        }
        if(other.gameObject.CompareTag("DoorDeadZone") ||
           other.gameObject.CompareTag("Spike")){
            
            // reload scene here

        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("NoteContent")&& photonView.IsMine) {
            playerUI.DisplayInterractButton();
            playerInteract.SetCurrentObject(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("NoteContent")&&photonView.IsMine) {
            playerUI.HideInteractButton();
        }
    }
}
