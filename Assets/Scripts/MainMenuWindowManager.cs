using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuWindowManager : MonoBehaviour
{
    [SerializeField] private WindowData[] windowDatas;

    public static MainMenuWindowManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void ShowWindow(MainMenuWindowType windowType)
    {
        foreach (var windowData in windowDatas)
        {
            windowData.Window.gameObject.SetActive(windowType == windowData.WindowType);
        }
    }
}

[Serializable]
public class WindowData
{
    public GameObject Window;
    public MainMenuWindowType WindowType;
}

public enum MainMenuWindowType
{
    Loading,
    NickNameWindow,
    MainMenuWindow,
    CreateRoomWindow,
    ConnectedToRoomWindow,
    RoomWindow,
}