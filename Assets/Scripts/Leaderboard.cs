using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private List<string> _nicknames;
    [SerializeField] private List<int> _score;
    [SerializeField] private List<TMP_Text> _highPlaceTexts;
    [SerializeField] private List<TMP_Text> _nicknamesTexts;
    [SerializeField] private List<TMP_Text> _scoreTexts;

    private void Awake()
    {
        FillLeaderboard();
    }

    private void FillLeaderboard()
    {
        for (int i = 0; i < _highPlaceTexts.Count; i++)
        {
            _highPlaceTexts[i].text = _nicknames[i];
        }
        for (int i = 0; i < _nicknamesTexts.Count; i++)
        {
            _nicknamesTexts[i].text = _nicknames[i];
        }
        for (int i = 0; i < _scoreTexts.Count; i++)
        {
            _scoreTexts[i].text = _score[i].ToString();
        }
    }
}
