using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher instance;
    private void Awake()
    {
        instance = this;
    }
    [Header("Panel")]

    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject loadingPanel;
    [SerializeField] GameObject createRoomPanel;
    [SerializeField] GameObject findRoomPanel;
    [SerializeField] GameObject roomPanel;

    [Header("Text")]

    [SerializeField] TMP_InputField roomNameInput;
    [SerializeField] TMP_Text roomName, playerName;
    [SerializeField] Transform content;
    [SerializeField] Transform playerJoinRoom;
    [SerializeField] TMP_Text nickNameText;
    private string nickName;

    [Header("Button")]

    [SerializeField] GameObject startButton;
    [SerializeField] GameObject readyButton;
    [SerializeField] GameObject swapButton;
    public RoomButton roomButton;
    private List<RoomButton> allRoomButtons = new List<RoomButton>();
    private List<TMP_Text> allPlayerNames = new List<TMP_Text>();
    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();
    private List<RoomInfo> roomsActive;
    private SwapButton swap;

    [Header("CharacterModel")]
    [SerializeField] GameObject[] characterModels;
    [SerializeField] GameObject[] uiLocationCharacter;
    // [Header("CharacterName")]
    // [SerializeField] RectTransform[] characterNames;


    [Header("StarGame")]

    [SerializeField] GameObject User;
    private GameObject hasReady;
    private bool isReady = false;
    [SerializeField] string Map;
    //private List<User> users = new List<User>();
    void Start()
    {
        CloseMenu();
        loadingPanel.SetActive(true);
        PhotonNetwork.ConnectUsingSettings();
        //PhotonNetwork.JoinLobby();
        PhotonNetwork.AddCallbackTarget(this);
        nickName = GenerateRandomString();
        nickNameText.text = nickName;
        swap = swapButton.GetComponent<SwapButton>();
    }
    public static string GenerateRandomString()
    {
        System.Random random = new System.Random();
        const string characters = "123456789";
        char[] randomChars = new char[6];

        for (int i = 0; i < 6; i++)
        {
            randomChars[i] = characters[random.Next(characters.Length)];
        }

        return new string(randomChars);
    }
    void CloseMenu()
    {
        menuPanel.SetActive(false);
        loadingPanel.SetActive(false);
        roomPanel.SetActive(false);
        createRoomPanel.SetActive(false);
        roomPanel.SetActive(false);
        findRoomPanel.SetActive(false);
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby()
    {
        CloseMenu();
        menuPanel.SetActive(true);
        PhotonNetwork.NickName = nickName;
    }
    public void OpenCreateRoom()
    {
        createRoomPanel.SetActive(true);
    }
    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(roomNameInput.text))
        {
            CloseMenu();
            loadingPanel.SetActive(true);
            RoomOptions room = new RoomOptions();
            room.MaxPlayers = 2;
            PhotonNetwork.CreateRoom(roomNameInput.text, room);
            roomNameInput.text = "";
            swap.SetDefaultLocation();
        }
    }
    public override void OnJoinedRoom()
    {
        CloseMenu();
        roomPanel.SetActive(true);
        roomName.SetText(PhotonNetwork.CurrentRoom.Name);
        ListAllPlayer();
        SetMaterClientForRoom();
    }
    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        SetMaterClientForRoom();
    }
    private void SetMaterClientForRoom()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            readyButton.SetActive(true);
            startButton.SetActive(false);
            ActiveCharacter(0);
            ActiveCharacter(1);
            EnableSwapButton(false);
        }
        else
        {
            readyButton.SetActive(false);
            startButton.SetActive(false);
            ActiveCharacter(0);
            ActiveCharacter(1);
            EnableSwapButton(true);
            //swap.SetBeginRoom();
            swap.photonView.RPC("SetFirstSwap", RpcTarget.Others);
        }
    }

    public void ExitToMainMenu()
    {
        CloseMenu();
        menuPanel.SetActive(true);
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        CloseMenu();
        loadingPanel.SetActive(true);
        HideCharacter(0);
        HideCharacter(1);
        EnableSwapButton(false);

    }
    public override void OnLeftRoom()
    {
        CloseMenu();
        menuPanel.SetActive(true);

    }
    public void OpenFindRoom()
    {
        CloseMenu();
        findRoomPanel.SetActive(true);
    }
    public void RandomRoom()
    {

    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateCachedRoomList(roomList);
        ListAllRooms(roomsActive);
    }
    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if (info.RemovedFromList)
            {
                cachedRoomList.Remove(info.Name);
            }
            else
            {
                cachedRoomList[info.Name] = info;
            }
        }
        roomsActive = new List<RoomInfo>(cachedRoomList.Values);

    }
    private void ListAllRooms(List<RoomInfo> roomList)
    {
        foreach (RoomButton room in allRoomButtons)
        {
            if (room != null)
            {
                Destroy(room.gameObject);
            }
        }
        allRoomButtons.Clear();
        foreach (RoomInfo info in roomList)
        {
            if (info.PlayerCount == 1)
            {
                RoomButton newButton = Instantiate(roomButton, content);
                newButton.SetButtonDetails(info);
                allRoomButtons.Add(newButton);
            }
        }
    }
    public void JoinRoom(RoomInfo room)
    {
        PhotonNetwork.JoinRoom(room.Name);
        CloseMenu();
        loadingPanel.SetActive(true);
    }
    private void ListAllPlayer()
    {
        foreach (TMP_Text player in allPlayerNames)
        {
            Destroy(player.gameObject);
        }
        allPlayerNames.Clear();
        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            TMP_Text newPlayer = Instantiate(playerName, playerJoinRoom);
            newPlayer.text = players[i].NickName;
            newPlayer.gameObject.SetActive(true);
            allPlayerNames.Add(newPlayer);
            if (!players[i].IsMasterClient)
            {
                hasReady = newPlayer.transform.Find("hasReady").GetComponent<Image>().gameObject;
            }
            else
            {
                SetMaterClientForRoom();
            }
        }
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {

        TMP_Text newPlayerLabel = Instantiate(playerName, playerJoinRoom);
        newPlayerLabel.text = newPlayer.NickName;
        newPlayerLabel.gameObject.SetActive(true);
        allPlayerNames.Add(newPlayerLabel);
        if (!newPlayer.IsMasterClient)
        {
            hasReady = newPlayerLabel.transform.Find("hasReady").GetComponent<Image>().gameObject;
        }
        if(PhotonNetwork.IsMasterClient)
        {
            SetMaterClientForRoom();
        }
    }
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        ListAllPlayer();
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CloseMenu();
        menuPanel.SetActive(true);
    }
    public void ReadyButton()
    {
        isReady = !isReady;
        photonView.RPC("SetPlayerReady", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber, isReady);
    }
    [PunRPC]
    public void SetPlayerReady(int actorNumber, bool readyState)
    {
        Photon.Realtime.Player player = PhotonNetwork.CurrentRoom.GetPlayer(actorNumber);
        player.CustomProperties["IsReady"] = readyState;
        hasReady.SetActive(readyState);
        CheckAllPlayerReady();
    }
    private void CheckAllPlayerReady()
    {
        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;
        
        bool allPlayersReady = true;
        foreach (Photon.Realtime.Player player in players)
        {
            if (player.ActorNumber != PhotonNetwork.MasterClient.ActorNumber)
            {
                if (!player.CustomProperties.ContainsKey("IsReady") || !(bool)player.CustomProperties["IsReady"])
                {
                    allPlayersReady = false;
                    break;
                }
            }
        }
        if (allPlayersReady)
        {
            WhenAllPlayerReady(true);
        }
        else
        {
            WhenAllPlayerReady(false);
        }
    }
    private void WhenAllPlayerReady(bool State)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log(State);
            startButton.SetActive(State);
        }
    }
    private void ActiveCharacter(int index){
        characterModels[index].SetActive(true);
    }
    private void HideCharacter(int index){
        characterModels[index].SetActive(false);
    }
    private void EnableSwapButton(bool toogle){
        swapButton.SetActive(toogle);
    }
    private void SwapCharacter(){
        Vector3 temp = characterModels[0].transform.position;
        characterModels[0].transform.position = characterModels[1].transform.position;
        characterModels[1].transform.position = temp;
    }
    public void StartGame()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LoadLevel(Map);
    }
}
