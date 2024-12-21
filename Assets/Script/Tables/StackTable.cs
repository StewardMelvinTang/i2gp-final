using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StackTable : Table
{
    private StackFood stackFood = null;
    

    public override GameObject PutItem(GameObject gameObject) {
        // Empty Table
        if (foodObject == null)
        {
            foodObject = gameObject;

            if (foodObject.TryGetComponent(out ItemTransformData itemData)) {
                itemData.ApplySavedTransform(
                    foodObject.transform, 
                    transform.position
                );
                    
                itemData.SaveTransform(foodObject.transform, itemPositionOffset); // save new transform
            }
            else {
                foodObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
            }
            
            return null;
        }

        /* Switch For Knife */
        Item item1 = gameObject.GetComponent<Item>();
        Item item2 = foodObject.GetComponent<Item>();
        if (item1.isTool || item2.isTool)
        {
            GameObject ret = foodObject;
            foodObject = gameObject;
            
            if (foodObject.TryGetComponent(out ItemTransformData itemData)) {
                itemData.ApplySavedTransform(
                    foodObject.transform, 
                    transform.position
                );
                    
                itemData.SaveTransform(foodObject.transform, itemPositionOffset); // save new transform
            }
            
            else {
                foodObject.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            }
            
            
            return ret;
        }
        /* If there is some food, stack the food */
        else
        {
            /* Join Stack */

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

            Item tmp = foodObject.GetComponent<Item>();
            if(tmp){
                tmp.Reset();
            }
            
            
            return null;
        }
    }

}
