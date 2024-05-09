using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomButton : MonoBehaviour
{
    [SerializeField] TMP_Text buttonText;
    private RoomInfo infor;
    public void SetButtonDetails(RoomInfo inputInfor)
    {
        infor = inputInfor;
        buttonText.text = infor.Name;
    }
    public string GetNameRoom()
    {
        return infor.Name;
    }
    public void OpenRoom()
    {
        Launcher.instance.JoinRoom(infor);
    }
}
