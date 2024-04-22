using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure : MonoBehaviour
{
    public GameObject door;
    public float step, maxY;
    public bool isPressed = false, lowerPlate = false;
    private float originY;
    private Rigidbody doorRb;
    private void Awake() {
        originY = door.transform.position.y;
        doorRb = door.GetComponent<Rigidbody>();
    }
    private void FixedUpdate() {
        
        if(isPressed && door.transform.position.y < originY + maxY){
            doorRb.velocity = new Vector3(0, step, 0);
        }  else if(!isPressed && door.transform.position.y > originY){
            doorRb.velocity = new Vector3(0, -step, 0);
        } else {
            doorRb.velocity = Vector3.zero;
        }

    }
    private void OnCollisionEnter(Collision other) {
        isPressed = true;
        transform.position -= new Vector3(0, 0.01f, 0);
    }
    private void OnCollisionExit(Collision other) {
        isPressed = false;
        transform.position += new Vector3(0, 0.01f, 0);
    }
 
}
