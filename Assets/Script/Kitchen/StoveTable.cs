using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoveTable : KitchenTable
{
    [Header("Prefabs")]
    [SerializeField] private GameObject wellDoneMeat;

    [Header("Cooking Time")]
    [SerializeField] private float cookingTime = 5.0f;

    private float timeCounter;
    private bool startCounter;

    void Start(){
        base.Start();
        timeCounter = 0.0f;
        startCounter = false;
    }

    void Update(){
        if(startCounter){
            timeCounter -= Time.deltaTime;
            if(timeCounter < 0.0f){
                startCounter = false;
                Destroy(foodObject);
                foodObject = Instantiate(wellDoneMeat);
                foodObject.transform.position = new Vector3(transform.position.x, 1.7f, transform.position.z);
            }
        }
    }

    public override GameObject PutItem(GameObject gameObject)
    {       
        /* Putting a raw meat */
        if(gameObject != null && gameObject.CompareTag("meat")){
            startCounter = true;
            timeCounter = cookingTime;

            foodObject = gameObject;
            foodObject.transform.position = new Vector3(transform.position.x, 1.7f, transform.position.z);
            return null;
        }
        /* Can't put anything else */
        else{
            GameObject ret = gameObject;
            Destroy(gameObject);
            return ret;
        }
    }

    public override GameObject TakeItem(){
        GameObject returnItem = foodObject;
        Destroy(foodObject);
        foodObject = null;
        startCounter = false;
        return returnItem;
    }
}
