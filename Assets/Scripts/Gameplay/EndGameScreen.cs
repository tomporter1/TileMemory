using System;
using TMPro;
using UnityEngine;

public class EndGameScreen : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private TextMeshProUGUI time, difficulty, errors;

    public void ShowPanel(Difficulty difficulty, float time, int errors)
    {
        this.difficulty.text = "Difficulty: " + difficulty.ToString();
        this.time.text = "Time: " + StatManager.FormatTime(time);
        this.errors.text = errors.ToString() + " errors";

        gameObject.SetActive(true);
        LeanTween.alphaCanvas(canvasGroup, 1, 0.3f);
    }

    void Awake()
    {
        GameManager.onGameReset.AddListener(HidePanel);
    }

    private void HidePanel()
    {
        LeanTween.alphaCanvas(canvasGroup, 0, 0);
        gameObject.SetActive(false);
    }
}