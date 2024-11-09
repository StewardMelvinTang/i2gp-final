using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StackerTable : KitchenTable
{
    private StackFood stackFood = null;
    public override GameObject PutItem(GameObject gameObject) {
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
        /* Switch For Knife */
        else if (gameObject.CompareTag("knife"))
        {
            GameObject ret = foodObject;
            foodObject = gameObject;
            foodObject.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            return ret;
        }
        /* If there is some food, stack the food */
        else
        {
            /* Empty Table */
            if (foodObject == null)
            {
                foodObject = gameObject;
                foodObject.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            }
            /* Join Stack */
            else
            {
                // Check if the existing `foodObject` or the incoming item already has a StackFood component
                StackFood currentStackFood = foodObject.GetComponent<StackFood>();
                StackFood otherStackFood = gameObject.GetComponent<StackFood>();
    
                // If there’s no existing stack on the current or incoming item, create and add it
                if (currentStackFood == null)
                {
                    currentStackFood = foodObject.AddComponent<StackFood>();
                    currentStackFood.InsertFood(foodObject); // Insert the current foodObject as the initial stack item
                }
                if (otherStackFood == null)
                {
                    otherStackFood = gameObject.AddComponent<StackFood>();
                    otherStackFood.InsertFood(gameObject); // Insert the incoming gameObject as the initial stack item
                }
    
                // Check if the current stack can accept items from the other stack
                /*
                if (!currentStackFood.CanJoinFood(otherStackFood))
                {
                    Debug.Log("Cannot join due to duplicate items.");
                    return gameObject; // Return the item to allow the player to keep carrying it without modifying foodObject
                } */
    
                // Join the other stack onto the current stack
                currentStackFood.JoinFood(otherStackFood);
    
                // Update the foodObject to the current stacked object and reposition it
                foodObject = currentStackFood.gameObject;
                foodObject.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            }
            return null;
        }
    }

}
