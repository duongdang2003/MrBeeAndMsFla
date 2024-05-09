using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpwanPlayer : MonoBehaviour
{
    public static SpwanPlayer instance;
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] GameObject playerPrefab1;
    [SerializeField] GameObject playerPrefab2;
    [SerializeField] Transform StartPosition;

    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            SpawnPlayer();
        }
    }
    public void SpawnPlayer()
    {
        string playerName = PhotonNetwork.LocalPlayer.NickName;
        GameObject player;

        player = PhotonNetwork.IsMasterClient ? 
        PhotonNetwork.Instantiate(this.playerPrefab1.name, StartPosition.position, StartPosition.rotation) :
        PhotonNetwork.Instantiate(this.playerPrefab2.name, StartPosition.position, StartPosition.rotation);

    }
}
