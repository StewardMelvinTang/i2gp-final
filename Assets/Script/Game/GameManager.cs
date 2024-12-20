using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RecipeManager recipeManager;
    public OrderUiManager orderUiManager;
    // Start is called before the first frame update
    public CustomerManager customerManager;
    void Start()
    {
        // a little bit ugly here but basically everytime the customerManager spawns a customer, we get the recipe and show in UI 
        customerManager.startSpawningCustomers(recipeManager, OnCustomerSpawned);
    }

    void OnCustomerSpawned(Recipe recipe) {
        orderUiManager.AddOrder(recipe);
    }
}
