using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Pizza/Recipe")]
public class Recipe : ScriptableObject
{
    public string recipeName;
    public List<string> ingredients;
}