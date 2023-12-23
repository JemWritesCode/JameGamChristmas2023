using JameGam.UI;

using UnityEngine;

namespace JameGam{
  public sealed class InputManager : MonoBehaviour {
    [field: SerializeField, Header("Panels")]
    public SettingsPanelController SettingsPanel { get; private set; }

    [field: SerializeField, Header("KeyBinds")]
    public KeyCode ToggleMenuKey { get; private set; } = KeyCode.P;

    [field: SerializeField]
    public KeyCode InteractKey { get; private set; } = KeyCode.Space;

    private void Update() {
      if (Input.GetKeyDown(ToggleMenuKey)) {
        OnToggleMenuKey();
      }

      if (Input.GetKeyDown(InteractKey)) {
        OnInteractKey();
      }
    }

    public void OnToggleMenuKey() {
      SettingsPanel.TogglePanel();
    }

    public void OnInteractKey() {
      Interactable interactable = InteractManager.Instance.ClosestInteractable;

      if (interactable) {
        interactable.Interact();
      }
    }
  }
}
