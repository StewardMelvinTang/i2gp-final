using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;

public class FoodData
{
    public string name;
    public List<string> baseIngredients;
    public List<string> specialIngredients;
}

public class Foods
{
    public List<FoodData> foods;
}

public class IngredientData
{
    public List<string> common;
    public List<string> herbs_spices;
    public List<string> exotic_special;
}

public class Ingredients
{
    public IngredientData ingredients;
}

public class OrderManager : MonoBehaviour
{
    public GameObject orderPrefab;             // Assign your order UI prefab
    public Transform orderHolder;              // Assign the content area of the Scroll View
    public Button addOrderButton;              // For debugging to add random orders

    private List<GameObject> activeOrders;     // List to hold active orders
    public List<FoodData> foodList;            // List of foods loaded from JSON
    public IngredientData ingredientData;      // Ingredients loaded from JSON

    void Start()
    {
        activeOrders = new List<GameObject>(); // Initialize the active orders list
        LoadFoods();                           // Load food data from JSON
        LoadIngredients();                     // Load ingredients data from JSON

        // Attach listener to addOrderButton for debugging purposes
        if (addOrderButton != null)
            addOrderButton.onClick.AddListener(CreateRandomOrder);
    }

    void LoadFoods()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Foods_JSON/foods.json");
        string json = File.ReadAllText(path);
        Foods foodsData = JsonConvert.DeserializeObject<Foods>(json);
        foodList = foodsData.foods;
    }

    void LoadIngredients()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Foods_JSON/ingredients.json");
        string json = File.ReadAllText(path);
        Ingredients ingredientsData = JsonConvert.DeserializeObject<Ingredients>(json);
        ingredientData = ingredientsData.ingredients;
    }


    public void CreateRandomOrder()
    {
        // Simply instantiate a blank order
        MakeOrder();
    }
    public void MakeOrder()
    {
        GameObject newOrder = Instantiate(orderPrefab, orderHolder);
        activeOrders.Add(newOrder);
    }

    public void ClearOrders()
    {
        // Optionally clear all active orders
        foreach (GameObject order in activeOrders)
        {
            Destroy(order);
        }
        activeOrders.Clear();
    }
}