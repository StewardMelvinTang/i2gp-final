using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ServingTableScript : Table
{
    private GameManager gameManager; // can get other components from gameManager

    void Start() {
        base.Start();

        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null) {
            Debug.LogError("Game manager not found");
        }
    }

    public override GameObject PutItem(GameObject gameObject)
    {
        /* Empty Hand, Take the food */
        if (gameObject == null)
        {
            return null;
        }
    
        StackFood stackFood = gameObject.GetComponent<StackFood>();

        if (!stackFood) return gameObject;

        List<GameObject> customerList = gameManager.customerManager.GetCustomerList();

        List<Recipe> recipeList = gameManager.customerManager.GetRecipesList();

        int cust_idx = 0;
        bool isRecipeMatch = false;
        Debug.Log("sTARTING");
        for (cust_idx = 0; cust_idx < customerList.Count; ++cust_idx) {
            Recipe customerOrder = recipeList[cust_idx];

            
            int i = 0;
            if (stackFood.GetFoodStack().Count != customerOrder.ingredients.Count) {
                continue;
            }

            isRecipeMatch = true;
            foreach (GameObject food in stackFood.GetFoodStack())
            {   
                
                // stack gets element from top of the list
                String foodStackName = food.GetComponent<Item>().itemName;
                Debug.Log(foodStackName);
                Debug.Log(customerOrder.ingredients[i].ingredientName);
                Debug.Log("=====");
                // ingredients from left to right
                if (customerOrder.ingredients[i].ingredientName != foodStackName) {
                    isRecipeMatch = false;
                    break;
                }
                ++i;
            }
            if (isRecipeMatch) {
                break;
            }
            Debug.Log("ending");
        }

        if (isRecipeMatch) {
            // remove kth Order and recipe (kth custoemr)
            gameManager.customerManager.RemoveCustomerFromListByIndex(cust_idx);
            // gameManager.customerManager.FulfilOrderByIndex(cust_idx);
            // remove UI
            gameManager.orderUiManager.RemoveOrderFromListByIndex(cust_idx);
            // destroy gameobject
            Destroy(gameObject);
        } else {
            // no matching dish found
            return gameObject;
        }
        return null;

        // // Check if the stack contents match any dish in the dictionary
        // string matchedDish = null;
        // // Get recipe from customerManaegr ?? 

        // foreach (var dish in dishLoader.dishes) //dishes)
        // {
        //     if (stackContents.SetEquals(dish.Value))
        //     {
        //         matchedDish = dish.Key;
        //         break;
        //     }
        // }

        // // Output the result
        // if (matchedDish != null)
        // {
        //     OrderRemoval(matchedDish);
        //     Destroy(gameObject);
        // }
        // else
        // {
        //     Debug.Log("No matching dish found.");
        //     return gameObject;
        // }

        // return null;
    }

    // private void OrderRemoval(string matchedDish)
    // {
    //     int orderId = orderManager.GetOrderId(matchedDish);
    //     orderManager.RemoveOrder(orderId); 

    //     Customer customerToRemove = FindCustomerByOrderId(orderId);
    //     if (customerToRemove != null)
    //     {
    //         customerManager.RemoveCustomer(customerToRemove.gameObject);
    //     }
    //     scoreManager.AddScore(100);
    // }

//     // private Customer FindCustomerByOrderId(int orderId)
//     // {
//     //     Customer[] customers = FindObjectsOfType<Customer>();
//     //     foreach (Customer customer in customers)
//     //     {
//     //         if (customer.OrderId == orderId)
//     //         {
//     //             return customer;
//     //         }
//     //     }
//     //     return null;
//     // }
// 
}