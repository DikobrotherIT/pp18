using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Button _startLevelButton;
    [SerializeField] private List<Image> _stars;
    [SerializeField] private Sprite _star;
    [SerializeField] private TMP_Text _levelNumber;
    [SerializeField] private GameObject _locker;
    private int _levelIndex = 0;
    public Action<int> StartLevel;
    public Action ClosedLevel;


    public void Init(int index)
    {
        _levelIndex = index;
        _levelNumber.text = (_levelIndex + 1).ToString();
    }

    public void UpdateLevel()
    {
        var levels = SaveSystem.LoadData<LevelsSaveData>();
        _startLevelButton.onClick.RemoveAllListeners();
        if (levels.UnlockedLevels[_levelIndex])
        {
            _levelNumber.gameObject.SetActive(true);
            _locker.SetActive(false);
            _startLevelButton.onClick.AddListener(OnStartButtonClicked);
        }
        else
        {
            _levelNumber.gameObject.SetActive(false);
            _locker.SetActive(true);
            _startLevelButton.onClick.AddListener(OnClosedLevelClicked);
        }
        for (int i = 0; i < levels.Stars[_levelIndex]; i++)
        {
            _stars[i].sprite = _star;
        }
    }

    private void OnStartButtonClicked()
    {
        StartLevel?.Invoke(_levelIndex);
    }

    private void OnClosedLevelClicked()
    {
        ClosedLevel?.Invoke();
    }
}
