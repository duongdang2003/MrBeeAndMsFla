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
    public void Dash(){
        if(canDash){
            dashDir = transform.localScale.x > 0 ? 1 : -1;
            isDashing = true;
            playerMovement.SetDashing(true);
            StartCoroutine(StopDashing());
            StartCoroutine(CoolDownDashing());
            canDash = false;
        }
    }
    public void HighJump(){
        ResetStats();
        isHighJump = !isHighJump;
        if(isHighJump)
            playerMovement.SetJumpForce(45);
        else
            playerMovement.SetJumpForce(32);
    }
    public void LowGravity(){
        ResetStats();
        isLowGravity = !isLowGravity;
    }
    IEnumerator StopDashing(){
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        playerMovement.SetDashing(false);
    }
    IEnumerator CoolDownDashing(){
        yield return new WaitForSeconds(2);
        canDash = true;
    }
    public void ResetStats(){
        playerMovement.SetJumpForce(32);
    }
}
