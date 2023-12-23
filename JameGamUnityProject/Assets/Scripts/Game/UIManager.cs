using JameGam.UI;

using UnityEngine;

namespace JameGam {
  public sealed class UIManager : MonoBehaviour {
    [field: SerializeField, Header("Panels")]
    public OrdersPanelController OrdersPanel { get; private set; }

    [field: SerializeField]
    public ScorePanelController ScorePanel { get; private set; }

    [field: SerializeField]
    public TimerPanelController TimerPanel { get; private set; }

    [field: SerializeField]
    public DialogPanelController DialogPanel { get; private set; }

    [field: SerializeField]
    public HelpPanelController HelpPanel { get; private set; }

    [field: SerializeField]
    public SettingsPanelController SettingsPanel { get; private set; }

    [field: SerializeField]
    public GameOverPanelController GameOverPanel { get; private set; }

    static UIManager _instance;

    public static UIManager Instance {
      get {
        if (!_instance) {
          _instance = FindObjectOfType<UIManager>();
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
