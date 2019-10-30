using System.Collections;
using TMPro;
using UnityEngine;

public class ShowBoard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI countdownText;

    void Start()
    {
        GameManager.onGameReset.AddListener(CancelTileCountdown);
    }

    private void CancelTileCountdown()
    {
        StopAllCoroutines();
    }

    internal void ShowBoardForSecs(int time)
    {
        StartCoroutine(BoardCountdown(time));
    }

    /// <summary>
    /// Shows the board for a specified number of seconds
    /// </summary>
    /// <param name="time">The nymber of seconds to show the board for</param>
    /// <returns></returns>
    private IEnumerator BoardCountdown(int time)
    {
        countdownText.gameObject.SetActive(true);
        for (int x = 0; x < time; x++)
        {
            countdownText.text = "Hiding tiles in: " + (time - x).ToString() + " seconds";
            yield return new WaitForSeconds(1);
        }
        countdownText.text = "";
        countdownText.gameObject.SetActive(false);
        HideTiles.onTilesHide.Invoke();
    }
}
