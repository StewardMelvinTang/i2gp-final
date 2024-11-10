using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

[System.Serializable]
public class Dish
{
    public string name;
    public List<string> ingredients;
}

[System.Serializable]
public class Dishes
{
    public List<Dish> dishes;
}

public class DishLoader : MonoBehaviour
{
    public Dictionary<string, HashSet<string>> dishes = new Dictionary<string, HashSet<string>>();

    void Start()
    {
        LoadDishes();
    }

    void LoadDishes()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Foods_JSON/foods.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Dishes dishList = JsonConvert.DeserializeObject<Dishes>(json);

            foreach (Dish dish in dishList.dishes)
            {
                dishes[dish.name] = new HashSet<string>(dish.ingredients);
            }
        }
        else
        {
            Debug.LogError("Dishes file not found at path: " + path);
        }
    }
}