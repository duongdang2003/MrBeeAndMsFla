using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
public class SwapButton : MonoBehaviourPunCallbacks
{
    public GameObject bee, fla;
    public TMP_Text beeName, flaName;
    private Vector3 locationCharacterLeft;
    private Vector3 locationCharacterRight;
    private Vector3 locationNameLeft;
    private Vector3 locationNameRight;
    private bool checkBeeLeft = true;
    private void Awake() {
        // bee = GameObject.FindWithTag("BeeAnchor");
        // fla = GameObject.FindWithTag("FlaAnchor");
        locationCharacterLeft = bee.transform.position;
        locationCharacterRight = fla.transform.position;
        locationNameLeft = beeName.transform.position;
        locationNameRight = flaName.transform.position;
    }
    public void Swap(){
        // swap model
        //Vector3 beePos = bee.transform.position;
        if (locationCharacterLeft == bee.transform.position)
        {
            bee.transform.position = locationCharacterRight;
            beeName.transform.position = locationNameRight;
            fla.transform.position = locationCharacterLeft;
            flaName.transform.position = locationNameLeft;
            checkBeeLeft = false;
        }
        else
        {
            bee.transform.position = locationCharacterLeft;
            beeName.transform.position = locationNameLeft;
            fla.transform.position = locationCharacterRight;
            flaName.transform.position = locationNameRight;
            checkBeeLeft = true;
        }
        //bee.transform.position = fla.transform.position;
        //fla.transform.position = beePos;
        ////swap text
        //Vector3 beeNamePos = beeName.position;
        //beeName.position = flaName.position;
        //flaName.position = beeNamePos;
        photonView.RPC("SetSwapOther", RpcTarget.Others);
    }
    [PunRPC]
    public void SetSwapOther()
    {
        if (checkBeeLeft)
        {
            bee.transform.position = locationCharacterRight;
            beeName.transform.position = locationNameRight;
            fla.transform.position = locationCharacterLeft;
            flaName.transform.position = locationNameLeft;
            checkBeeLeft = false;
        }
        else
        {
            bee.transform.position = locationCharacterLeft;
            beeName.transform.position = locationNameLeft;
            fla.transform.position = locationCharacterRight;
            flaName.transform.position = locationNameRight;
            checkBeeLeft = true;
        }
    }
    [PunRPC]
    public void SetFirstSwap()
    {
        Debug.Log("Ngug vcl dsamdams");
        if (!checkBeeLeft)
        {
            bee.transform.position = locationCharacterRight;
            beeName.transform.position = locationNameRight;
            fla.transform.position = locationCharacterLeft;
            flaName.transform.position = locationNameLeft;
        }
        else
        {
            bee.transform.position = locationCharacterLeft;
            beeName.transform.position = locationNameLeft;
            fla.transform.position = locationCharacterRight;
            flaName.transform.position = locationNameRight;
        }
    }
    public void SetBeginRoom()
    {
        //bee.transform.position = locationCharacterLeft;
        //beeName.position = locationNameLeft;
        //fla.transform.position = locationCharacterRight;
        //flaName.position = locationCharacterRight;
    }
    public void SetDefaultLocation()
    {
        bee.transform.position = locationCharacterLeft;
        beeName.transform.position = locationNameLeft;
        fla.transform.position = locationCharacterRight;
        flaName.transform.position = locationNameRight;
        checkBeeLeft = true;
    }
}
