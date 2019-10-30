using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsData : MonoBehaviour
{
    public StatsInfo Stats { get; private set; }

    public StatsData()
    {
        TextAsset fileAsString = Resources.Load("Stats") as TextAsset;
        Stats = JsonUtility.FromJson<StatsInfo>(fileAsString.ToString());
    }
}

[Serializable]
public class StatsInfo
{
    [SerializeField]
    private List<StatType> statTypes;
    [SerializeField]
    private List<Mode> modes;
    [SerializeField]
    private List<StatDefaults> statTypeDefaults;

    public List<StatType> StatTypes { get { return statTypes; } }
    public List<Mode> Modes { get { return modes; } }
    public List<StatDefaults> StatTypeDefaults { get { return statTypeDefaults; } }

    public StatDefaults GetStatDefualt(statTypesEnum type)
    {
        foreach (StatDefaults defualtInfo in statTypeDefaults)
            if (type == defualtInfo.Type)
                return defualtInfo;
        return null;
    }

    public StatType GetStatInfo(statNames name)
    {
        foreach (StatType stat in statTypes)
            if (stat.Name == name)
                return stat;
        return null;
    }
}

[Serializable]
public class StatType
{
    [SerializeField]
    private string name, label, playerPrefName, dataType;

    public statNames Name { get { return (statNames)Enum.Parse(typeof(statNames), name); } }
    public string Label { get { return label; } }
    public string PlayerPrefName { get { return playerPrefName; } }
    public statTypesEnum DataType { get { return (statTypesEnum)Enum.Parse(typeof(statTypesEnum), dataType); } }
}

[Serializable]
public class Mode
{
    [SerializeField]
    private string modeName, playerPrefSuffix;

    public Difficulty ModeName { get { return (Difficulty)Enum.Parse(typeof(Difficulty), modeName); } }
    public string PlayerPrefSuffix { get { return playerPrefSuffix; } }
}

[Serializable]
public class StatDefaults
{
    [SerializeField]
    private string type;
    [SerializeField]
    private int value;

    public statTypesEnum Type { get { return (statTypesEnum)Enum.Parse(typeof(statTypesEnum), type); } }
    public int Value { get { return value; } }
}
