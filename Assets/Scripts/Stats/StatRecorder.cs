using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class StatRecorder : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timer;
    [SerializeField]
    private EndGameScreen _endScreen;
    [SerializeField]
    private GameManager gameManager;
    private float startTime;
    private bool isTiming = false;
    private StatsData statsData;
    int numOfErrors = 0;

    public static UnityEvent onIncorrectSelection = new();

    CancellationTokenSource timerTokenSource;

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
        timerTokenSource = new CancellationTokenSource();
        RunTimer(timerTokenSource.Token);

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

        Difficulty gameDifficulty = gameManager.GetCurrentGameDifficulty();

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

        _endScreen.ShowPanel(gameDifficulty, currentGameTime, numOfErrors);
    }

    private void StopTimer()
    {
        if (isTiming)
        {
            Debug.Log("Stopping Timer");
            timerTokenSource?.Cancel();
        }
    }

    /// <summary>
    /// Coroutine to display the timer for the current game
    /// </summary>
    /// <returns></returns>
    private async void RunTimer(CancellationToken cancellationToken)
    {
        //records the exact start time of the game
        startTime = Time.time;

        //the interval that the clock is updated in the ui
        int timingInterval = 1;
        Debug.Log("Starting Timer");
        while (true)
        {
            if(cancellationToken.IsCancellationRequested)
            {
                Debug.Log("Timer Cancelled");
                return;
            }
            _timer.text = StatManager.FormatTime(Time.time - startTime);
            await Task.Delay(timingInterval);
        }
    }
}
