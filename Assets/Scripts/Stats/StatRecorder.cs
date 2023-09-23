using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class StatRecorder : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timer;
    [SerializeField]
    private EndGameScreen _endScreen;
    private float startTime;
    private IEnumerator gameTimer;
    private bool isTiming = false;
    private StatsData statsData;
    int numOfErrors = 0;

    public static UnityEvent onIncorrectSelection = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        GameManager.onGameEnd.AddListener(RecordStats);
        GameManager.onGameEnd.AddListener(ClearTimer);
        GameManager.onGameReset.AddListener(ClearTimer);
        GameManager.onGameReset.AddListener(StopTimer);
        GameManager.onGameReset.AddListener(ClearErrors);
        HideTiles.onTilesHide.AddListener(StartTimer);
        onIncorrectSelection.AddListener(RecordError);

        statsData = new StatsData();
    }

    private void RecordError()
    {
        numOfErrors++;
    }

    private void ClearTimer()
    {
        _timer.text = "";
    }

    private void ClearErrors()
    {
        numOfErrors = 0;
    }

    private void StartTimer()
    {
        gameTimer = Timer();
        StartCoroutine(gameTimer);
        isTiming = true;
    }

    /// <summary>
    /// Updates the stats that are currently stored
    /// </summary>
    private void RecordStats()
    {
        StopTimer();
        //uses the recorded start time for the game to calculate how long the game tuck
        //removes the time between the game actually finishing and the the starts being recorded
        float currentGameTime = Time.time - startTime - Time.deltaTime;

        Difficulty gameDifficulty = GetComponent<GameManager>().GetCurrentGameDifficulty();

        StatType bestTimeInfo = statsData.Stats.GetStatInfo(statNames.BestTime);
        StatType averageTimeInfo = statsData.Stats.GetStatInfo(statNames.AverageTime);
        StatType timesPlayedInfo = statsData.Stats.GetStatInfo(statNames.TimesPlayed);

        ////////////////////////////////////////Times PLayed///////////////////////////////////////////////////
        string timesPLayedKey = StatManager.GenerateStatKey(timesPlayedInfo, gameDifficulty);
        int oldTimesPlayed = PlayerPrefs.GetInt(timesPLayedKey);
        //increases the num of times played
        PlayerPrefs.SetInt(timesPLayedKey, PlayerPrefs.GetInt(timesPLayedKey) + 1);
        Debug.Log("Updated times played to: " + PlayerPrefs.GetInt(timesPLayedKey));

        ////////////////////////////////////////Best time//////////////////////////////////////////////////////
        string bestKey = StatManager.GenerateStatKey(bestTimeInfo, gameDifficulty);
        float OldBest = PlayerPrefs.GetFloat(bestKey);
        //updates if new time is better of there isn't a best time set
        if (currentGameTime < OldBest || OldBest == 0)
        {
            PlayerPrefs.SetFloat(bestKey, currentGameTime);
            Debug.Log("Updated best time to: " + PlayerPrefs.GetFloat(bestKey));
        }

        ////////////////////////////////////////Average time///////////////////////////////////////////////////
        string averageKey = StatManager.GenerateStatKey(averageTimeInfo, gameDifficulty);
        float oldAverageTime = PlayerPrefs.GetFloat(averageKey);
        //the old average hasn't been set yet
        if (oldAverageTime == 0)
        {
            PlayerPrefs.SetFloat(averageKey, currentGameTime);
        }
        else
        {
            float newAverageTime = ((oldAverageTime * oldTimesPlayed) + currentGameTime) / (oldTimesPlayed + 1);
            PlayerPrefs.SetFloat(averageKey, newAverageTime);
        }
        Debug.Log("Updated average time to: " + PlayerPrefs.GetFloat(averageKey));

        _endScreen.ShowPanel(gameDifficulty, currentGameTime,numOfErrors);
    }

    private void StopTimer()
    {
        if (isTiming)
        {
            Debug.Log("Stopping Timer");
            StopCoroutine(gameTimer);
        }
    }

    /// <summary>
    /// Coroutine to display the timer for the current game
    /// </summary>
    /// <returns></returns>
    private IEnumerator Timer()
    {
        //records the exact start time of the game
        startTime = Time.time;

        //the interval that the clock is updated in the ui
        float timingInterval = 0.01f;
        Debug.Log("Starting Timer");
        while (true)
        {
            _timer.text = StatManager.FormatTime(Time.time - startTime);
            yield return new WaitForSeconds(timingInterval);
        }
    }
}
