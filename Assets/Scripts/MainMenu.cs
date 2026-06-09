using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
   [SerializeField] private Button createRoom;
   [SerializeField] private Button listRoom;

   private void Awake()
   {
      createRoom.onClick.AddListener(() =>
      {
         MainMenuWindowManager.Instance.ShowWindow(MainMenuWindowType.CreateRoomWindow);
      });
      
      listRoom.onClick.AddListener(() =>
      {
         MainMenuWindowManager.Instance.ShowWindow(MainMenuWindowType.ConnectedToRoomWindow);
      });
   }
}
