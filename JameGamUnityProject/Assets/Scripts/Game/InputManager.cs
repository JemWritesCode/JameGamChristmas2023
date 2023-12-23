using JameGam.UI;

using UnityEngine;

namespace JameGam{
  public sealed class InputManager : MonoBehaviour {
    [field: SerializeField, Header("Panels")]
    public SettingsPanelController SettingsPanel { get; private set; }

    [field: SerializeField, Header("KeyBinds")]
    public KeyCode ToggleMenuKey { get; private set; } = KeyCode.P;

    private void Update() {
      if (Input.GetKeyDown(ToggleMenuKey)) {
        SettingsPanel.TogglePanel();
      }
    }
  }
}
