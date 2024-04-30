using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : Player
{
    private bool isListeningInput = true;
    public GameObject[] playerWithTag;
    private void Start() {
        playerWithTag = GameObject.FindGameObjectsWithTag("Player");
    }
    public void SetListeningInput(bool listeningInput){
        isListeningInput = listeningInput;
    }
    public void Move(InputAction.CallbackContext context){
        if (photonView.IsMine)
        {
            if (context.performed && isListeningInput)
            {
                playerMovement.SetDir(context.ReadValue<Vector2>());
                playerPunCallBack.photonView.RPC("PlayerSetDir", Photon.Pun.RpcTarget.Others, context.ReadValue<Vector2>());
                playerPunCallBack.photonView.RPC("AnimatorSetBoolByName", Photon.Pun.RpcTarget.Others,"Run", true);
                animator.SetBool("Run", true);
            }
            else if (context.canceled)
            {
                playerMovement.SetDir(Vector2.zero);
                playerPunCallBack.photonView.RPC("PlayerSetDir", Photon.Pun.RpcTarget.Others, Vector2.zero);
                playerPunCallBack.photonView.RPC("AnimatorSetBoolByName", Photon.Pun.RpcTarget.Others,"Run", false);
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
                //playerPunCallBack.photonView.RPC("HighJumpPressOther", Photon.Pun.RpcTarget.Others);
                for(int i = 0; i < playerWithTag.Length; i++)
                {
                    Player other = playerWithTag[i].GetComponent<Player>();
                    if (!other.photonView.IsMine)
                    {
                        playerWithTag[i].GetComponent<Player>().playerPunCallBack.photonView.RPC("HighJumpPressOther", Photon.Pun.RpcTarget.Others);
                    }
                }
            }
        }
    }
    public void LowGravity(InputAction.CallbackContext context){
        if (photonView.IsMine)
        {
            if (context.performed && isListeningInput)
            {
                playerSkillController.LowGravity();
                //playerPunCallBack.photonView.RPC("LowGravityPressOther", Photon.Pun.RpcTarget.Others);
                for (int i = 0; i < playerWithTag.Length; i++)
                {
                    Player other = playerWithTag[i].GetComponent<Player>();
                    if (!other.photonView.IsMine)
                    {
                        playerWithTag[i].GetComponent<Player>().playerPunCallBack.photonView.RPC("LowGravityPressOther", Photon.Pun.RpcTarget.Others);
                    }
                }
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
