using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrashBin : Table
{
    public override GameObject PutItem(GameObject gameObject) {
        Item item = gameObject.GetComponent<Item>();
        
        if (!item) return gameObject;
        if (!item.canBeTrash) return gameObject;
        
        
        Destroy(gameObject);
        return null;
    }

}
