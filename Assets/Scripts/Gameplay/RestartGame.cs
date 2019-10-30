using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public void Restart()
    {
        GameManager.onGameReset.Invoke();
    }
}
