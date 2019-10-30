using UnityEngine;

[RequireComponent(typeof(SceneLoader))]
public class SetDifficulty : MonoBehaviour
{
    [SerializeField]
    private Difficulty _difficulty;
    [SerializeField]
    private GameDifficulty _gameDifficulty;

    public void StartGame()
    {
        _gameDifficulty.SetGameDifficulty(_difficulty);
        GetComponent<SceneLoader>().LoadScene();
    }
}
