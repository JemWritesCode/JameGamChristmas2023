using UnityEngine;

namespace JameGam {
  [CreateAssetMenu(menuName = "JameGam/Item", fileName = "Item")]
  public class Item : ScriptableObject {
    [field: SerializeField]
    public string ItemName { get; private set; }

    [field: SerializeField, TextArea]
    public string ItemDescription { get; private set; }

    [field: SerializeField]
    public Sprite ItemIcon { get; private set; }

    [field: SerializeField]
    public GameObject ItemPrefab { get; private set; }
  }
}
