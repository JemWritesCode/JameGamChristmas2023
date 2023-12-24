using System.Collections.Generic;

using UnityEngine;

namespace JameGam {
  [CreateAssetMenu(menuName = "JameGam/Recipe", fileName = "Recipe")]
  public class Recipe : ScriptableObject {
    [field: SerializeField]
    public string RecipeName { get; private set; }

    [field: SerializeField]
    public Sprite RecipeIcon { get; private set; }

    [field: SerializeField]
    public string RecipeDescription { get; private set; }

    [field: SerializeField]
    public List<Item> ItemsNeeded { get; private set; }

    [field: SerializeField]
    public List<Item> ItemsProduced { get; private set; }
  }
}
