using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class GameManagerNew : MonoBehaviour
{
    
    public static GameManagerNew Instance { get; private set; }

    public int ordersMade = 0;
    public int ordersMissed = 0;
    public int animalsKilled = 0;
    public int score = 0;

    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make this GameObject persistent
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    public void ChangingScene(int a, int b, int c, int d){
        ordersMade = a;
        ordersMissed = b;
        animalsKilled = c;
        score = d;

        SceneManager.LoadScene("EndGame");
    }

}