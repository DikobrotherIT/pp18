using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Navigator : MonoBehaviour
{
    [SerializeField] private List<Image> _buttonsImages;
    [SerializeField] private List<Sprite> _activeSprites;
    [SerializeField] private List<Sprite> _inactiveSprites;
    [SerializeField] private List<GameObject> _canvases;
    [SerializeField] private GameController _gameController;

    private void Awake()
    {
        OpenLevels();
    }

    public void OpenShop()
    {
        for (int i = 0; i < _buttonsImages.Count; i++)
        {
            _buttonsImages[i].sprite = _inactiveSprites[i];
        }
        _buttonsImages[0].sprite = _activeSprites[0];
        foreach (var item in _canvases)
        {
            item.SetActive(false);
        }
        _canvases[0].SetActive(true);
        _gameController.ResetGame();
    }

    public void OpenLevels()
    {
        for (int i = 0; i < _buttonsImages.Count; i++)
        {
            _buttonsImages[i].sprite = _inactiveSprites[i];
        }
        _buttonsImages[1].sprite = _activeSprites[1];
        foreach (var item in _canvases)
        {
            item.SetActive(false);
        }
        _canvases[1].SetActive(true);
        _gameController.ResetGame();
    }

    public void OpenLeaders()
    {
        for (int i = 0; i < _buttonsImages.Count; i++)
        {
            _buttonsImages[i].sprite = _inactiveSprites[i];
        }
        _buttonsImages[2].sprite = _activeSprites[2];
        foreach (var item in _canvases)
        {
            item.SetActive(false);
        }
        _canvases[2].SetActive(true);
        _gameController.ResetGame();
    }

    public void OpenSettings()
    {
        for (int i = 0; i < _buttonsImages.Count; i++)
        {
            _buttonsImages[i].sprite = _inactiveSprites[i];
        }
        _buttonsImages[3].sprite = _activeSprites[3];
        foreach (var item in _canvases)
        {
            item.SetActive(false);
        }
        _canvases[3].SetActive(true);
        _gameController.ResetGame();
    }


}
