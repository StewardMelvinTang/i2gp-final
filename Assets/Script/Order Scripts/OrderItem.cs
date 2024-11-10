using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class OrderItem : MonoBehaviour
{
    private OrderManager orderManager;// Transform for where to place ingredients as text objects
    public Image dishImage;
    public string foodName;

    private void Start()
    {
        // Find OrderManager in the scene
        orderManager = FindObjectOfType<OrderManager>();
    }

    public void OnOrderClicked()
    {
        if (orderManager != null)
        {
            orderManager.ShowOrderDetail(this);
        }
    }

    public void SetOrderDetails(string dishName)
    {
        foodName = dishName;
        // Load the material from Resources based on the dish name
        Material material = Resources.Load<Material>($"Foods_ICONS/{dishName}");
        if (material != null)
        {
            // Assign the loaded material to the dishImage component
            dishImage.material = material;
        }
        else
        {
            Debug.LogWarning("Material not found for dish: " + dishName);
        }
    }
}