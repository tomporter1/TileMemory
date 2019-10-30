using UnityEngine;

/// <summary>
/// change to use a list insted of being explicit
/// </summary>
public class Pair
{
    Tile tile1;
    Tile tile2;
    public bool pairFound = false;

    public Pair(Tile tile1, Tile tile2)
    {
        this.tile1 = tile1;
        this.tile2 = tile2;
    }

    public Tile Tile1 { get => tile1; }
    public Tile Tile2 { get => tile2; }

    public void SetPairColour(Color newColour)
    {
        tile1.GetComponent<Tile>().SetColour(newColour);
        tile2.GetComponent<Tile>().SetColour(newColour);
    }
}
