using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glub : Enemy
{
    public GameObject startPoint, endPoint, bulletSpawnPoint, bulletPrefab;
    public float speed;
    private int direction = 0;
    private float target, timeCount;
    void Start()
    {
        transform.position = startPoint.transform.position;
        
    }

        void Update()
    {
        if(!isDeath){
            Debug.DrawRay(transform.position + new Vector3(0, 0.25f, 0), transform.TransformDirection(Vector3.forward) * 30, Color.white);
            RaycastHit hit;
            if(Physics.Raycast(transform.position + new Vector3(0, 0.25f, 0), transform.TransformDirection(Vector3.forward), out hit, 30, 1 << 7)){
                animator.SetTrigger("Attack");
                speed = 0;
            } else {
                speed = 2;
            }
            if (startPoint.transform.position.z - transform.position.z >= 0)
            {
                direction = 1;
                target = 0;
                timeCount = 0;
            }
            else if (endPoint.transform.position.z - transform.position.z <= 0)
            {
                direction = -1;
                target = 180;
                timeCount = 0;
            }     
            float interpolationFactor = Mathf.Clamp01(timeCount / 2);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, target, 0), interpolationFactor);
            timeCount += Time.deltaTime;
        } else {
            speed = 0;
        }
        
    }
    private void FixedUpdate() {
        if(!isDeath)
        rb.velocity = Vector3.forward * direction * speed;
    }
    public override void Death(){
        base.Death();
        isDeath = true;
        rb.isKinematic = true;
        GetComponent<CapsuleCollider>().enabled = false;
    }
    public void FireBullet(){
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = Vector3.forward * direction * 8;
        Destroy(bullet, 5);
    }
    public void EndAttack(){
        speed = 2;
    }
}
