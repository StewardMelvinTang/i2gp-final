using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class VariableSetter : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI animalsKilledText; // Text for animals killed
    public TextMeshProUGUI ordersMissedText; // Text for orders missed
    public TextMeshProUGUI ordersMadeText; // Text for orders made
    public TextMeshProUGUI finalScoreText; // Text for final score

    void Start()
    {
        int animalsKilled = FindObjectOfType<GameManagerNew>().animalsKilled;
        int ordersMissed = FindObjectOfType<GameManagerNew>().ordersMissed;
        int ordersMade = FindObjectOfType<GameManagerNew>().ordersMade;
        int score = FindObjectOfType<GameManagerNew>().score;

        if (animalsKilledText != null)
        {
            animalsKilledText.text = $"{animalsKilled}";
        }

        if (ordersMissedText != null)
        {
            ordersMissedText.text = $"{ordersMissed}";
        }

        if (ordersMadeText != null)
        {
            ordersMadeText.text = $"{ordersMade}";
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = $"{score}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
