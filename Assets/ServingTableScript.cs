using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingTableScript : Table
{
    // private Dictionary<string, HashSet<string>> dishes;
    private OrderManager orderManager;
    private CustomerManager customerManager;
    private DishLoader dishLoader;
    private ScoreManager scoreManager;


    // Initialize the dictionary with possible dishes and ingredients
    void Start()
    {
        // dishes = new Dictionary<string, HashSet<string>>()
        // {
        //     { "hamBurger", new HashSet<string> { "Plate", "BottomBun", "CookedMeat", "TopBun" } },
        //     { "hamBurgerWithCarrot", new HashSet<string> { "Plate", "BottomBun", "CookedMeat", "TopBun", "ChoppedCarrot" } }
        // };
        orderManager = FindObjectOfType<OrderManager>();
        customerManager = FindObjectOfType<CustomerManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        dishLoader = FindObjectOfType<DishLoader>(); // Initialize the DishLoader reference
        if (dishLoader == null)
        {
            Debug.LogError("DishLoader not found in the scene.");
        }

        if (orderManager != null)
        {
            // foreach (var dish in dishLoader.dishes)
            // {
            //     orderManager.MakeOrder(dish.Key);
            //     Debug.Log(dish.Key);
            // }
            // orderManager.MakeOrder("hamBurger", dishes["hamBurger"]);
            // orderManager.MakeOrder("hamBurgerWithCarrot", dishes["hamBurgerWithCarrot"]);
            //orderManager.MakeOrder("hamBurger");
            //orderManager.MakeOrder("hamBurgerWithCarrot");
        }
        else{
            Debug.LogError("Order Manager not found");
        }
    }

    public override GameObject PutItem(GameObject gameObject)
    {
        /* Empty Hand, Take the food */
        if (gameObject == null)
        {
            // if (foodObject == null)
            // {
            //     return null;
            // }
            // GameObject ret = foodObject;
            // foodObject = null;
            // return ret;
            return null;
        }
    
        StackFood stackFood = gameObject.GetComponent<StackFood>();
        if (!stackFood) return gameObject;

        // Get tags of all items in the stack
        HashSet<string> stackContents = new HashSet<string>();
        foreach (GameObject food in stackFood.GetFoodStack())
        {
            String name = food.GetComponent<Item>().itemName;
            stackContents.Add(name); 
            Debug.Log("Stack contains: " + food.tag);
        }

        // Check if the stack contents match any dish in the dictionary
        string matchedDish = null;
        foreach (var dish in dishLoader.dishes) //dishes)
        {
            if (stackContents.SetEquals(dish.Value))
            {
                matchedDish = dish.Key;
                break;
            }
        }

        // Output the result
        if (matchedDish != null)
        {
            OrderRemoval(matchedDish);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("No matching dish found.");
            return gameObject;
        }

        return null;
    }

    private void OrderRemoval(string matchedDish)
    {
        int orderId = orderManager.GetOrderId(matchedDish);
        orderManager.RemoveOrder(orderId); 

        Customer customerToRemove = FindCustomerByOrderId(orderId);
        if (customerToRemove != null)
        {
            customerManager.RemoveCustomer(customerToRemove.gameObject);
        }
        scoreManager.AddScore(100);
    }

    private Customer FindCustomerByOrderId(int orderId)
    {
        Customer[] customers = FindObjectsOfType<Customer>();
        foreach (Customer customer in customers)
        {
            if (customer.OrderId == orderId)
            {
                return customer;
            }
        }
        return null;
    }
}