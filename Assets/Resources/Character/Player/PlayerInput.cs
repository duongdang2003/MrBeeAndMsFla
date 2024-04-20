using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : Player
{
    public void Move(InputAction.CallbackContext context){
        if(context.performed){
            playerMovement.SetDir(context.ReadValue<Vector2>());
            animator.SetBool("Run", true);
        } else if(context.canceled){
            playerMovement.SetDir(Vector2.zero);
            animator.SetBool("Run", false);
        }
    }
    public void Jump(InputAction.CallbackContext context){
        if(context.performed){
            playerMovement.Jump();
        }
    }
    public void Pull(InputAction.CallbackContext context){
        if(context.performed){
            playerMovement.Pull();
        } else if(context.canceled){
            playerMovement.Disengage();
        }
    }
    public void Dash(InputAction.CallbackContext context){
        if(context.performed){
            playerSkillController.Dash();
        }
    }
    public void HighJump(InputAction.CallbackContext context){
        if(context.performed){
            playerSkillController.HighJump();
        }
    }
    public void LowGravity(InputAction.CallbackContext context){
        if(context.performed){
            playerSkillController.LowGravity();
        }
    }
}
