using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private string region = "ru";

    private void Start()
    {
        MainMenuWindowManager.Instance.ShowWindow(MainMenuWindowType.Loading);
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion(region);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnConnected()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log($"Photon manager connected to server {PhotonNetwork.CloudRegion}");
        if (string.IsNullOrEmpty(PhotonNetwork.NickName))
        {
            MainMenuWindowManager.Instance.ShowWindow(MainMenuWindowType.NickNameWindow);
        }
        else
        {
            MainMenuWindowManager.Instance.ShowWindow(MainMenuWindowType.MainMenuWindow);
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Photon manager disconnected from server");
    }
}