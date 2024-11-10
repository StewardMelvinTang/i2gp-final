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
    public Transform orderHolder;              // Assign the content area of the Scroll View              // For debugging to add random orders

    private List<GameObject> activeOrders = new List<GameObject>();      // List to hold active order 
    public GameObject orderDetailPrefab; 
    private GameObject activeOrderDetail = null;    // Current active detailed view
    private OrderItem lastDeactivatedOrder = null; // Last deactivated order

    void Start()
    {// Initialize the active orders list                   // Load ingredients data from JSON
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
        Debug.Log("Setting order details for dish: " + dishName);
        orderItem.SetOrderDetails(dishName);

        activeOrders.Add(newOrder);
        Debug.Log("Order successfully created and added to active orders.");
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