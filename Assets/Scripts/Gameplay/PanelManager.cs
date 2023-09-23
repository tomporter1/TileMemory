using System;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [Serializable]
    public struct PanelInfo
    {
        public Panel panelType;
        public GameObject panel;
    }

    [SerializeField]
    private List<PanelInfo> panels;

    [Serializable]
    public enum Panel { Home, Game, Settings, Stats }

    [Header("Managers")]
    [SerializeField]
    private GameManager gameManager;

    private void Start()
    {
        ShowPanel(Panel.Home);
    }

    public void ShowPanel(Panel panelType, Action onComplete = null)
    {
        foreach (PanelInfo panelInfo in panels)
        {
            if (panelInfo.panelType == panelType)
            {
                panelInfo.panel.SetActive(true);
                LeanTween.alphaCanvas(panelInfo.panel.GetComponent<CanvasGroup>(), 1, 0.3f).setEaseInOutSine().setOnComplete(() => onComplete?.Invoke());
            }
            else
            {
                LeanTween.alphaCanvas(panelInfo.panel.GetComponent<CanvasGroup>(), 0, 0.3f).setEaseInOutSine().setOnComplete(() => panelInfo.panel.SetActive(false));
            }
        }
    }

    public void ShowHome() => ShowPanel(Panel.Home);
    public void ShowGame()
    {
        GameManager.onGameReset.Invoke();
        ShowPanel(Panel.Game, () => gameManager.StartGame());
    }

    public void ShowSettings() => ShowPanel(Panel.Settings);
    public void ShowStats() => ShowPanel(Panel.Stats);
}
