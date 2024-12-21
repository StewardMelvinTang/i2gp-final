using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    void Start()
    {
        // a little bit ugly here but basically everytime the customerManager spawns a customer, we get the recipe and show in UI 
        currentGameTime = totalGameTime;

        customerManager.startSpawningCustomers(recipeManager);
    }

    void Update() {
        if (currentGameTime > 0) {
            currentGameTime -= Time.deltaTime;
        } else {
            Debug.Log("Game is finished");
        }
    }



    public float GetCurrentTime() {
        return currentGameTime;
    }
    public float GetTotalGameTime() {
        return totalGameTime;
    }

    // void OnCustomerSpawned(Recipe recipe) {
    //     orderUiManager.AddOrder(recipe);
    // }

    // void OnCustomerTimerEnd(bool hasLeft) {
    //     StartCoroutine(RemoveCustomerAfterDelay(customerDestroyDelay));
    // }
}
