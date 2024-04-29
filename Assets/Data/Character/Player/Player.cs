using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
public class Player : MonoBehaviourPunCallbacks
{
    // component
    protected Rigidbody rb;
    protected Animator animator;

    // script
    protected PlayerInput playerInput;
    protected PlayerMovement playerMovement;
    protected PlayerSkillController playerSkillController;
    protected PlayerUI playerUI;
    protected PlayerInteract playerInteract;
    protected PlayerPunCallBack playerPunCallBack;
    // object
    protected GameObject box;
    private void Awake() {
        // rigidbody
        rb = GetComponent<Rigidbody>();
        //animator
        animator = GetComponent<Animator>();
        //script
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerSkillController = GetComponent<PlayerSkillController>();
        playerUI = GetComponent<PlayerUI>();
        playerInteract = GetComponent<PlayerInteract>();
        playerPunCallBack = GetComponent<PlayerPunCallBack>();
    }
}
