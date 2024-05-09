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
        }
        else
        {
            bee.transform.position = locationCharacterLeft;
            beeName.transform.position = locationNameLeft;
            fla.transform.position = locationCharacterRight;
            flaName.transform.position = locationNameRight;
        }
        //bee.transform.position = fla.transform.position;
        //fla.transform.position = beePos;
        ////swap text
        //Vector3 beeNamePos = beeName.position;
        //beeName.position = flaName.position;
        //flaName.position = beeNamePos;
    }
    public void SetSwapOther()
    {

    }
    public void SetBeginRoom()
    {
        //bee.transform.position = locationCharacterLeft;
        //beeName.position = locationNameLeft;
        //fla.transform.position = locationCharacterRight;
        //flaName.position = locationCharacterRight;
    }
}
