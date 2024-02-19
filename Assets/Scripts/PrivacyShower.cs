using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrivacyShower : MonoBehaviour
{
    [SerializeField] private UniWebView _uniWebView;
    [SerializeField] private string _privacyUrl;
    [SerializeField] private Button _agree;

    public void ShowPrivacy()
    {
        _uniWebView.Load(_privacyUrl);
        _uniWebView.OnPageFinished += OnPrivacyLoaded;
        _uniWebView.OnPageErrorReceived += LoadWithError;
        _agree.interactable = true;
    }

    private void LoadWithError(UniWebView webView, int errorCode, string errorMessage)
    {
        _uniWebView.Show();
    }

    private void OnPrivacyLoaded(UniWebView webView, int statusCode, string url)
    {
        _uniWebView.Show();
    }

    public void OnAgreeButtonClicked()
    {
        _uniWebView.Hide();
        PlayerPrefs.SetInt("Onboarding", 1);
        SceneManager.LoadScene("MainScene");
    }
}
