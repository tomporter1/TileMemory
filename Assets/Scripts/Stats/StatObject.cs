using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StatObject : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _label, _data;

    internal void SetData(StatType stat, Difficulty SelectedDifficulty)
    {
        _label.text = stat.Name + ":";

        if (stat.DataType == statTypesEnum.Int)
            _data.text = PlayerPrefs.GetInt(StatManager.GenerateStatKey(stat, SelectedDifficulty)).ToString();
        else if (stat.DataType == statTypesEnum.Float)
            _data.text = StatManager.FormatTime(PlayerPrefs.GetFloat(StatManager.GenerateStatKey(stat, SelectedDifficulty)));
    }
}
