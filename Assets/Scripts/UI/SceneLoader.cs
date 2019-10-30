using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private int SceneIndex;

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneIndex);
    }
}
