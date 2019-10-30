using UnityEngine;
using UnityEngine.Events;

public class HideTiles : MonoBehaviour
{
    private GameManager _gameManager;

    public static UnityEvent onTilesHide = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GetComponentInParent<GameManager>();
        onTilesHide.AddListener(HideAllTiles);
    }

    private void HideAllTiles()
    {
        foreach (Tile tile in _gameManager.GetAllTiles())
        {
            tile.Hide();
        }
    }
}
