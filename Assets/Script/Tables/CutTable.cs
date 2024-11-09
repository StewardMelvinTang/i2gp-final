using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CutTable : Table
{
    public override GameObject PutItem(GameObject targetObject) {

        Item targetItem = null;
        Item currentItem = null;

        if (targetObject != null) {
            targetItem = targetObject.GetComponent<Item>();
        }

        if (foodObject != null) {
            currentItem = foodObject.GetComponent<Item>();
        }

        if (targetObject != null && foodObject != null &&
            targetItem.itemName == "knife" && currentItem.canCut) {
            
            Destroy(foodObject);
            foodObject = Instantiate(currentItem.objAfterCut);
            foodObject.transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);
            GameObject ret = targetObject;
            return ret;
        }
        else {
            GameObject ret = foodObject;
            foodObject = targetObject;
            foodObject.transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);
            return ret;
        }
        
    }
}
