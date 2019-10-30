using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class InitialiseValues : MonoBehaviour
{
    [SerializeField]
    private Setting settingType;

    private enum Setting
    {
        row,
        Column
    }

    // Start is called before the first frame update
    void Awake()
    {
        DifficultySettingsData difficultySettingsData = new DifficultySettingsData();

        if (settingType == Setting.Column)
            GetComponent<Slider>().value = PlayerPrefs.GetInt(difficultySettingsData.Difficulties.CustomPlayerPref.Col.Name);
        else if (settingType == Setting.row)
            GetComponent<Slider>().value = PlayerPrefs.GetInt(difficultySettingsData.Difficulties.CustomPlayerPref.Row.Name);
    }
}
