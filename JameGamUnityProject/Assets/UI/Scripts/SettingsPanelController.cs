using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

namespace JameGam.UI {
  public sealed class SettingsPanelController : MonoBehaviour {
    [field: SerializeField, Header("Panel")]
    public RectTransform Panel { get; private set; }

    [field: SerializeField]
    public CanvasGroup PanelCanvasGroup { get; private set; }

    [field: SerializeField, Header("SFX")]
    public AudioSource SfxAudioSource { get; private set; }

    [field: SerializeField]
    public AudioClip ShowPanelSfx { get; private set; }

    [field: SerializeField]
    public AudioClip HidePanelSfx { get; private set; }

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

    public void TogglePanel() {
      if (IsPanelVisible) {
        HidePanel();
      } else {
        ShowPanel();
      }
    }

    public void ShowPanel() {
      Panel.DOComplete(withCallbacks: true);

      IsPanelVisible = true;
      PanelCanvasGroup.blocksRaycasts = true;

      AudioVolumeSlider.SetValueWithoutNotify(AudioListener.volume);

      DOTween.Sequence()
          .SetTarget(Panel)
          .Insert(0f, PanelCanvasGroup.DOFade(1f, 0.25f))
          .Insert(0f, SfxAudioSource.DOPlayOneShot(ShowPanelSfx));

      GameManager.Instance.PauseGame();
    }

    public void HidePanel() {
      Panel.DOComplete(withCallbacks: true);

      IsPanelVisible = false;
      PanelCanvasGroup.blocksRaycasts = false;

      DOTween.Sequence()
          .SetTarget(Panel)
          .Insert(0f, PanelCanvasGroup.DOFade(0f, 0.25f))
          .Insert(0f, SfxAudioSource.DOPlayOneShot(HidePanelSfx));

      GameManager.Instance.ResumeGame();
    }

    public void OnAudioVolumeSliderChanged(float value) {
      AudioListener.volume = value;
    }
  }
}
