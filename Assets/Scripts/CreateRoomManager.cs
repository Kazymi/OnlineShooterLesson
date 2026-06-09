using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField roomName;
    [SerializeField] private Slider maxPlayerSlider;
    [SerializeField] private Button createRoomButton;
    [SerializeField] private Button returnToMainMenuButton;

    private void Awake()
    {
        createRoomButton.onClick.AddListener(CreateRoom);
        returnToMainMenuButton.onClick.AddListener(() =>
            MainMenuWindowManager.Instance.ShowWindow(MainMenuWindowType.MainMenuWindow));
    }

    private void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomName.text) || roomName.text.Length > 10)
        {
            Debug.Log("Your room name is unCorrectly");
            return;
        }

        var roomSetting = new RoomOptions
        {
            MaxPlayers = (byte)maxPlayerSlider.value,
        };
        PhotonNetwork.CreateRoom(roomName.text, roomSetting, TypedLobby.Default);
        MainMenuWindowManager.Instance.ShowWindow(MainMenuWindowType.Loading);
        Debug.Log("Start room create");
    }
}