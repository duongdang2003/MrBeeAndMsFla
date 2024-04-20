using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : Player
{
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Slime")) {
            other.gameObject.transform.parent.GetComponent<Slime>().Death();
        }
    }
}
