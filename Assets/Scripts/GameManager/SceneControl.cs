using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour {

    public int currentScene = -1;

	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CloseCurrentGame()
    {
        if (currentScene > 0)
        {
            SceneManager.UnloadSceneAsync(currentScene);
        }
    }

    public void LoadGame(int index)
    {
        CloseCurrentGame();
        SceneManager.LoadScene(index, LoadSceneMode.Additive);
        currentScene = index;
    }
}
