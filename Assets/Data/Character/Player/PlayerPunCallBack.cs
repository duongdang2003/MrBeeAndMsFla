using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerPunCallBack :Player
{
    [PunRPC]
    public void JumpOther(float jumpForce)
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    [PunRPC]
    public void AnimatorSetTriggerByName(string type)
    {
        animator.SetTrigger(type);

    }
    [PunRPC]
    public void PlayerSetDir(Vector2 direction)
    {
        playerMovement.SetDir(direction);
    }
    [PunRPC]
    public void AnimatorRunOther(bool check)
    {
        animator.SetBool("Run", check);
    }
    [PunRPC]
    public void MoveVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }
    [PunRPC]
    public void AnimatorSetBoolByName(string type,bool check)
    {
        animator.SetBool(type, check);
    }
    [PunRPC]
    public void JumpSkillOther(float heigh)
    {
        playerMovement.SetJumpForce(heigh);
    }
    [PunRPC]
    public void SetHighJumpOther(bool check)
    {
        playerSkillController.ToggleHighJump(check);
    }
    [PunRPC]
    public void UpdateSkillOther(int activeSkill)
    {
        playerUI.UpdateSkill(activeSkill);
    }
    [PunRPC]
    public void SetLowGravityOther(bool check)
    {
        playerSkillController.ToggleLowGravity(check);
    }
    [PunRPC]
    public void AnimatorSetFloatByName(string type, float value)
    {
        animator.SetFloat(type, value);

    }
}
