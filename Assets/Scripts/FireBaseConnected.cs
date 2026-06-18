using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using Newtonsoft.Json;
using UnityEngine;

public class FireBaseConnected : MonoBehaviour
{
    private async void Start()
    {
        var result = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (result == DependencyStatus.Available)
        {
            Debug.Log($"Successfully connection.");
            FirebaseDatabase.GetInstance("https://onlineshooter-a75fd-default-rtdb.europe-west1.firebasedatabase.app").SetPersistenceEnabled(false);
        }
    }

    public async Task SavePlayerData(string playerId, PlayerData playerData)
    {
        string json = JsonConvert.SerializeObject(playerData);
        await FirebaseDatabase.GetInstance("https://onlineshooter-a75fd-default-rtdb.europe-west1.firebasedatabase.app").RootReference.Child("player").Child(playerId).SetRawJsonValueAsync(json);
        Debug.Log("Save");
    }

    public async Task<PlayerData> LoadPlayerData(string playerId)
    {
        var snapshot = await FirebaseDatabase.GetInstance("https://onlineshooter-a75fd-default-rtdb.europe-west1.firebasedatabase.app").RootReference.Child("player").Child(playerId)
            .GetValueAsync();
        if (snapshot.Exists == false) return null;
        string json = snapshot.GetRawJsonValue();
        return JsonConvert.DeserializeObject<PlayerData>(json);
    }
}

[Serializable]
public class PlayerData
{
    public string NickName;
}