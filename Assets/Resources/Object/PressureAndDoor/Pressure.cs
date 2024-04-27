using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure : MonoBehaviour
{
    public GameObject door;
    public float step, maxY, objCount = 0;
    public bool isPressed = false, isLowerPlate = false;
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

    private void OnTriggerEnter(Collider other) {
        int objLayer = other.gameObject.layer;
        if(objLayer == LayerMask.NameToLayer("Box") || objLayer == LayerMask.NameToLayer("Player")){
            objCount++;
            isPressed = true;
            if(!isLowerPlate){
                transform.position -= new Vector3(0, 0.01f, 0);
                isLowerPlate = true;
            }
        }
        
    }
    private void OnTriggerExit(Collider other) {
        int objLayer = other.gameObject.layer;
        if(objLayer == LayerMask.NameToLayer("Box") || objLayer == LayerMask.NameToLayer("Player")){
            objCount--;
        }
        if(objCount == 0){
            isPressed = false;
            transform.position += new Vector3(0, 0.01f, 0);
            isLowerPlate = false;
        }
        
    }
}
