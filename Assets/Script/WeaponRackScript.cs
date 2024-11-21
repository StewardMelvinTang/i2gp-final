using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRackScript : Table {
    [SerializeField] private Vector3 weaponStoreLocation = new Vector3();
    
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HasAnyWeaponStored() {
        return foodObject != null;
    }

    public override GameObject PutItem(GameObject gameObject) {
        if (gameObject == null) return null;
        
        Debug.Log("Put Item to Weapon Rack");
        foodObject = gameObject;
        foodObject.transform.position = transform.position + weaponStoreLocation;
        foodObject.transform.localScale = transform.localScale * 1.5f;

        return null; //pass empty holding to the player
    }

    public virtual GameObject TakeItem() {
        // foodObject.transform.localScale = transform.localScale * 0.75f;
        GameObject returnItem = foodObject;
        foodObject = null;
        return returnItem;
    }
}
