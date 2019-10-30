using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Countdown : MonoBehaviour
{
    [SerializeField]
    private int _seconds;
    [SerializeField]
    private TextMeshProUGUI _countdownText;

    internal static UnityEvent onCountdownEnd = new UnityEvent();
    internal static UnityEvent onCountdownStart = new UnityEvent();

    void Start()
    {
        onCountdownEnd.AddListener(HideCountdown);
        GameManager.onGameReset.AddListener(CancelCountdown);
        HideCountdown();
    }

    private void CancelCountdown()
    {
        StopAllCoroutines();
    }

    internal IEnumerator StartCountdown()
    {
        ShowCountdown();
        //_countdownText.color = Color.black;
        for (int x = 0; x < _seconds; x++)
        {
            _countdownText.text = (_seconds - x).ToString();
            yield return new WaitForSeconds(1);
        }
        onCountdownEnd.Invoke();
    }

    private void HideCountdown()
    {
        gameObject.SetActive(false);
    }

    private void ShowCountdown()
    {
        gameObject.SetActive(true);
    }
}
