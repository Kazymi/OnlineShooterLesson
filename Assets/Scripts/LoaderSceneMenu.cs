using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoaderSceneMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField LoginInputField;
    [SerializeField] private TMP_InputField PasswordInputField;

    [SerializeField] private Button SignInButton;
    [SerializeField] private Button SignUpButton;

    private void Start()
    {
        AuthenticationService.Instance.SignedIn += InstanceOnSignedIn;
        SignInButton.onClick.AddListener(() =>
        {
            AuthenticationManager.Instance.TryAuthenticate(LoginInputField.text, PasswordInputField.text);
        });
        SignUpButton.onClick.AddListener(() =>
        {
            AuthenticationManager.Instance.TryRegistration(LoginInputField.text, PasswordInputField.text);
        });
    }

    private void InstanceOnSignedIn()
    {
        SceneManager.LoadScene(1);
    }
}