using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;

public class NickNameWindow : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button enterButton;
    [SerializeField] private FireBaseConnected firebaseConnected;

    private void Awake()
    {
        enterButton.onClick.AddListener(() =>
        {
            if (nameInputField.text != "" && nameInputField.text.Length >= 3)
            {
                PhotonNetwork.NickName = nameInputField.text;
                MainMenuWindowManager.Instance.ShowWindow(MainMenuWindowType.MainMenuWindow);
                firebaseConnected.SavePlayerData(AuthenticationService.Instance.PlayerId, new PlayerData()
                {
                    NickName = nameInputField.text
                });
            }
        });
    }
}