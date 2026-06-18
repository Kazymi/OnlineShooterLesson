using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Unity.Services.Authentication;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private FireBaseConnected firebaseConnected;
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
        TryLoadPlayerData();
    }

    private async void TryLoadPlayerData()
    {
        var playerData = await firebaseConnected.LoadPlayerData(AuthenticationService.Instance.PlayerId);
        if (playerData != null && playerData.NickName != null)
        {
            PhotonNetwork.NickName = playerData.NickName;
            MainMenuWindowManager.Instance.ShowWindow(MainMenuWindowType.MainMenuWindow);
        }
        else
        {
            MainMenuWindowManager.Instance.ShowWindow(MainMenuWindowType.NickNameWindow);
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Photon manager disconnected from server");
    }
}