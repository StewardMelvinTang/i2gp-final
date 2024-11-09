using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChoppingTable : KitchenTable
{
    [Header("Prefabs")]
    [SerializeField] private GameObject carrotChopped;

    public override GameObject PutItem(GameObject gameObject)
    {        
        if( gameObject != null && foodObject != null &&
            gameObject.CompareTag("knife") && foodObject.CompareTag("carrot")){
                Destroy(foodObject);
                foodObject = Instantiate(carrotChopped);
                foodObject.transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);
                GameObject ret = gameObject;
                return ret;
        }
        else{
            GameObject ret = foodObject;
            foodObject = gameObject;
            foodObject.transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);

            return ret;
        }
        
    }
}
