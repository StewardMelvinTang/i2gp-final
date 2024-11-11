using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int AssignedPositionIndex { get; set; }
    public int OrderId { get; private set; }
    private bool orderPlaced = false;
    private OrderManager orderManager;

    void Start()
    {
        orderManager = FindObjectOfType<OrderManager>();
        if (orderManager == null)
        {
            Debug.LogError("OrderManager not found in the scene.");
        }
    
    }

    public void MoveTo(Vector3 targetPosition)
    {
        StartCoroutine(MoveTowards(targetPosition));
    }

    private IEnumerator MoveTowards(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        if (!orderPlaced)
        {
            PlaceOrder();
            orderPlaced = true;
        }
    }

    private void PlaceOrder()
    {
        if (orderManager != null)
        {
            OrderId = orderManager.MakeOrder("hamBurger");
        }
    }
}