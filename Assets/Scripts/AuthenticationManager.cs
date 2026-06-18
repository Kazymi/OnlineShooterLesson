using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class AuthenticationManager : MonoBehaviour
{
    public static AuthenticationManager Instance;
    private const string savePathLogin = "Login";
    private const string savePathPassword = "Password";
    public bool IsInitialized { get; private set; }

    private async void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        await Authenticate();
        TryAuthenticateFromToken();
    }

    private void TryAuthenticateFromToken()
    {
        var login = PlayerPrefs.GetString(savePathLogin);
        var password = PlayerPrefs.GetString(savePathPassword);
        if (string.IsNullOrEmpty(login) == false && string.IsNullOrEmpty(password) == false)
        {
            TryAuthenticate(login, password);
            Debug.Log("Authentication Successful from save path");
            Debug.Log(AuthenticationService.Instance.PlayerId);
        }
    }

    private async Task Authenticate()
    {
        try
        {
            await UnityServices.InitializeAsync();
            IsInitialized = true;
        }
        catch (Exception e)
        {
            Debug.Log("Error");
            throw;
        }
    }

    private void SaveToken(string login, string password)
    {
        PlayerPrefs.SetString(savePathLogin, login);
        PlayerPrefs.SetString(savePathPassword, password);
        PlayerPrefs.Save();
    }

    public async void TryAuthenticate(string login, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(login, password);
            Debug.Log("Logging in");
            Debug.Log(AuthenticationService.Instance.PlayerId);
            SaveToken(login, password);
        }
        catch (Exception e)
        {
            Debug.Log("Не правильный логин или пароль");
            throw;
        }
    }

    public async void TryRegistration(string login, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(login, password);
            Debug.Log("Account created");
            Debug.Log(AuthenticationService.Instance.PlayerId);
            SaveToken(login, password);
        }
        catch (Exception e)
        {
            Debug.Log("Не правильный логин или пароль");
            throw;
        }
    }

    public void Logout()
    {
        AuthenticationService.Instance.SignOut();
        PlayerPrefs.SetString(savePathLogin, "");
        PlayerPrefs.SetString(savePathPassword, "");
        PlayerPrefs.Save();
    }
}