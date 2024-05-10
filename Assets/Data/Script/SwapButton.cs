using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
public class SwapButton : MonoBehaviourPunCallbacks
{
    public GameObject bee, fla;
    public TMP_Text beeName, flaName;
    public GameObject locationCharacterLeft;
    public GameObject locationCharacterRight;
    public GameObject locationNameLeft;
    public GameObject locationNameRight;
    public bool checkBeeLeft = true;
    private void Awake() {
        //// bee = GameObject.FindWithTag("BeeAnchor");
        //// fla = GameObject.FindWithTag("FlaAnchor");
        //locationNameLeft = beeName.transform.position;
        //locationNameRight = flaName.transform.position;
    }
    public void Swap(){
        // swap model
        //Vector3 beePos = bee.transform.position;
        if (locationCharacterLeft.transform.position == bee.transform.position)
        {
            bee.transform.position = locationCharacterRight.transform.position;
            beeName.transform.position = locationNameRight.transform.position;
            fla.transform.position = locationCharacterLeft.transform.position;
            flaName.transform.position = locationNameLeft.transform.position;
            checkBeeLeft = false;
        }
        else
        {
            bee.transform.position = locationCharacterLeft.transform.position;
            beeName.transform.position = locationNameLeft.transform.position;
            fla.transform.position = locationCharacterRight.transform.position;
            flaName.transform.position = locationNameRight.transform.position;
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
            bee.transform.position = locationCharacterRight.transform.position;
            beeName.transform.position = locationNameRight.transform.position;
            fla.transform.position = locationCharacterLeft.transform.position;
            flaName.transform.position = locationNameLeft.transform.position;
            checkBeeLeft = false;
        }
        else
        {
            bee.transform.position = locationCharacterLeft.transform.position;
            beeName.transform.position = locationNameLeft.transform.position;
            fla.transform.position = locationCharacterRight.transform.position;
            flaName.transform.position = locationNameRight.transform.position;
            checkBeeLeft = true;
        }
    }
    [PunRPC]
    public void SetFirstSwap()
    {
        Debug.Log("Ngug vcl dsamdams");
        if (!checkBeeLeft)
        {
            bee.transform.position = locationCharacterRight.transform.position;
            beeName.transform.position = locationNameRight.transform.position;
            fla.transform.position = locationCharacterLeft.transform.position;
            flaName.transform.position = locationNameLeft.transform.position;
            checkBeeLeft = true;
        }
        else
        {
            bee.transform.position = locationCharacterLeft.transform.position;
            beeName.transform.position = locationNameLeft.transform.position;
            fla.transform.position = locationCharacterRight.transform.position;
            flaName.transform.position = locationNameRight.transform.position;
            checkBeeLeft = false;
        }
    }
    //public void SetBeginRoom()
    //{
    //    //bee.transform.position = locationCharacterLeft;
    //    //beeName.position = locationNameLeft;
    //    //fla.transform.position = locationCharacterRight;
    //    //flaName.position = locationCharacterRight;
    //}
    public void SetDefaultLocation()
    {
        bee.transform.position = locationCharacterLeft.transform.position;
        beeName.transform.position = locationNameLeft.transform.position;
        fla.transform.position = locationCharacterRight.transform.position;
        flaName.transform.position = locationNameRight.transform.position;
        checkBeeLeft = true;
    }
}
