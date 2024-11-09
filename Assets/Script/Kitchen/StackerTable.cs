using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StackerTable : KitchenTable
{
    private StackFood stackFood = null;

    public override GameObject PutItem(GameObject gameObject)
    {
        /* Empty Hand, Take the food */
        if(gameObject == null){
            if(foodObject == null){
                return null;
            }
            GameObject ret = foodObject;
            foodObject = null;
            return ret;
        }
        /* Switch For Knife */
        else if(gameObject.CompareTag("knife")){
            GameObject ret = foodObject;
            foodObject = null;
            foodObject = gameObject;
            foodObject.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);

            return ret;
        }
        /* If there is some food, stack the food */
        else{
            /* Empty Table */
            if(foodObject == null){
                foodObject = gameObject;
                foodObject.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            }
            /* Join Stack */
            else{
                GameObject emptyObject = new GameObject("StackFood");
                StackFood stackFood = emptyObject.AddComponent<StackFood>();

                StackFood otherStackFood = gameObject.GetComponent<StackFood>();
                StackFood currentStackFood = foodObject.GetComponent<StackFood>();
                if (otherStackFood == null) {
                    otherStackFood = gameObject.AddComponent<StackFood>();
                    otherStackFood.InsertFood(gameObject);
                } 
                if (currentStackFood == null) {
                    currentStackFood = foodObject.AddComponent<StackFood>();
                    currentStackFood.InsertFood(foodObject);
                }
                
                stackFood.JoinFood(currentStackFood);
                stackFood.JoinFood(otherStackFood);
                

                foodObject = emptyObject;
                foodObject.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            }
            return null;
        }
    }
}
