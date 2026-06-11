using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private TMP_Text listPeopleInRoom;
    [SerializeField] private Button startGame;
    [SerializeField] private Button toMenuButton;

    private void Awake()
    {
        startGame.gameObject.SetActive(false);
        startGame.onClick.AddListener(() =>
        {
            MainMenuWindowManager.Instance.ShowWindow(MainMenuWindowType.Loading);
            PhotonNetwork.LoadLevel("GameScene");
        });
        toMenuButton.onClick.AddListener(() =>
        {
            PhotonNetwork.LeaveRoom();
            MainMenuWindowManager.Instance.ShowWindow(MainMenuWindowType.MainMenuWindow);
        });
    }

    public override void OnCreatedRoom()
    {
        startGame.gameObject.SetActive(true);
        Debug.Log("Room created");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RecalculateListPeopleInRoom();
        if (PhotonNetwork.CurrentRoom.masterClientId == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            startGame.gameObject.SetActive(true);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        RecalculateListPeopleInRoom();
    }

    public override void OnJoinedRoom()
    {
        MainMenuWindowManager.Instance.ShowWindow(MainMenuWindowType.RoomWindow);
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        RecalculateListPeopleInRoom();
        Debug.Log("Room coonected");
    }

    private void RecalculateListPeopleInRoom()
    {
        listPeopleInRoom.text = "";
        var peopleInRoom = PhotonNetwork.CurrentRoom.Players;
        foreach (var person in peopleInRoom)
        {
            listPeopleInRoom.text += $"\n{person.Value.NickName}";
        }
    }
}