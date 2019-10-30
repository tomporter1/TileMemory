using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSettings : MonoBehaviour
{
    [SerializeField]
    private Slider _rowSlider, _columnSlider;

    public void Save()
    {
        DifficultySettingsData difficultySettingsData = new DifficultySettingsData();

        difficultySettingsData.UpdateCustomSettings((int)_columnSlider.value, (int)_rowSlider.value);
    }
}
