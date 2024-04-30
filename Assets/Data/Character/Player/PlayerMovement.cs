using System.Collections;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : Player
{
    public float runSpeed, jumpForce, timeCount = 0, target, currentDir, gravity;
    private Vector2 dir;
    public Collider[] groundCheck;
    private bool onGround = false, isJumping = false, isPulling = false, isDashing = false, isCasting = false, isInteracting = false;
    private float pullDir;
    public float x, y, z, radius;
    public float verticalVelocity;
    private Vector3 dropVector;
    private bool wallLeft;
    private bool wallRight;
    public Transform orientation;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    public float wallCheckDistance;
    public LayerMask whatIsWall;
    private bool exitingWall;
    public float exitWallTime;
    private float exitWallTimer;
    public float wallJumpUpForce;
    public float wallJumpSideForce;
    private void Update()
    {
        if(!isInteracting){
            if(!isPulling){
            float interpolationFactor = Mathf.Clamp01(timeCount / 0.5f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, target, 0), interpolationFactor);
            timeCount += Time.deltaTime;
            } if(isPulling){
                if(!IsOnGround()){
                    Disengage();
                }
                else if (GetDir().x == 0) 
                    animator.SetFloat("BoxInteract", 0.5f);
                else if (GetDir().x != pullDir) 
                    animator.SetFloat("BoxInteract", 0);
                else
                    animator.SetFloat("BoxInteract", 1);
            }
            CheckForWall();
        }
        if (photonView.IsMine)
        {
            StartCoroutine(CallRPCAfterDelay());
        }
    }
    private IEnumerator CallRPCAfterDelay()
    {
        yield return new WaitForSeconds(5f); 
        playerPunCallBack.photonView.RPC("SetUpdatePositionOther", RpcTarget.Others, transform.position);
    }
    private void FixedUpdate()
    {
        if(!isInteracting){
            groundCheck = Physics.OverlapSphere(transform.position, 0.2f, 1<< 3 | 1 << 6);
            if(groundCheck.Length > 0){
                onGround = true;
                verticalVelocity = -1;
                if (isJumping)
                {
                    animator.SetTrigger("Land");
                    playerPunCallBack.photonView.RPC("AnimatorSetTriggerByName", RpcTarget.Others, "Land");
                    isJumping = false;
                    verticalVelocity = jumpForce;
                }
            } else {
                onGround = false;
                verticalVelocity -= gravity * Time.deltaTime;
            }
            dir.y = verticalVelocity;
            if (!isDashing && !isCasting && photonView.IsMine)
            {
                playerPunCallBack.photonView.RPC("MoveVelocity", RpcTarget.Others, new Vector3(0, rb.velocity.y, dir.x * runSpeed * Time.deltaTime));
            }
            rb.velocity = new Vector3(0, rb.velocity.y, dir.x * playerMovement.runSpeed * Time.deltaTime);
        }
    }
    public void SetDir(Vector2 direction){
        dir = direction;
        if(dir.x > 0){
            target = 0;
            currentDir = 1;
        } else if(dir.x < 0) {
            target = -180;
            currentDir = -1;
        }
        timeCount = 0;
    }
    public Vector2 GetDir(){
        return dir;
    }
    public float GetCurrentDir(){
        return currentDir;
    }
    // jump
    public void Jump(){
        if (IsOnGround() && !isJumping && !isCasting)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
            playerPunCallBack.photonView.RPC("JumpOther", RpcTarget.Others, jumpForce);
            playerPunCallBack.photonView.RPC("AnimatorSetTriggerByName", RpcTarget.Others, "Jump");
            // isJumping = true;
        }else if (!IsOnGround() && isJumping && !isCasting && (wallLeft || wallRight))
        {
            WallJump();
        }
    }
    public void SetJumpForce(float force){
        jumpForce = force;
    }
    // pull
    public void Pull(){
        if(box && IsOnGround()){
            FixedJoint fixedJoint = this.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = box.GetComponent<Rigidbody>();
            fixedJoint.massScale = 10;
            box.GetComponent<Rigidbody>().mass = 4f;
            animator.SetTrigger("InteractBox");
            isPulling = true;
            pullDir = GetCurrentDir();
        }
        playerPunCallBack.photonView.RPC("PullOther", RpcTarget.Others);
    }
    // dash
    public void SetDashing(bool dashing){
        isDashing = dashing;
    }
    // disengage
    public void Disengage(){
        if(box && GetComponent<FixedJoint>()
        ){
            GetComponent<FixedJoint>().breakTorque = 0;
            GetComponent<FixedJoint>().breakForce = 0;
            box.GetComponent<Rigidbody>().mass = 999;
            animator.SetTrigger("Disengage");
            isPulling = false;
        }

    }
    // interact
    public void SetInteract(bool toggle){
        isInteracting = toggle;
    }
    public bool IsOnGround(){
        return onGround;
    }
    public void setJumping(){
        isJumping = true;
    }
    public void SetBox(GameObject currentBox){
        box = currentBox;
    }
    public void EndDash(){
        if(!IsOnGround()){
            // animator.SetTrigger("DashToJump");
            animator.SetBool("DashToJump", true);
            setJumping();
            // StartCoroutine(ClearTrigger());
            } else {
                animator.SetTrigger("Idle");
            }
    }
    public bool IsCasting(){
        return isCasting;
    }
    public void StartCasting(){
        isCasting = true;
    }
    public void EndCasting(){
        isCasting = false;
    }
    public void RightFootSmoke(){
        ObjectPooler.Instance.SpawnFromPool("FootStepSmoke", transform.position + new Vector3(-0.1f, 0.01f, 0), Quaternion.Euler(90, 0 , 0));
    }
    public void LeftootSmoke(){
        ObjectPooler.Instance.SpawnFromPool("FootStepSmoke", transform.position + new Vector3(0.1f, 0.01f, 0), Quaternion.Euler(90, 0 , 0));
    }
    IEnumerator ClearTrigger(){
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("DashToJump", false);
        animator.ResetTrigger("Land");
        animator.ResetTrigger("Jump");

    }
    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);
    }
    private void WallJump()
    {
        // enter exiting wall state
        exitingWall = true;
        exitWallTimer = exitWallTime;

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

        Vector3 forceToApply = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;

        // reset y velocity and add force
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);
    }
    private void OnDrawGizmos() {
        // ground check
        Gizmos.DrawSphere(new Vector3(transform.position.x + x, transform.position.y + y,
        transform.position.z + z), radius);

        // box facing direct
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, orientation.right);
    }
}