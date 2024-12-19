using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Recipe/Recipe")]
public class Recipe : ScriptableObject
{
    public string recipeName;
    public Sprite recipeIcon;
    public List<Ingredient> ingredients;
}
