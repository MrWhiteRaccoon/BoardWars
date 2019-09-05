using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 TILE ORDER:
    0:UP
    1:RIGHT
    2:DOWN
    3:LEFT
    KEEP THE ORDER CLEAR AND EVERYTHING WILL BE NICE
*/
public class Tile : MonoBehaviour {

    public Tile[] surroundingTiles;
    public int index;
    public bool isFirstTile = false;
    public bool isLastTile = false;
    public bool isCross = false;
    public bool doubleTurn = false;
    public bool safe = false;
    public string testNextTile = "";

    [HideInInspector]
    public Tile nextWalkable;
    [HideInInspector]
    public Tile[] nextWalkables=null;

    public Token tokenOnTop=null;

    private void Awake()
    {
        if (!isLastTile)
        {
            FindNextTile();
        }

        else if (!isCross&&!isLastTile)
        {
            testNextTile = nextWalkable.name;
        }
    }

    private void FindNextTile()
    {
        if (gameObject.CompareTag("PlayerTile"))
        {
            for (int i = 0; i < surroundingTiles.Length; i++)
            {
                if (surroundingTiles[i] == null||surroundingTiles[i].gameObject.CompareTag("EnemyTile"))
                {
                    continue;
                }
                else if(surroundingTiles[i].CompareTag("PlayerTile")&& surroundingTiles[i].index>index)
                {
                    nextWalkable = surroundingTiles[i];
                    break;
                }
                else if (surroundingTiles[i].CompareTag("CommonTile"))
                {
                    nextWalkable = surroundingTiles[i];
                }
            }
        }
        else if (gameObject.CompareTag("CommonTile"))
        {
            nextWalkables = new Tile[2];
            for (int i = 0; i < surroundingTiles.Length; i++)
            {
                if (surroundingTiles[i] == null)
                {
                    continue;
                }
                else if (surroundingTiles[i].CompareTag("CommonTile") && surroundingTiles[i].index > index)
                {
                    nextWalkable = surroundingTiles[i];
                    nextWalkables[0]=null;
                    nextWalkables[1] = null;
                    isCross = false;
                    break;
                }
                else if (surroundingTiles[i].CompareTag("PlayerTile"))
                {
                    nextWalkables[0] = surroundingTiles[i];
                    isCross = true;
                }
                else if (surroundingTiles[i].CompareTag("EnemyTile"))
                {
                    nextWalkables[1] = surroundingTiles[i];
                    isCross = true;
                }
            }
        }
        else if (gameObject.CompareTag("EnemyTile"))
        {
            for (int i = 0; i < surroundingTiles.Length; i++)
            {
                if (surroundingTiles[i] == null || surroundingTiles[i].gameObject.CompareTag("PlayerTile"))
                {
                    continue;
                }
                else if (surroundingTiles[i].CompareTag("EnemyTile") && surroundingTiles[i].index > index)
                {
                    nextWalkable = surroundingTiles[i];
                    break;
                }
                else if (surroundingTiles[i].CompareTag("CommonTile"))
                {
                    nextWalkable = surroundingTiles[i];
                }
            }
        }
        else
        {
            Debug.LogError(gameObject.name + " UNTAGGED");
        }
    }
}
