using UnityEngine;

namespace JameGam{
  public sealed class InputManager : MonoBehaviour {
    [field: SerializeField, Header("KeyBinds")]
    public KeyCode ToggleMenuKey { get; private set; } = KeyCode.P;

    [field: SerializeField]
    public KeyCode InteractKey { get; private set; } = KeyCode.E;

    static InputManager _instance;

    public static InputManager Instance {
      get {
        if (!_instance) {
          _instance = FindObjectOfType<InputManager>();
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

    private void Update() {
      if (Input.GetKeyDown(ToggleMenuKey)) {
        OnToggleMenuKey();
      }

      if (Input.GetKeyDown(InteractKey)) {
        OnInteractKey();
      }
    }

    public void OnToggleMenuKey() {
      UIManager.Instance.SettingsPanel.TogglePanel();
    }

    public void OnInteractKey() {
      Interactable interactable = InteractManager.Instance.ClosestInteractable;

      if (interactable) {
        interactable.Interact();
      }
    }
  }
}
