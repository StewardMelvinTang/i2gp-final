using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuScript : MonoBehaviour {

    [SerializeField]
    private string gamelevelname;
    
    public void ExitGameButton() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void StartGameButton() {
        if (gamelevelname == "") return;
        SceneManager.LoadScene(gamelevelname);
    }
}
