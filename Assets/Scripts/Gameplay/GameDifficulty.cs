using UnityEngine;

/// <summary>
/// Used to cary the selected game difficulty between scenes
/// </summary>
public class GameDifficulty : MonoBehaviour
{
    [SerializeField]
    private Difficulty _difficulty;
    public static GameDifficulty instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    internal void SetGameDifficulty(Difficulty newDifficulty)
    {
        _difficulty = newDifficulty;
    }

    public Difficulty GetCurrentGameDifficulty()
    {
        return _difficulty;
    }
}