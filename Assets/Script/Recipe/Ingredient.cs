using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Recipe/Ingredient")]
public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public Sprite ingredientIcon;
}
