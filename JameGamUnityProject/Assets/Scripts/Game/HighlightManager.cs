using Facepunch;

using UnityEngine;

namespace JameGam {
  public sealed class HighlightManager : MonoBehaviour {
    [field: SerializeField]
    public Highlight InteractHighlight { get; private set; }

    public void AddInteractable(Interactable interactable) {
      if (interactable && interactable.HighlightRenderer) {
        InteractHighlight.Add(interactable.HighlightRenderer);
        InteractHighlight.Rebuild();
      }
    }

    public void RemoveInteractable(Interactable interactable) {
      if (interactable && interactable.HighlightRenderer) {
        InteractHighlight.Remove(interactable.HighlightRenderer);
        InteractHighlight.Rebuild();
      }
    }

    static HighlightManager _instance;

    public static HighlightManager Instance {
      get {
        if (!_instance) {
          _instance = FindObjectOfType<HighlightManager>();
        }

        return _instance;
      }
    }

    private void Awake() {
      if (_instance) {
        Destroy(this);
      } else {
        _instance = this;
      }
    }
  }
}
