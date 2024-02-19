using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundChanger : MonoBehaviour
{
    [SerializeField] private List<Sprite> _backgrounds;
    [SerializeField] private Image _backImage;

    private void Awake()
    {
        UpdateBackground();
    }

    public void UpdateBackground()
    {
        var back = SaveSystem.LoadData<BackgroundsSaveData>();
        _backImage.sprite = _backgrounds[back.CurrentBackground];
    } 
}
