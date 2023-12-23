using UnityEngine;

namespace JameGam {
  public sealed class InteractManager : MonoBehaviour {
    static InteractManager _instance;

    public static InteractManager Instance {
      get {
        if (!_instance) {
          _instance = FindObjectOfType<InteractManager>();
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
