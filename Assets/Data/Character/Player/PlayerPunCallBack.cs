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
        playerSkillController.SetBoolHighJump(false);
    }
    [PunRPC]
    public void SetHighJumpOther()
    {
        playerSkillController.ToggleHighJump();
    }
    [PunRPC]
    public void UpdateSkillOther(int activeSkill)
    {
        playerUI.UpdateSkill(activeSkill);
    }
    [PunRPC]
    public void SetLowGravityOther()
    {
        playerSkillController.ToggleLowGravity();
    }
    [PunRPC]
    public void AnimatorSetFloatByName(string type, float value)
    {
        animator.SetFloat(type, value);
    }
    [PunRPC]
    public void ResetJumpOther()
    {
        playerSkillController.ResetJump();
    }
    [PunRPC]
    public void DisableDashOther()
    {
        playerUI.DisableDash();
    }
    [PunRPC]
    public void DashSkillOther()
    {
        playerSkillController.Dash();
    }
    [PunRPC]
    public void PullOther()
    {
        playerMovement.Pull();
    }
    [PunRPC]
    public void DisengageOther()
    {
        playerMovement.Disengage();
    }
}
