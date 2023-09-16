using UnityEngine;

public class SetDifficulty : MonoBehaviour
{
    [SerializeField]
    private Difficulty _difficulty;
    [SerializeField]
    private GameDifficulty _gameDifficulty;

    public static PanelManager _panelManager;
    private static PanelManager panelManager
    {
        get
        {
            if (_panelManager == null)
                _panelManager = FindObjectOfType<PanelManager>();
            return _panelManager;
        }
    }

    public void StartGame()
    {
        _gameDifficulty.SetGameDifficulty(_difficulty);
        panelManager.ShowGame();
    }
}
