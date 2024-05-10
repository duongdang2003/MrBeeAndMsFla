using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlubBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") ||
        other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            Destroy(gameObject);
    }
}
