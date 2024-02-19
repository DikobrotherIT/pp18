using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelsData", order = 1)]
public class LevelSO : ScriptableObject
{
    public PlincoGamefield PlincoGamefield;
    public int Task; 
}
