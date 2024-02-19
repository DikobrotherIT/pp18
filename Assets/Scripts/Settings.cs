using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
//using UnityEngine.iOS;
public class Settings : MonoBehaviour
{
    [SerializeField] private string _email;
    [SerializeField] private GameObject _visualEditor;
    [SerializeField] private GameObject _rateUs;
    [SerializeField] private GameObject _privacy;
    [SerializeField] private GameObject _terms;
    [SerializeField] private Slider _slider;
    [SerializeField] private AudioMixer _audio;
    [SerializeField] private List<Image> _stars;
    [SerializeField] private List<Sprite> _startSprites;

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(ChangeSound);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveAllListeners();
    }

    public void ChangeSound(float value)
    {
        _audio.SetFloat("Volume", value);
    }

    public void ShowVisual()
    {
        _visualEditor.SetActive(true);
        gameObject.SetActive(false);
    }

    public void HideVisual()
    {
        _visualEditor.SetActive(false);
        gameObject.SetActive(true);
    }

    public void RateUs()
    {
#if UNITY_ANDROID
        Application.OpenURL("market://details?id=" + Application.identifier);
//#elif UNITY_IPHONE
//        Device.RequestStoreReview();
#endif
    }

    public void ClickOnStar(int index)
    {
        foreach (var item in _stars)
        {
            item.sprite = _startSprites[0];
        }
        for (int i = 0; i < index; i++)
        {
            _stars[i].sprite = _startSprites[1];
        }
    }

    public void ShowRateUs()
    {
        _rateUs.SetActive(true);
    }

    public void HideRateUs()
    {
        _rateUs.SetActive(false);
    }

    public void ShowPrivacy()
    {
        _privacy.SetActive(true);
        gameObject.SetActive(false);
    }

    public void HidePrivacy()
    {
        _privacy.SetActive(false);
        gameObject.SetActive(true);
    }

    public void ShowTerms()
    {
        _terms.SetActive(true);
        gameObject.SetActive(false);
    }

    public void HideTerms()
    {
        _terms.SetActive(false);
        gameObject.SetActive(true);
    }

    public void ContactWithDeveloper()
    {
        Application.OpenURL("mailto:" + _email + "?subject=Mail to developer");
    }
}
