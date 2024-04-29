using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : Player
{
    private bool isDashing = false, canDash = true, isLowGravity = false, isHighJump = false;
    public float dashForce, dashTime, dashDir;
    public float gravityTest;
    private void FixedUpdate() {
        if(isDashing){
            rb.velocity = new Vector3(0, 0, rb.velocity.z + playerMovement.GetCurrentDir() * dashForce);
        } else if(isLowGravity && rb.velocity.y < 0){
            rb.velocity = new Vector3(0, gravityTest, rb.velocity.z);
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
        }
    }
    // high jump
    public void HighJump(){
        if(playerMovement.IsOnGround() && !playerMovement.IsCasting()){
            animator.SetTrigger("UseOrb");
            playerMovement.photonView.RPC("AnimatorSetTriggerByName", Photon.Pun.RpcTarget.Others, "UseOrb");
            animator.SetFloat("OrbNum", 0);
        }
        
    }
    public void SetHighJump(){
        isHighJump = !isHighJump;
        if(isHighJump){
            playerMovement.SetJumpForce(40);
            playerPunCallBack.photonView.RPC("JumpSkillOther", Photon.Pun.RpcTarget.Others, 40);
            isLowGravity = false;
            playerUI.UpdateSkill(1);
        } else {
            playerMovement.SetJumpForce(32);
            playerPunCallBack.photonView.RPC("JumpSkillOther", Photon.Pun.RpcTarget.Others, 32);
            playerUI.UpdateSkill(0);
        }
    }
    //low gravity
    public void LowGravity(){
        if(playerMovement.IsOnGround() && !playerMovement.IsCasting()){
            animator.SetTrigger("UseOrb");
            playerMovement.photonView.RPC("AnimatorSetTriggerByName", Photon.Pun.RpcTarget.Others, "UseOrb");
            animator.SetFloat("OrbNum", 1);
        }
    }
    public void SetLowGravity(){
        isLowGravity = !isLowGravity;
        if (isLowGravity)
        {
            ResetJump();
            playerUI.UpdateSkill(2);
        } else {
            playerUI.UpdateSkill(0);
        }
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
