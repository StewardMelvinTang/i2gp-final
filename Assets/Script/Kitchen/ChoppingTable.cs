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
                foodObject.transform.position = new Vector3(transform.position.x, 1.7f, transform.position.z);
                GameObject ret = gameObject;
                Destroy(gameObject);
                return ret;
        }
        else{
            if(infiniteItem){
                Destroy(gameObject);
                return null;
            }
            GameObject ret = foodObject;
            if(foodObject) Destroy(foodObject);
            foodObject = gameObject;
            foodObject.transform.position = new Vector3(transform.position.x, 1.7f, transform.position.z);

            return ret;
        }
        
    }
}
