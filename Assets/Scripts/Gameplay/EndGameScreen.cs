using TMPro;
using UnityEngine;

public class EndGameScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _time, _difficulty, _errors;

    public void SetText(Difficulty difficulty, float time, int errors)
    {
        _difficulty.text = "Difficulty: " + difficulty.ToString();
        _time.text = "Time: " + StatManager.FormatTime(time);
        _errors.text = errors.ToString() + " errors";
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }
}
