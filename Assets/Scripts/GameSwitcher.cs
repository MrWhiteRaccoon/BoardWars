using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSwitcher : MonoBehaviour {

    public GameObject gameSelectionMenu;

    SceneControl scene;

    private void Start()
    {
        scene = GetComponent<SceneControl>();
    }

    public void EndCurrentGame()
    {
        scene.CloseCurrentGame();
        gameSelectionMenu.SetActive(true);
        Debug.Log("Endgame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
