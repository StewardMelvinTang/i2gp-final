using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class OrderItem : MonoBehaviour
{
    private OrderManager orderManager;// Transform for where to place ingredients as text objects
    public Image dishImage;

    private void Start()
    {
        // Find OrderManager in the scene
        orderManager = FindObjectOfType<OrderManager>();
    }

    public void OnOrderClicked()
    {
        Debug.Log("anjign fsds");
        if (orderManager != null)
        {
            orderManager.ShowOrderDetail(this);
        }
    }

    public void SetOrderDetails(string dishName)
    {
        // Load the material from Resources based on the dish name
        Material material = Resources.Load<Material>($"Foods_ICONS/{dishName}");
        if (material != null)
        {
            // Assign the loaded material to the dishImage component
            dishImage.material = material;
            Debug.Log("Material successfully assigned for dish: " + dishName);
        }
        else
        {
            Debug.LogWarning("Material not found for dish: " + dishName);
        }
    }
}