using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateTriggerScript : MonoBehaviour
{
    
    public SceneManagerScript sceneManagerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        if (sceneManagerScript == null) {
            sceneManagerScript = GetComponentInParent<SceneManagerScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (sceneManagerScript) {
            // sceneManagerScript.ChangeGameState(sceneManagerScript.currentGameState == GameState.Cooking ? GameState.Hunting : GameState.Cooking);
            sceneManagerScript.ChangeGameState(GameState.Cooking);
        }
    }

    void OnTriggerExit(Collider other) {
        if (sceneManagerScript) {
            sceneManagerScript.ChangeGameState(GameState.Hunting);
            // sceneManagerScript.ChangeGameState(sceneManagerScript.currentGameState == GameState.Cooking ? GameState.Hunting : GameState.Cooking);
        }
    }
}
