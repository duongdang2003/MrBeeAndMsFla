using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : Player
{
    private bool isListeningInput = true;
    public void SetListeningInput(bool listeningInput){
        isListeningInput = listeningInput;
    }
    public void Move(InputAction.CallbackContext context){
        if (photonView.IsMine)
        {
            if (context.performed && isListeningInput)
            {
                playerMovement.SetDir(context.ReadValue<Vector2>());
                animator.SetBool("Run", true);
            }
            else if (context.canceled)
            {
                playerMovement.SetDir(Vector2.zero);
                animator.SetBool("Run", false);
            }
        }
    }
    public void Jump(InputAction.CallbackContext context){
        if (photonView.IsMine)
        {
            if (context.performed && isListeningInput)
            {
                playerMovement.Jump();
            }
        }
    }
    public void Pull(InputAction.CallbackContext context){
        if (photonView.IsMine)
        {
            if (context.performed && isListeningInput)
            {
                playerMovement.Pull();
            }
            else if (context.canceled)
            {
                playerMovement.Disengage();
            }
        }
    }
    public void Dash(InputAction.CallbackContext context){
        if (photonView.IsMine)
        {
            if (context.performed && isListeningInput)
            {
                playerSkillController.Dash();
            }
        }
    }
    public void HighJump(InputAction.CallbackContext context){
        if (photonView.IsMine)
        {
            if (context.performed && isListeningInput)
            {
                playerSkillController.HighJump();
            }
        }
    }
    public void LowGravity(InputAction.CallbackContext context){
        if (photonView.IsMine)
        {
            if (context.performed && isListeningInput)
            {
                playerSkillController.LowGravity();
            }
        }
    }
    public void Interact(InputAction.CallbackContext context){
        if (photonView.IsMine)
        {
            if (context.performed && playerInteract.currentObject != null)
            {
                playerInteract.Interact();
            }
        }
    }
}
