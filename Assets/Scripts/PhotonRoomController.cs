using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PhotonRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField] private RoomItem Prefab;
    [SerializeField] private Transform container;
    [SerializeField] private Button returnToMainMenu;


    private void Awake()
    {
        returnToMainMenu.onClick.AddListener(() =>
            MainMenuWindowManager.Instance.ShowWindow(MainMenuWindowType.MainMenuWindow));
    }

    private List<RoomItem> _currentItem = new List<RoomItem>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var currentItem in _currentItem)
        {
            Destroy(currentItem.gameObject);
        }

        var id = 10;
        var currentId = 0;
        foreach (var room in roomList)
        {
            currentId++;
            if (currentId == id) break;
            var newRoom = Instantiate(Prefab, container);
            newRoom.Initialize(room);
            _currentItem.Add(newRoom);
        }
    }
}