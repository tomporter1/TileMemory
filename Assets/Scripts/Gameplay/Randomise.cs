using System.Collections.Generic;
using UnityEngine;

public class Randomise : MonoBehaviour
{
    private static float tolerance = 0.075f;
    private static float ToleranceScale = 1.5f;
    /// <summary>
    /// The system's random reference that is used by this class
    /// </summary>
    private static System.Random rnd = new System.Random();

    /// <summary>
    /// Randomises the order of the tiles and assigns the random colour to each pair. 
    /// Needs to be static so it will not make the same random selection each time
    /// </summary>
    /// <param name="tiles">List of all tiles currently on the board</param>
    /// <param name="pairs">To output the list of pairings of tiles for the game</param>
    public static void ColourTiles(List<Tile> tiles, out List<Pair> pairs)
    {
        pairs = new List<Pair>();
        Color hiddenColour = new Color();
        //randomises the order of the list of the tiles
        for (int i = 0; i < tiles.Count; i++)
        {
            Tile temp = tiles[i];
            int randomIndex = rnd.Next(i, tiles.Count);
            tiles[i] = tiles[randomIndex];
            tiles[randomIndex] = temp;
        }

        //creates the list of tile pairs
        for (int x = 0; x < tiles.Count; x += 2)
            pairs.Add(new Pair(tiles[x], tiles[x + 1]));

        hiddenColour = tiles[0].GetHiddenColour();
        int numOfClashes = 0;
        tolerance = (float)1 / (pairs.Count + (pairs.Count / ToleranceScale));
        Debug.Log("Tolerance: " + tolerance.ToString());
        //assigns a colour to each pair
        List<Color> selectedColours = new List<Color>();
        for (int x = 0; x < pairs.Count; x++)
        {
            bool colourDifferentEnough = false;
            Color newColour = new Color();

            while (!colourDifferentEnough)
            {
                float red, green, blue;
                red = (float)rnd.Next(0, 1000) / 1000;
                green = (float)rnd.Next(0, 1000) / 1000;
                blue = (float)rnd.Next(0, 1000) / 1000;

                newColour = new Color(red, green, blue, 1);
                bool similarRed = false;
                bool similargreen = false;
                bool similarBlue = false;
                bool similarHidden = false;

                foreach (Color color in selectedColours)
                {
                    if (newColour.r > color.r - tolerance && newColour.r < color.r + tolerance)
                        similarRed = true;
                    if (newColour.g > color.g - tolerance && newColour.g < color.g + tolerance)
                        similargreen = true;
                    if (newColour.b > color.b - tolerance && newColour.b < color.b + tolerance)
                        similarBlue = true;
                }

                if (newColour.g > hiddenColour.g - tolerance && newColour.g < hiddenColour.g + tolerance)
                    similarHidden = true;

                //if ((similarRed && similargreen) || (similarRed && similarBlue) || (similarBlue && similargreen))
                if (similarRed ||  similargreen || similarBlue || similarHidden)
                    numOfClashes++;
                else
                    colourDifferentEnough = true;
            }
            selectedColours.Add(newColour);
            pairs[x].SetPairColour(newColour);
        }
        Debug.Log("Number of clashes: " + numOfClashes.ToString());
    }
}