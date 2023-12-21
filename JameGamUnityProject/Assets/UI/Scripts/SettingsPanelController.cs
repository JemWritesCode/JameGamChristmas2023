using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

namespace JameGam.UI {
  public sealed class SettingsPanelController : MonoBehaviour {
    [field: SerializeField, Header("Panel")]
    public RectTransform Panel { get; private set; }

    [field: SerializeField]
    public CanvasGroup PanelCanvasGroup { get; private set; }

    [field: SerializeField, Header("Audio Volume")]
    public Slider AudioVolumeSlider { get; private set; }

    public bool IsPanelVisible { get; private set; }

    private void Awake() {
      ResetPanel();
    }

    public void ResetPanel() {
      PanelCanvasGroup.alpha = 0f;
      PanelCanvasGroup.blocksRaycasts = false;
      IsPanelVisible = false;
    }

    public void ShowPanel() {
      Panel.DOComplete(withCallbacks: true);

      DOTween.Sequence()
          .SetTarget(Panel)
          .Insert(0f, PanelCanvasGroup.DOFade(1f, 0.25f))
          .OnComplete(() => {
            PanelCanvasGroup.blocksRaycasts = true;
            IsPanelVisible = true;
          });
    }

    public void HidePanel() {
      Panel.DOComplete(withCallbacks: true);

      DOTween.Sequence()
          .SetTarget(Panel)
          .Insert(0f, PanelCanvasGroup.DOFade(0f, 0.25f))
          .OnComplete(() => {
            PanelCanvasGroup.blocksRaycasts = false;
            IsPanelVisible = false;
          });
    }

    public void OnAudioVolumeSliderChanged(float value) {
      AudioListener.volume = value;
    }
  }
}
