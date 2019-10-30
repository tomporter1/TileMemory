using System;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySettingsData : MonoBehaviour
{
    public DifficultySettings Difficulties { get; private set; }

    public DifficultySettingsData()
    {
        TextAsset fileAsString = Resources.Load("DifficultySettings") as TextAsset;
        Difficulties = JsonUtility.FromJson<DifficultySettings>(fileAsString.ToString());
    }

    public DifficultyInfo GetDifficultyInfo(Difficulty _difficulty)
    {
        foreach (DifficultyInfo info in Difficulties.Difficulty)
        {
            if (info.Name == _difficulty)
                return info;
        }
        return null;
    }

    public void UpdateCustomSettings(int newX, int newY)
    {
        Difficulties.CustomPlayerPref.UpdateRow(newX);
        Difficulties.CustomPlayerPref.UpdateCol(newY);
    }
}

[Serializable]
public class DifficultySettings
{
    [SerializeField]
    private List<DifficultyInfo> difficulty;
    [SerializeField]
    private Customplayerpref customPlayerPref;

    public List<DifficultyInfo> Difficulty { get { return difficulty; } }
    public Customplayerpref CustomPlayerPref { get { return customPlayerPref; } }
}

[Serializable]
public class DifficultyInfo
{
    [SerializeField]
    private string name;
    [SerializeField]
    private int xSize, ySize, showTime;

    public Difficulty Name { get { return (Difficulty)Enum.Parse(typeof(Difficulty), name); } }
    public int XSize { get { return xSize; } }
    public int YSize { get { return ySize; } }
    public int ShowTime { get { return showTime; } }
}

[Serializable]
public class Customplayerpref
{
    [SerializeField]
    private CustomSetting row, col;

    public CustomSetting Row { get { return row; } }
    public CustomSetting Col { get { return col; } }

    public void UpdateRow(int newRow)
    {
        PlayerPrefs.SetInt(row.Name, newRow);
    }

    public void UpdateCol(int newCol)
    {
        PlayerPrefs.SetInt(Col.Name, newCol);
    }
}

[Serializable]
public class CustomSetting
{
    [SerializeField]
    private string name;
    [SerializeField]
    private int initialValue;

    public string Name { get { return name; } }
    public int InitialValue { get { return initialValue; } }
}