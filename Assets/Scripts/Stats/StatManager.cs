using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum statTypesEnum
{
    Float,
    Int
}

public enum statNames
{
    BestTime,
    AverageTime,
    TimesPlayed
}

public class StatManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _statPrefab;
    [SerializeField]
    private Transform _statPanel;
    [SerializeField]
    private Button _defualtButton;
    [SerializeField]
    private TextMeshProUGUI _modeLabel;

    private StatsData statsData;
    public Difficulty SelectedDifficulty = Difficulty.Easy;


    void Start()
    {
        statsData = new StatsData();

        _defualtButton.onClick.Invoke();
    }

    public void SetUpStatsPanel()
    {
        SetStatObjects(SelectedDifficulty);
    }

    public void SetStatObjects(Difficulty newMode)
    {
        SelectedDifficulty = newMode;
        _modeLabel.text = SelectedDifficulty.ToString() + " statistics";

        //removes all the stat objects
        foreach (Transform child in _statPanel)
        {
            Destroy(child.gameObject);
        }

        //populates the planel with new stat data
        foreach (StatType stat in statsData.Stats.StatTypes)
        {
            GameObject newStatObj = Instantiate(_statPrefab, _statPanel, false);
            newStatObj.GetComponent<StatObject>().SetData(stat, SelectedDifficulty);
        }
    }

    /// <summary>
    /// Generates the stat key to be used in player prefs
    /// </summary>
    /// <param name="stat"></param>
    /// <param name="mode"></param>
    /// <returns>The key for player prefs</returns>
    public static string GenerateStatKey(StatType stat, Difficulty mode)
    {
        //combines the name of the stat with the difficulty
        return stat.Name + mode.ToString();
    }

    /// <summary>
    /// formats the time in the form mm:ss:ff
    /// </summary>
    /// <param name="time">The current time in seconds</param>
    /// <returns>The formatted time</returns>
    public static string FormatTime(float time)
    {
        int numOfSecs = (int)Math.Floor(time);
        int secsRemainder = numOfSecs % 60;
        int numOfMinutes = (numOfSecs - secsRemainder) / 60;
        int secFraction = (int)(Math.Round((time - numOfSecs), 2) * 100);

        string mins, secs, fract;
        if (numOfMinutes.ToString().Length == 1)
            mins = "0" + numOfMinutes.ToString();
        else
            mins = numOfMinutes.ToString();

        if (secsRemainder.ToString().Length == 1)
            secs = "0" + secsRemainder.ToString();
        else
            secs = secsRemainder.ToString();

        if (secFraction.ToString().Length == 1)
            fract = "0" + secFraction.ToString();
        else
            fract = secFraction.ToString();

        return mins + ":" + secs + ":" + fract;
    }

    /// <summary>
    /// Clears the stats in the player prefs and refreshes the ui
    /// </summary>
    public void ClearAllStats()
    {
        foreach (StatType stat in statsData.Stats.StatTypes)
        {
            foreach (Mode mode in statsData.Stats.Modes)
            {
                string key = GenerateStatKey(stat, mode.ModeName);
                if (PlayerPrefs.HasKey(key))
                    PlayerPrefs.DeleteKey(key);
            }
        }
        //Resets the ui to its default values
        SetStatObjects(SelectedDifficulty);
    }
}
