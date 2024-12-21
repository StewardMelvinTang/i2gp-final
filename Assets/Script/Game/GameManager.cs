using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public RecipeManager recipeManager;
    public OrderUiManager orderUiManager;
    // Start is called before the first frame update
    public CustomerManager customerManager;

    [SerializeField] 
    private float currentGameTime;

    // 5 minutes?
    private float totalGameTime = 120f;  
    public TextMeshProUGUI timerText;

    private float realTimeTaken = 0f;

    public int ordersMade = 0;
    public int ordersMissed = 0;
    public int animalsKilled = 0;
    public int score = 0;


    public GameObject endGamePanel;
    public TextMeshProUGUI animalsKilledText; // Text for animals killed
    public TextMeshProUGUI ordersMissedText; // Text for orders missed
    public TextMeshProUGUI ordersMadeText; // Text for orders made
    public TextMeshProUGUI finalScoreText; // Text for final score
    public Image fadeImage; 

    void Start()
    {
        // a little bit ugly here but basically everytime the customerManager spawns a customer, we get the recipe and show in UI 
        currentGameTime = totalGameTime;
        realTimeTaken = 0f;

        customerManager.startSpawningCustomers(recipeManager);
        UpdateTimerUI();
    }

    void Update() {
        if (currentGameTime > 0) {
            currentGameTime -= Time.deltaTime;
        } else {
            Debug.Log("Game is finished");
            EndGame();
        }
        realTimeTaken += Time.deltaTime;
        UpdateTimerUI();
    }


    public void UpdateScore(int value)
    {
        score += value;
    }

    public void IncrementOrdersMade()
    {
        ordersMade++;
    }

    public void IncrementOrdersMissed()
    {
        ordersMissed++;
    }

    public void IncrementAnimalsKilled()
    {
        animalsKilled++;
    }

    public void IncrementGameTime(float seconds)
    {
        currentGameTime += seconds;

        // Ensure the time does not exceed the total game time
        currentGameTime = totalGameTime;
        UpdateTimerUI();
    }

    public float GetCurrentTime() {
        return currentGameTime;
    }
    public float GetTotalGameTime() {
        return totalGameTime;
    }

    public float GetRealTime() {
        return realTimeTaken;
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentGameTime / 60f);
            int seconds = Mathf.FloorToInt(currentGameTime % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    private void EndGame()
    {
        Debug.Log("Game Over!");
        //SceneManager.LoadScene("EndGameScene");
        StartCoroutine(FadeToEndGame());
    }

    public void GetStats(out int made, out int missed, out int killed, out int finalScore)
    {
        made = ordersMade;
        missed = ordersMissed;
        killed = animalsKilled;
        finalScore = score;
    }

    private IEnumerator FadeToEndGame()
    {
        float fadeDuration = 2f; // Duration of the fade
        float elapsedTime = 0f;

        if (fadeImage != null)
        {
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
                fadeImage.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
        }

        // Activate end game panel and update stats
        if (endGamePanel != null)
        {
            endGamePanel.SetActive(true);

            // Update individual stats UI elements
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
    }

    public void SwitchScene(string sceneName)
    {
        // Load the specified scene
        SceneManager.LoadScene(sceneName);
    }


    // void OnCustomerSpawned(Recipe recipe) {
    //     orderUiManager.AddOrder(recipe);
    // }

    // void OnCustomerTimerEnd(bool hasLeft) {
    //     StartCoroutine(RemoveCustomerAfterDelay(customerDestroyDelay));
    // }
}
