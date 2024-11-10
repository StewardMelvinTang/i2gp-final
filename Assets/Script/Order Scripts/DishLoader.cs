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
    public static DishLoader Instance;
    public Dictionary<string, HashSet<string>> dishes = new Dictionary<string, HashSet<string>>();

    void Start()
    {
        LoadDishes();
    }

    void LoadDishes()
    {
        Debug.Log("Loading Dishes");
        string path = Path.Combine(Application.streamingAssetsPath, "Foods_JSON/foods.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Dishes dishList = JsonConvert.DeserializeObject<Dishes>(json);

            foreach (Dish dish in dishList.dishes)
            {
                Debug.Log(dish.name);
                dishes[dish.name] = new HashSet<string>(dish.ingredients);
            }
        }
        else
        {
            Debug.LogError("Dishes file not found at path: " + path);
        }
    }

    public void RemoveDish(string dishName)
    {
        if (dishes.ContainsKey(dishName))
        {
            dishes.Remove(dishName);
            Debug.Log($"Dish removed: {dishName}");
        }
    }
}