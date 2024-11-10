using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;
using TMPro;

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
    public Transform orderHolder;              // Assign the content area of the Scroll View              // For debugging to add random orders

    private List<GameObject> activeOrders = new List<GameObject>();      // List to hold active order 
    public GameObject orderDetailPrefab; 
    private GameObject activeOrderDetail = null;    // Current active detailed view
    private OrderItem lastDeactivatedOrder = null; // Last deactivated order
    private DishLoader dishLoader;

    void Start()
    {
        dishLoader = FindObjectOfType<DishLoader>(); // Initialize the DishLoader reference
        if (dishLoader == null)
        {
            Debug.LogError("DishLoader not found in the scene.");
        }
    }

    public void ShowOrderDetail(OrderItem orderItem)
    {
        if (activeOrderDetail != null)
        {
            CloseOrderDetail();
        }
        activeOrderDetail = Instantiate(orderDetailPrefab, FindObjectOfType<Canvas>().transform);
        lastDeactivatedOrder = orderItem;
        orderItem.gameObject.SetActive(false); 

        TextMeshProUGUI ingredientListText = activeOrderDetail.transform.Find("IngredientListText").GetComponent<TextMeshProUGUI>();
        Image detailImage = activeOrderDetail.transform.Find("DishImage").GetComponent<Image>();

        if (ingredientListText != null && dishLoader != null)
        {
            if (dishLoader.dishes.TryGetValue(orderItem.foodName, out var ingredients))
            {
                string formattedIngredients = "Ingredients:\n- " + string.Join("\n- ", ingredients);
                ingredientListText.text = formattedIngredients;
            }
            else
            {
                Debug.Log("ASUU anjing cok");
                ingredientListText.text = "Ingredients not found.";
            }
        }

        if (detailImage != null)
        {
            detailImage.material = orderItem.dishImage.material; // Use the sprite already set in OrderItem
        }

        Button closeButton = activeOrderDetail.GetComponentInChildren<Button>();
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseOrderDetail);
        }
    }

    public void CloseOrderDetail()
    {
        if (activeOrderDetail != null)
        {
            Destroy(activeOrderDetail);
            activeOrderDetail = null;
            if (lastDeactivatedOrder != null)
            {
                lastDeactivatedOrder.gameObject.SetActive(true);
                lastDeactivatedOrder = null;
            }
        }
    }

    public void CreateRandomOrder()
    {
        MakeOrder("null");
    }
    public void MakeOrder(string dishName)
    {
        GameObject newOrder = Instantiate(orderPrefab, orderHolder);
        OrderItem orderItem = newOrder.GetComponent<OrderItem>();

        string imagePath =  ($"Foods_ICONS/{dishName}");
        orderItem.SetOrderDetails(dishName);

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