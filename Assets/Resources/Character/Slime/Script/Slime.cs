using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    public GameObject startPoint, endPoint;
    private int direction = 0;
    void Start()
    {
        transform.position = startPoint.transform.position;
        
    }

        void Update()
    {
        direction = startPoint.transform.position.z - endPoint.transform.position.z > 0 ? 
            direction = 1 : direction = -1;
        rb.velocity = Vector3.forward * direction;
    }
}
