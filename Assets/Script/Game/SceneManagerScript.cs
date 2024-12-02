using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public enum GameState {
    Cooking,
    Hunting
}



public class SceneManagerScript : MonoBehaviour {

    public GameState currentGameState = GameState.Cooking;
    [SerializeField]private TMP_Text Debug_GameStateText = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeGameState(GameState newGameState) {
        currentGameState = newGameState;
        switch (currentGameState) {
            case GameState.Cooking:
                Debug_GameStateText.SetText("Game State: Cooking");
                break;
            case GameState.Hunting:
                Debug_GameStateText.SetText("Game State: Hunting");
                break;
        }
    }
}
