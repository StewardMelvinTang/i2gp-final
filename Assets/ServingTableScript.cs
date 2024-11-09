using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingTableScript : KitchenTable
{
    private Dictionary<string, HashSet<string>> dishes;

    // Initialize the dictionary with possible dishes and ingredients
    void Start()
    {
        dishes = new Dictionary<string, HashSet<string>>()
        {
            { "hamBurger", new HashSet<string> { "plate", "cookedmeat", "bun" } },
            { "hamBurgerWithCarrot", new HashSet<string> { "plate", "cookedmeat", "bun", "carrot" } }
        };
    }

    public override GameObject PutItem(GameObject gameObject)
    {
        /* Empty Hand, Take the food */
        if (gameObject == null)
        {
            if (foodObject == null)
            {
                return null;
            }
            GameObject ret = foodObject;
            foodObject = null;
            return ret;
        }
    
        StackFood stackFood = gameObject.GetComponent<StackFood>();
        if (!stackFood) return null;

        // Get tags of all items in the stack
        HashSet<string> stackContents = new HashSet<string>();
        foreach (GameObject food in stackFood.GetFoodStack())
        {
            stackContents.Add(food.tag); 
            Debug.Log("Stack contains: " + food.tag);
        }

        // Check if the stack contents match any dish in the dictionary
        string matchedDish = null;
        foreach (var dish in dishes)
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
            Debug.Log("Dish created: " + matchedDish);
        }
        else
        {
            Debug.Log("No matching dish found.");
        }

        return null;
    }

}