using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDeadZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.layer == 7){
            Debug.Log("hit");
        }
        Debug.Log(other.gameObject.name);
    }
}
