using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TileSelect : UnityEvent<Tile> { }

public class SelectionManager : MonoBehaviour
{
    public static TileSelect onTileSelect = new TileSelect();

    [SerializeField, Range(0f, 1f)]
    private float _showWrongSelectionSecs;

    private static GameManager _gameManager;
    private int numOfPairsFound = 0;
    private Tile currentlySelected;

    void Start()
    {
        onTileSelect.AddListener(TileSelected);
        if (_gameManager == null)
            _gameManager = GetComponentInParent<GameManager>();

        GameManager.onGameReset.AddListener(Reset);
    }

    private void Reset()
    {
        numOfPairsFound = 0;
        currentlySelected = null;
    }

    private void TileSelected(Tile newTile)
    {
        //no other tile is selected
        if (currentlySelected == null)
        {
            currentlySelected = newTile;
            currentlySelected.hidingSoon = false;
        }
        //there is already a tile selected
        else
        {
            Pair newTilePair = _gameManager.GetPair(newTile);

            //correct pair selected
            if (currentlySelected == newTilePair.Tile1 || currentlySelected == newTilePair.Tile2)
            {
                newTilePair.pairFound = true;
                numOfPairsFound++;
            }
            //incorrect pair selected 
            else
            {
                StatRecorder.onIncorrectSelection.Invoke();
                StartCoroutine(HideTilesInSecs(_showWrongSelectionSecs, newTile, currentlySelected));
            }
            currentlySelected = null;
        }

        if (numOfPairsFound == _gameManager.TilePairs.Count)
        {
            GameManager.onGameEnd.Invoke();
            Debug.Log("All pairs found!!");
        }
    }

    private IEnumerator HideTilesInSecs(float _secs, Tile tile1, Tile tile2)
    {
        tile1.hidingSoon = true;
        tile2.hidingSoon = true;
        //wait 2 secs 
        yield return new WaitForSeconds(_secs);

        //hide both tiles
        if (tile2.hidingSoon)
            tile1.Hide();
        if (tile2.hidingSoon)
            tile2.Hide();
    }
}
