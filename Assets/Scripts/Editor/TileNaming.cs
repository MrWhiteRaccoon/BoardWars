
using UnityEngine;
using UnityEditor;

public class TileNaming : EditorWindow {

    string playerTileName="PlayerPath";
    string enemyTileName="EnemyPath";
    string commonTileName = "CommonPath";

    int playerTile=1;
    int enemyTile=1;
    int commonTile=1;

    [MenuItem("Window/Tile Naming")]
    public static void ShowWindow()
    {
        GetWindow<TileNaming>("Tile Naming");
    }
    private void OnGUI()
    {
        //Window Code
        GUILayout.Label("SET TILES NAMES",EditorStyles.boldLabel);

        playerTileName = EditorGUILayout.TextField("Player Tiles Name", playerTileName);
        enemyTileName = EditorGUILayout.TextField("Enemy Tiles Name", enemyTileName);
        commonTileName = EditorGUILayout.TextField("Common Tiles Name", commonTileName);

        if (GUILayout.Button("Player Tiles"))
        {
            
            Selection.activeGameObject.name = playerTileName + playerTile;
            playerTile++;
            
        }
        if (GUILayout.Button("Enemy Tiles"))
        {
            Selection.activeGameObject.name = enemyTileName + enemyTile;
            enemyTile++;
        }
        if (GUILayout.Button("Common Tiles"))
        {
            Selection.activeGameObject.name = commonTileName + commonTile;
            commonTile++;
        }
        if (GUILayout.Button("RESET ALL"))
        {
            playerTile = enemyTile = commonTile = 1;
        }

    }
}
