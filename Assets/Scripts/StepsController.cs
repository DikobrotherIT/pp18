using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StepsController : MonoBehaviour
{
    [SerializeField] private GameObject _firstCanvas;
    [SerializeField] private GameObject _secondCanvas;
    [SerializeField] private GameObject _privacyCanvas;
    [SerializeField] private PrivacyShower _privacyShower;

    private void Start()
    {
        GoToFirstStep();
    }

    public void GoToFirstStep()
    {
        _firstCanvas.SetActive(true);
    }

    public void GoToSecondStep()
    {
        _firstCanvas.SetActive(false);
        _secondCanvas.SetActive(true);
    }

    public void EndOnboarding()
    {       
        _secondCanvas.SetActive(false);
        _privacyCanvas.SetActive(true);
        _privacyShower.ShowPrivacy();
    }
}
