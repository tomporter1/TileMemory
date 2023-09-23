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
    private TextMeshProUGUI _modeLabel;

    private StatsData statsData;
    public Difficulty SelectedDifficulty = Difficulty.Easy;

    void Start()
    {
        statsData = new StatsData();
        gameObject.SetActive(false);
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

        //populates the panel with new stat data
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
        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        int milliseconds = (int)(100 * (time - minutes * 60 - seconds));
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
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
