using UnityEngine;

public class StatModeSelection : MonoBehaviour
{
    [SerializeField]
    private Difficulty _selectedMode;

    public void UpdateStats()
    {
        GetComponentInParent<StatManager>().SetStatObjects(_selectedMode);
    }
}
