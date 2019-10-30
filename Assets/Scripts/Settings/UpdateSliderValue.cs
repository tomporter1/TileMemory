using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpdateSliderValue : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    void Start()
    {
        UpdateLabel();
    }

    public void UpdateLabel()
    {
        GetComponent<TextMeshProUGUI>().text = _slider.value.ToString();
    }
}
