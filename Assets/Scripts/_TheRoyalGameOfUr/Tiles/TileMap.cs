using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour {

    // The public Lists store the hole path, to move the player I just have to move him 
    // throuhg the path, which is REALY helpful. GJ men!

    public List<Tile> playerPath = new List<Tile>();
    public List<Tile> enemyPath = new List<Tile>();

    private List<Tile> playerTiles = new List<Tile>();
    private List<Tile> enemyTiles = new List<Tile>();

    private Tile[] tiles;

    private void Start()
    {
        FindPaths();
    }

    //TEST ONLY--------------
    //----------------------
    private void Update()
    {
        for (int i = 0; i < enemyPath.Count-1; i++)
        {
            Debug.DrawLine(enemyPath[i].transform.position, enemyPath[i + 1].transform.position, Color.red);
            Debug.DrawLine(playerPath[i].transform.position, playerPath[i + 1].transform.position, Color.red);

        }
    }

    public void ClearLists()
    {
        playerPath.Clear();
        enemyPath.Clear();
        playerTiles.Clear();
        enemyTiles.Clear();
    }

    public void SortTiles()
    {
        tiles = FindObjectsOfType<Tile>();
        foreach (Tile t in tiles)
        {
            if (t.CompareTag("PlayerTile"))
            {
                playerTiles.Add(t);
            }
            else if (t.CompareTag("EnemyTile"))
            {
                enemyTiles.Add(t);
            }
            else
            {
                playerTiles.Add(t);
                enemyTiles.Add(t);
            }
        }
        Debug.Log(enemyTiles.Count);
    }

    public void FindPaths()
    {
        ClearLists();
        SortTiles();
        playerPath.Add(playerTiles.Find(t=>t.isFirstTile));
        enemyPath.Add(enemyTiles.Find(t => t.isFirstTile));

        playerTiles.Remove(playerPath[0]);
        enemyTiles.Remove(enemyPath[0]);
        for (int i = 0; i < playerTiles.Count; i++)
        {
            if (playerPath[i].isLastTile)
            {
                Debug.LogError("LastReached");
                break;
            }
            if (playerPath[i].isCross)
            {
                playerPath.Add(playerPath[i].nextWalkables[0]);
            }
            else
            {
                playerPath.Add(playerPath[i].nextWalkable);
            }
            
        }
        for (int i = 0; i < enemyTiles.Count; i++)
        {
            if (enemyPath[i].isLastTile)
            {
                Debug.LogError("LastReached");
                break;
            }
            if (enemyPath[i].isCross)
            {
                enemyPath.Add(enemyPath[i].nextWalkables[1]);
            }
            else
            {
                enemyPath.Add(enemyPath[i].nextWalkable);
            }

        }
    }
}
