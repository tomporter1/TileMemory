using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSettings : MonoBehaviour
{
    [SerializeField]
    private Slider _rowSlider, _columnSlider;

    private void Start()
    {
        GetComponentInParent<Canvas>().gameObject.SetActive(false);
    }
    public void Save()
    {
        DifficultySettingsData difficultySettingsData = new();

        difficultySettingsData.UpdateCustomSettings((int)_columnSlider.value, (int)_rowSlider.value);
    }
}
