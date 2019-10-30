using UnityEngine;

/// <summary>
/// Used to cary the selected game difficulty between scenes
/// </summary>
public class GameDifficulty : MonoBehaviour
{
    [SerializeField]
    private Difficulty _difficulty;
    public static GameDifficulty gameDifficulty;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoadManager.AddObj(gameObject);
        gameDifficulty = this;
    }

    internal void SetGameDifficulty(Difficulty newDifficulty)
    {
        _difficulty = newDifficulty;
    }

    public Difficulty GetCurrentGameDifficulty()
    {
        return _difficulty;
    }

    public void DestroyOnLoad()
    {
        DontDestroyOnLoadManager.RemoveObj(gameObject);
    }
}