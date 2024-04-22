using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : Player
{
    public float runSpeed, jumpForce, timeCount = 0, target, currentDir, gravity;
    private Vector2 dir;
    public Collider[] groundCheck;
    private bool onGround = false, isJumping = false, isPulling = false, isDashing = false;
    public float x, y, z, radius;
    private void Update() {
        if(!isPulling){
            float interpolationFactor = Mathf.Clamp01(timeCount / 0.5f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, target, 0), interpolationFactor);
            timeCount += Time.deltaTime;
        }   
        
    }
    private void FixedUpdate()
    {
        groundCheck = Physics.OverlapSphere(transform.position, 0.2f, 1<< 3 | 1 << 6);
        if(groundCheck.Length > 0){
            onGround = true;
            if (isJumping)
            {
                animator.SetTrigger("Land");
                isJumping = false;
            }
        } else {
            onGround = false;
        }
        if(!isDashing)
        rb.velocity = new Vector3(0, rb.velocity.y, dir.x * playerMovement.runSpeed * Time.deltaTime);

        // gravity
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
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
        if(IsOnGround() && !isJumping){
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
            // isJumping = true;
        }
    }
    public void SetJumpForce(float force){
        jumpForce = force;
    }
    // pull
    public void Pull(){
        if(box){
            FixedJoint fixedJoint = this.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = box.GetComponent<Rigidbody>();
            fixedJoint.massScale = 10;
            box.GetComponent<Rigidbody>().mass = 1.5f;
            animator.SetTrigger("InteractBox");
            isPulling = true;
        }
    }
    public void SetDashing(bool dashing){
        isDashing = dashing;
    }
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
    public bool IsOnGround(){
        return onGround;
    }
    public void setJumping(){
        isJumping = true;
    }
    public void SetBox(GameObject currentBox){
        box = currentBox;
    }
    private void OnDrawGizmos() {
        Gizmos.DrawSphere(new Vector3(transform.position.x + x, transform.position.y + y,
        transform.position.z + z), radius);
    }
    
}