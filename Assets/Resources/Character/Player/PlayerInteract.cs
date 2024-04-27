using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : Player
{
    public GameObject currentObject;
    private bool isInteracting = false;
    public void SetInteract(){
        isInteracting = !isInteracting;
    }
    public void SetCurrentObject(GameObject obj){
        currentObject = obj;
    }
    public void Interact(){
        SetInteract();
        if(currentObject != null && isInteracting){
            PauseScene();
        } else if(currentObject != null && !isInteracting){
            ContinueScene();
        }
        switch (currentObject.tag)
        {
            case "Note":
                Note();
                break;
        }
    }
    private void Note(){
        playerUI.ReadNote(isInteracting, currentObject.GetComponent<Note>().content);
    }
    public void PauseScene(){
        Time.timeScale = 0;
        playerMovement.SetInteract(true);
        playerInput.SetListeningInput(false);
    }
    public void ContinueScene(){
        Time.timeScale = 1;
        playerMovement.SetInteract(false);
        playerInput.SetListeningInput(true);
    }
}
