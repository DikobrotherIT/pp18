using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsController : MonoBehaviour
{
    [SerializeField] private GameObject _levelCanvas;
    [SerializeField] private GameObject _levelClosedCanvas;
    [SerializeField] private List<LevelSO> _levels;
    [SerializeField] private LevelButton _levelButton;
    [SerializeField] private Transform _levelButtonContainer;
    [SerializeField] private GameController _gameController;
    [SerializeField] private GameObject _panel;
    private List<LevelButton> _levelButtons = new List<LevelButton>();

    private void Awake()
    {
        _gameController.NextClicked += OnStartLevel;
        _gameController.GameClosed += OpenLevels;
        Wallet.SetStartMoney();
        for (int i = 0; i < _levels.Count; i++)
        {
            LevelButton button = Instantiate(_levelButton, _levelButtonContainer);
            _levelButtons.Add(button);
            button.Init(i);
            button.StartLevel += OnStartLevel;
            button.ClosedLevel += OnCloseLevel;
        }
        Instantiate(_panel, _levelButtonContainer);
        Instantiate(_panel, _levelButtonContainer);
        Instantiate(_panel, _levelButtonContainer);
        Instantiate(_panel, _levelButtonContainer);
    }

    private void OnEnable()
    {
        foreach (var item in _levelButtons)
        {
            item.UpdateLevel();
        }
    }

    public void OnCloseLevel()
    {
        _levelClosedCanvas.SetActive(true);
    }

    public void HideCloseCanvas()
    {
        _levelClosedCanvas.SetActive(false);
    }

    public void OnStartLevel(int index)
    {
        CloseLevels();
        if(index >= _levels.Count)
        {
            _gameController.CloseGame();

        }
        else
        {
            _gameController.StartGame(_levels[index], index);
        }
    }

    public void OpenLevels()
    {
        _levelCanvas.SetActive(true);
    }

    public void CloseLevels()
    {
        _levelCanvas.SetActive(false);
    }
}
