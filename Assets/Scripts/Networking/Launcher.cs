using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Photon.Pun;
using System.Collections.Generic;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField _roomNameInputField;
    [SerializeField] TMP_Text _roomNameText;

    [Space(10)]

    [SerializeField] Transform _roomListContent;
    [SerializeField] Transform _playerListContent;

    [SerializeField] GameObject _roomListItemPrefab;
    [SerializeField] GameObject _playerListItemPrefab;

    [Space(10)]

    [SerializeField] GameObject _startGameButton;

    public static Launcher _instance;
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        MenuActorManager.instance.OpenMenu("LOADING");
        PhotonNetwork.ConnectUsingSettings();
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(_roomNameInputField.text))
            return;

        PhotonNetwork.CreateRoom(_roomNameInputField.text);
        MenuActorManager.instance.OpenMenu("LOADING");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuActorManager.instance.OpenMenu("LOADING");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuActorManager.instance.OpenMenu("LOADING");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby()
    {
        MenuActorManager.instance.OpenMenu("TITLE");
        Debug.Log("OnJoinedLobby");
        PhotonNetwork.NickName = "Player: " + Random.Range(0, 9999).ToString("0000");
    }
    public override void OnJoinedRoom()
    {
        MenuActorManager.instance.OpenMenu("ROOM_MENU");
        _roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] m_players = PhotonNetwork.PlayerList;

        foreach (Transform m_child in _playerListContent)
            Destroy(m_child.gameObject);

        for (int i = 0; i < m_players.Length; i++)
        {
            Instantiate(_playerListItemPrefab, _playerListContent).GetComponent<PlayerListItem>().SetUp(m_players[i]);
        }

        _startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        _startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        MenuActorManager.instance.OpenMenu("TITLE");
    }
    public override void OnLeftRoom()
    {
        MenuActorManager.instance.OpenMenu("TITLE");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform m_transform in _roomListContent)
            Destroy(m_transform.gameObject);

        for(int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;

            Instantiate(_roomListItemPrefab, _roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(_playerListItemPrefab, _playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
}