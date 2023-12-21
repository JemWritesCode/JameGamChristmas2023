using JameGam.UI;

using UnityEngine;

namespace JameGam{
  public sealed class InputManager : MonoBehaviour {
    [field: SerializeField, Header("Panels")]
    public SettingsPanelController SettingsPanel { get; private set; }


  }
}
