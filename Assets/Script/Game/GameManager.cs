using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public RecipeManager recipeManager;
    public OrderUiManager orderUiManager;
    // Start is called before the first frame update
    public CustomerManager customerManager;

    [SerializeField] 
    private float currentGameTime;

    // 5 minutes?
    private float totalGameTime = 60f;  
    public TextMeshProUGUI timerText;

    private float realTimeTaken = 0f;


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
        }
        realTimeTaken += Time.deltaTime;
        UpdateTimerUI();
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

    // void OnCustomerSpawned(Recipe recipe) {
    //     orderUiManager.AddOrder(recipe);
    // }

    // void OnCustomerTimerEnd(bool hasLeft) {
    //     StartCoroutine(RemoveCustomerAfterDelay(customerDestroyDelay));
    // }
}
