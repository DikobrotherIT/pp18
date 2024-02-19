using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _gameCanvas;
    [SerializeField] private Button _startButton;
    [SerializeField] private Transform _plincoPoint;
    [SerializeField] private TMP_Text _task;
    [SerializeField] private TMP_Text _betText;
    [SerializeField] private TMP_Text _ballsText;
    [SerializeField] private int _startBalls;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private List<TMP_Text> _levelTexts;
    [SerializeField] private AudioSource _droped;
    [SerializeField] private AudioSource _win;
    [SerializeField] private AudioSource _lose;
    private LevelSO _levelSO;
    private int _currentLevelIndex;
    private int _currentBalls;
    private int _taskPoints;
    private int _currentPoints;
    private int _currentMoney = 0;
    private PlincoGamefield _plincoGamefield;
    public Action<int> NextClicked;
    public Action GameClosed;


    public void StartGame(LevelSO level, int index)
    {
        _levelSO = level;
        _plincoGamefield = Instantiate(level.PlincoGamefield, _plincoPoint);
        _plincoGamefield.Init(this);
        _startButton.onClick.RemoveAllListeners();
        _startButton.onClick.AddListener(OnStartButtonClicked);
        _startButton.interactable = true;
        _gameCanvas.SetActive(true);
        _currentPoints = 0;
        _taskPoints = level.Task;
        _currentMoney = 100;
        _betText.text = _currentMoney.ToString();
        _currentBalls = _startBalls;
        _currentLevelIndex = index;
        int currentLevel = _currentLevelIndex + 1;
        foreach (var item in _levelTexts)
        {
            item.text = "Level " + currentLevel;
        }
        UpdateBallsText();
        UpdateTaskText();
    }

    public void ResetGame()
    {
        if(_plincoGamefield != null)
        {
            Destroy(_plincoGamefield.gameObject);
            _plincoGamefield = null;
        }
        _winPanel.SetActive(false);
        _losePanel.SetActive(false);
    }

    public void IncreaseBet()
    {
        if(Wallet.CanRemoveMoney(_currentMoney * 2))
        {
            _currentMoney = _currentMoney * 2;
            _betText.text = _currentMoney.ToString();
        }
    }

    public void DecreaseBet()
    {
        if(_currentMoney / 2 >= 100)
        {
            _currentMoney = _currentMoney / 2;
            _betText.text = _currentMoney.ToString();
        }
    }

    public void UpdateBallsText()
    {
        _ballsText.text = _currentBalls + "/" + _startBalls;
    }

    public void UpdateTaskText()
    {
        _task.text = _currentPoints + "/" + _taskPoints / 1000 + "K";
    }

    public void PlayDropSFX()
    {
        _droped.Play();
    }

    public void OnStartButtonClicked()
    {
        if (Wallet.CanRemoveMoney(_currentMoney))
        {
            Wallet.RemoveMoney(_currentMoney);
            _currentBalls--;
            UpdateBallsText();
            _startButton.interactable = false;
            _plincoGamefield.DropBall(_currentMoney);
        }
    }

    public void OnRoundEnded(int money)
    {
        Wallet.AddMoney(money);
        _currentPoints += money;
        UpdateTaskText();
        if (_currentPoints >= _taskPoints)
        {
            EndGame();
        }
        else
        {
            _startButton.interactable = true;
        }

    }

    public void CloseGame()
    {
        ResetGame();
        _gameCanvas.SetActive(false);
        GameClosed?.Invoke();
    }

    public void RestartGame()
    {
        ResetGame();
        StartGame(_levelSO, _currentLevelIndex);
    }

    public void OnNextButtonClicked()
    {
        ResetGame();
        NextClicked?.Invoke(_currentLevelIndex + 1);
    }


    public void EndGame()
    {
        if(_currentPoints >= _taskPoints)
        {
            _winPanel.SetActive(true);
            _win.Play();
            var levels = SaveSystem.LoadData<LevelsSaveData>();
            levels.Stars[_currentLevelIndex] = 1;
            if(_startBalls - _currentBalls <= 6)
            {
                levels.Stars[_currentLevelIndex] = 2;
            }
            if (_startBalls - _currentBalls <= 3)
            {
                levels.Stars[_currentLevelIndex] = 3;
            }
            if (_currentLevelIndex < levels.UnlockedLevels.Count - 1)
            {
                levels.UnlockedLevels[_currentLevelIndex + 1] = true;
            }
            SaveSystem.SaveData(levels);
        }
        else
        {
            _losePanel.SetActive(true);
            _lose.Play();
        }
    }
}
