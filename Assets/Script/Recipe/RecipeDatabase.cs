using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeDatabase", menuName = "Recipe/Database")]
public class RecipeDatabase : ScriptableObject
{
    public List<Recipe> recipes;
}
