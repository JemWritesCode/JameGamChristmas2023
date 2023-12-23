using UnityEngine;
using UnityEngine.Events;

namespace JameGam {
  public class Interactable : MonoBehaviour {
    [field: SerializeField, Header("Interact")]
    public float InteractRange { get; set; } = 2f;

    [field: SerializeField]
    public string InteractText { get; set; } = string.Empty;

    [field: SerializeField, Header("Highlight")]
    public MeshRenderer HighlightRenderer { get; set; }

    [Header("Events"), Space(10)]
    public UnityEvent OnInteract;

    public void Interact() {
      OnInteract?.Invoke();
    }
  }
}
