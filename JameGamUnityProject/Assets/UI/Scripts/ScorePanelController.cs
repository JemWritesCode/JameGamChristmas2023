using UnityEngine;

namespace JameGam.UI {
  public sealed class ScorePanelController : MonoBehaviour {
    [field: SerializeField, Header("Panel")]
    public RectTransform Panel { get; private set; }

    [field: SerializeField]
    public CanvasGroup PanelCanvasGroup { get; private set; }
  }
}
