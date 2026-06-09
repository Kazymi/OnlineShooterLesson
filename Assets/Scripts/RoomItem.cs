using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    [SerializeField] private TMP_Text nameRoom;
    [SerializeField] private TMP_Text amountPlayer;
    [SerializeField] private Button connectButton;

    public void Initialize(RoomInfo roomInfo)
    {
        nameRoom.text = roomInfo.Name;
        amountPlayer.text = $"{roomInfo.PlayerCount}/{roomInfo.MaxPlayers}";
    }

    private void Start()
    {
        connectButton.onClick.AddListener(() =>
        {
            MainMenuWindowManager.Instance.ShowWindow(MainMenuWindowType.Loading);
            PhotonNetwork.JoinRoom(nameRoom.text);
        });
    }
}