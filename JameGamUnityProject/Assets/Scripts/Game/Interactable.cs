using UnityEngine;

namespace JameGam {
  public class Interactable : MonoBehaviour {
    public static readonly int ColorShaderId = Shader.PropertyToID("_Color");

    [field: SerializeField]
    public float InteractRange { get; set; } = 2f;

    [field: SerializeField]
    public string InteractText { get; set; } = string.Empty;
  }
}
