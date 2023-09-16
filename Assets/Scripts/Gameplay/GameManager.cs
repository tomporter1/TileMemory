using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStart : UnityEvent<DifficultyInfo> { }

public enum Difficulty
{
    Easy,
    Medium,
    Hard,
    Custom
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _board;
    private List<Tile> allTiles;

    public static UnityEvent onGameReset = new UnityEvent();
    public static GameStart onGameStart = new GameStart();
    public static UnityEvent onGameEnd = new UnityEvent();

    internal List<Pair> TilePairs = new List<Pair>();

    private DifficultyInfo currentDifficulty = new DifficultyInfo();
    public static DifficultySettingsData difficultySettingsData;

    private Customplayerpref customSettingInfo;

    public static PanelManager _panelManager;
    private static PanelManager panelManager
    {
        get
        {
            if (_panelManager == null)
                _panelManager = FindObjectOfType<PanelManager>();
            return _panelManager;
        }
    }

    void Start()
    {
        Countdown.onCountdownEnd.AddListener(EnableBoard);
        onGameReset.AddListener(Reset);
        onGameStart.AddListener(StartGame);
        difficultySettingsData = new DifficultySettingsData();
        InitialiseStatPlayerPrefs();
        onGameReset.Invoke();

        customSettingInfo = difficultySettingsData.Difficulties.CustomPlayerPref;
        if (!PlayerPrefs.HasKey(customSettingInfo.Col.Name))
        {
            PlayerPrefs.SetInt(customSettingInfo.Col.Name, customSettingInfo.Col.InitialValue);
        }
        if (!PlayerPrefs.HasKey(customSettingInfo.Row.Name))
        {
            PlayerPrefs.SetInt(customSettingInfo.Row.Name, customSettingInfo.Row.InitialValue);
        }

        Difficulty currentGameDifficulty = GameDifficulty.instance.GetCurrentGameDifficulty();
        onGameStart.Invoke(GetDifficultyInfo(currentGameDifficulty));
    }

    public void StartGame(DifficultyInfo _difficulty)
    {
        currentDifficulty = _difficulty;
        if (!(currentDifficulty.Name == Difficulty.Custom))
            GetComponent<MakeGrid>().Create(new MakeGrid.GridArgs(_difficulty.XSize, _difficulty.YSize));
        else
        {
            int numOfRows = PlayerPrefs.GetInt(difficultySettingsData.Difficulties.CustomPlayerPref.Row.Name);
            int numOfCols = PlayerPrefs.GetInt(difficultySettingsData.Difficulties.CustomPlayerPref.Col.Name);
            GetComponent<MakeGrid>().Create(new MakeGrid.GridArgs(numOfRows, numOfCols));
        }
        Randomise.ColourTiles(allTiles, out TilePairs);
        StartCoroutine(GetComponentInChildren<Countdown>(true).StartCountdown());
    }

    internal Pair GetPair(Tile newTile)
    {
        foreach (Pair pair in TilePairs)
        {
            if (newTile == pair.Tile1 || newTile == pair.Tile2)
                return pair;
        }
        return null;
    }

    private void Reset()
    {
        allTiles = new List<Tile>();

        foreach (Transform tile in _board.transform)
        {
            Destroy(tile.gameObject);
        }
    }

    internal DifficultyInfo GetDifficultyInfo(Difficulty difficulty)
    {
        return difficultySettingsData.GetDifficultyInfo(difficulty);
    }

    internal void SetTiles(List<Tile> newTiles)
    {
        allTiles = newTiles;
    }

    private void EnableBoard()
    {
        _board.GetComponent<ShowBoard>().ShowBoardForSecs(currentDifficulty.ShowTime);
    }

    internal IEnumerable<Tile> GetAllTiles()
    {
        return allTiles;
    }

    internal Difficulty GetCurrentGameDifficulty()
    {
        return currentDifficulty.Name;
    }

    private void InitialiseStatPlayerPrefs()
    {
        StatsData statsData = new StatsData();

        foreach (StatType stat in statsData.Stats.StatTypes)
        {
            foreach (Mode mode in statsData.Stats.Modes)
            {
                string key = StatManager.GenerateStatKey(stat, mode.ModeName);
                if (!PlayerPrefs.HasKey(key))
                    if (stat.DataType == statTypesEnum.Int)
                    {
                        PlayerPrefs.SetInt(key, statsData.Stats.GetStatDefualt(statTypesEnum.Int).Value);
                    }
                    else if (stat.DataType == statTypesEnum.Float)
                    {
                        PlayerPrefs.SetFloat(key, statsData.Stats.GetStatDefualt(statTypesEnum.Float).Value);
                    }
            }
        }
    }
}
