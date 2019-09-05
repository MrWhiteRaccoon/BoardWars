using UnityEngine;
using UnityEditor;

public class TileRandomRotator : EditorWindow
{

    [MenuItem("Window/Tile Rotator")]
    public static void ShowWindow()
    {
        GetWindow<TileRandomRotator>("Tile Rotator");
    }
    private void OnGUI()
    {
        GUILayout.Label("ROTATES SELECTED OBJECTS RANDOMLY", EditorStyles.boldLabel);

        if (GUILayout.Button("ROTATE!"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                obj.transform.rotation = Quaternion.Euler(RandomRotate());
            }
        }
    }

    Vector3 RandomRotate()
    {
        Vector3 rotation = new Vector3(Mathf.Floor(Random.Range(-1,2)),
            Mathf.Floor(Random.Range(-1, 2)),
            Mathf.Floor(Random.Range(-1, 2)));
        rotation = rotation * 90;
        return rotation;
    }
}
