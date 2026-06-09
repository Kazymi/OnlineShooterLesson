using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NickNameWindow : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button enterButton;

    private void Awake()
    {
        enterButton.onClick.AddListener(() =>
        {
            if (nameInputField.text != "" && nameInputField.text.Length >= 3)
            {
                PhotonNetwork.NickName = nameInputField.text;
                MainMenuWindowManager.Instance.
                    ShowWindow(MainMenuWindowType.MainMenuWindow);
            }
        });
    }
}