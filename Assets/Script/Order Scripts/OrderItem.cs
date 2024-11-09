using UnityEngine;
using UnityEngine.UI;

public class OrderItem : MonoBehaviour
{
    private OrderManager orderManager;

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
}