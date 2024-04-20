using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // component
    protected Rigidbody rb;
    protected Animator animator;

    // script
    protected PlayerInput playerInput;
    protected PlayerMovement playerMovement;
    protected PlayerSkillController playerSkillController;

    // object
    protected GameObject box;
    private void Awake() {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerSkillController = GetComponent<PlayerSkillController>();
    }
}
