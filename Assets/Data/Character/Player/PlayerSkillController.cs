using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : Player
{
    private bool isDashing = false, canDash = true, isLowGravity = false, isHighJump = false;
    public float dashForce, dashTime, dashDir;
    public float gravityTest;
    private void FixedUpdate() {
        if(isDashing)
        {
            rb.velocity = new Vector3(0, 0, rb.velocity.z + playerMovement.GetCurrentDir() * dashForce);
            playerPunCallBack.photonView.RPC("MoveVelocity", Photon.Pun.RpcTarget.Others, new Vector3(0, 0, rb.velocity.z + playerMovement.GetCurrentDir() * dashForce));
        } else if(isLowGravity && rb.velocity.y < 0){
            rb.velocity = new Vector3(0, gravityTest, rb.velocity.z);
            playerPunCallBack.photonView.RPC("MoveVelocity", Photon.Pun.RpcTarget.Others, new Vector3(0, gravityTest, rb.velocity.z));
        }
    }
    // dash
    public void Dash(){
        if(canDash){
            dashDir = transform.localScale.x > 0 ? 1 : -1;
            isDashing = true;
            playerMovement.SetDashing(true);
            StartCoroutine(StopDashing());
            // StartCoroutine(CoolDownDashing());
            canDash = false;
            animator.SetTrigger("Dash");
            playerMovement.photonView.RPC("AnimatorSetTriggerByName", Photon.Pun.RpcTarget.Others, "Dash");
            playerUI.DisableDash();
            playerPunCallBack.photonView.RPC("DashSkillOther", Photon.Pun.RpcTarget.Others);
        }
    }
    // high jump
    public void HighJump()
    {
        if (playerMovement.IsOnGround() && !playerMovement.IsCasting())
        {
            animator.SetFloat("OrbNum", 0);
            playerPunCallBack.photonView.RPC("AnimatorSetFloatByName", Photon.Pun.RpcTarget.Others, "OrbNum", 0f);
            animator.SetTrigger("UseOrb");
            playerMovement.photonView.RPC("AnimatorSetTriggerByName", Photon.Pun.RpcTarget.Others, "UseOrb");
        }
    }
    public void SetHighJump(){
        //isHighJump = !isHighJump;
        ToggleHighJump();
        //playerPunCallBack.photonView.RPC("SetHighJumpOther", Photon.Pun.RpcTarget.Others);
        if(isHighJump){
            playerMovement.SetJumpForce(40);
            //playerPunCallBack.photonView.RPC("JumpSkillOther", Photon.Pun.RpcTarget.Others, 40f);
            isLowGravity = false;
            playerUI.UpdateSkill(1);
            //playerPunCallBack.photonView.RPC("UpdateSkillOther", Photon.Pun.RpcTarget.Others, 1);
        } else {
            playerMovement.SetJumpForce(32);
            //playerPunCallBack.photonView.RPC("JumpSkillOther", Photon.Pun.RpcTarget.Others, 32f);
            playerUI.UpdateSkill(0);
            //playerPunCallBack.photonView.RPC("UpdateSkillOther", Photon.Pun.RpcTarget.Others, 0);
        }
    }
    public void SetBoolHighJump(bool check)
    {
        isLowGravity = check;
    }
    public void ToggleHighJump()
    {
        isHighJump = !isHighJump;
    }
    //low gravity
    public void LowGravity(){
        if (playerMovement.IsOnGround() && !playerMovement.IsCasting())
        {
            animator.SetFloat("OrbNum", 1);
            playerPunCallBack.photonView.RPC("AnimatorSetFloatByName", Photon.Pun.RpcTarget.Others, "OrbNum", 1f);
            animator.SetTrigger("UseOrb");
            playerPunCallBack.photonView.RPC("AnimatorSetTriggerByName", Photon.Pun.RpcTarget.Others, "UseOrb");
        }
    }
    public void SetLowGravity(){
        isLowGravity = !isLowGravity;
        if (isLowGravity)
        {
            ResetJump();
            playerUI.UpdateSkill(2);
        }
        else
        {
            playerUI.UpdateSkill(0);
        }
    }
    public void ToggleLowGravity()
    {
        isLowGravity = !isLowGravity;
    }
    IEnumerator StopDashing(){
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        playerMovement.SetDashing(false);
    }
    public void ActiveDashing(){
        canDash = true;
    }
    public void ResetJump(){
        isHighJump = false;
        playerMovement.SetJumpForce(32);
    }
}
