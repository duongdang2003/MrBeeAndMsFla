using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glub : Enemy
{
    public GameObject startPoint, endPoint;
    private int direction = 0;
    private float target, timeCount;
    void Start()
    {
        transform.position = startPoint.transform.position;
        
    }

        void Update()
    {
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
    }
    private void FixedUpdate() {
        if(!isDeath)
        rb.velocity = Vector3.forward * direction;
    }
    public override void Death(){
        base.Death();
        isDeath = true;
    }
    
}