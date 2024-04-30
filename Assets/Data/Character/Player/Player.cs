using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
public class Player : MonoBehaviourPunCallbacks
{
    // component
    public Rigidbody rb;
    public Animator animator;

    // script
    public PlayerInput playerInput;
    public PlayerMovement playerMovement;
    public PlayerSkillController playerSkillController;
    public PlayerUI playerUI;
    public PlayerInteract playerInteract;
    public PlayerPunCallBack playerPunCallBack;
    // object
    public GameObject box;
    public GameObject[] playerWithTag;
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
