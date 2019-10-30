using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject Board, SquarePrefab, Canvas;

    internal class GridArgs
    {
        internal int xSize;
        internal int ySize;

        public GridArgs(int xSize, int ySize)
        {
            this.xSize = xSize;
            this.ySize = ySize;
        }
    }

    internal void Create(GridArgs BoardSize)
    {
        List<Tile> tiles = new List<Tile>();

        float boardWidth = Board.GetComponent<RectTransform>().rect.width;
        float boardHight = Board.GetComponent<RectTransform>().rect.height;

        float squareWidth = boardWidth / BoardSize.xSize;
        float squareHight = boardHight / BoardSize.ySize;
        Board.GetComponent<GridLayoutGroup>().cellSize = new Vector2(squareWidth, squareHight);

        for (int y = 0; y < BoardSize.ySize; y++)
        {
            for (int x = 0; x < BoardSize.xSize; x++)
            {
                GameObject newTile = Instantiate(SquarePrefab, Board.GetComponent<Transform>());
                newTile.name = "Tile: (" + x.ToString() + ", " + y.ToString() + ")";
                newTile.GetComponent<Coord>().SetValue(x, y);

                tiles.Add(newTile.GetComponent<Tile>());
            }
        }

        GetComponent<GameManager>().SetTiles(tiles);
    }
}