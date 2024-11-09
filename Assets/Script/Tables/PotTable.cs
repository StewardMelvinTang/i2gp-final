using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PotTable : Table {

    private float timeCounter;
    private bool startCounter;

    void Update(){
        if(startCounter){
            timeCounter -= Time.deltaTime;
            if(timeCounter < 0.0f){
                startCounter = false;
                Destroy(foodObject);
                foodObject = Instantiate(foodObject.GetComponent<Item>().objAfterPot);
                foodObject.transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);
            
                if (foodObject.GetComponent<Item>().canPot) {
                    startCounter = true;
                    timeCounter = foodObject.GetComponent<Item>().potTime;
                }
            }
        }
    }

    public override GameObject PutItem(GameObject targetObject) {

        Item targetItem = null;
        Item currentItem = null;
        
        if (targetObject != null) {
            targetItem = targetObject.GetComponent<Item>();
        }

        if (foodObject != null) {
            currentItem = foodObject.GetComponent<Item>();
        }

        if (targetObject != null && targetItem.canPot) {
            GameObject returnItem = foodObject;
            startCounter = true;
            timeCounter = targetItem.potTime;
            foodObject = targetObject;
            foodObject.transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);
            return returnItem;
        }
        else {
            return targetObject;
        }
    }

    public override GameObject TakeItem(){
        GameObject returnItem = foodObject;
        foodObject = null;
        startCounter = false;
        return returnItem;
    }
}
