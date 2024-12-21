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
    private float totalGameTime = 300f;  
    public TextMeshProUGUI timerText;


    void Start()
    {
        // a little bit ugly here but basically everytime the customerManager spawns a customer, we get the recipe and show in UI 
        currentGameTime = totalGameTime;

        customerManager.startSpawningCustomers(recipeManager);
        UpdateTimerUI();
    }

    void Update() {
        if (currentGameTime > 0) {
            currentGameTime -= Time.deltaTime;
        } else {
            Debug.Log("Game is finished");
        }
        UpdateTimerUI();
    }

    public void IncrementGameTime(float seconds)
    {
        currentGameTime += seconds;

        // Ensure the time does not exceed the total game time
        if (currentGameTime > totalGameTime)
        {
            currentGameTime = totalGameTime;
        }
        UpdateTimerUI();
    }

    public float GetCurrentTime() {
        return currentGameTime;
    }
    public float GetTotalGameTime() {
        return totalGameTime;
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
