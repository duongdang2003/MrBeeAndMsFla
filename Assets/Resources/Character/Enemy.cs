using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody rb;
    protected Animator animator;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
}
