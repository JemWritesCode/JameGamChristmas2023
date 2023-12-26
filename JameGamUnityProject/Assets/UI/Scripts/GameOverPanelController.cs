using Coffee.UIEffects;

using DG.Tweening;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace JameGam.UI {
  public sealed class GameOverPanelController : MonoBehaviour {
    [field: SerializeField, Header("Panel")]
    public RectTransform Panel { get; private set; }

    [field: SerializeField]
    public CanvasGroup PanelCanvasGroup { get; private set; }

    [field: SerializeField]
    public UIGradient PanelBackgroundGradient { get; private set; }

    [field: SerializeField, Header("SantaQuote")]
    public Image QuoteBackground { get; private set; }

    [field: SerializeField]
    public Image QuoteIcon { get; private set; }

    [field: SerializeField]
    public UIEffect QuoteIconEffect { get; private set; }

    [field: SerializeField]
    public TMP_Text QuoteText { get; private set; }

    [field: SerializeField, Header("Stars")]
    public Image StarsBackground { get; private set; }

    [field: SerializeField]
    public UIEffect StarIconLeftEffect { get; private set; }

    [field: SerializeField]
    public UIEffect StarIconCenterEffect { get; private set; }

    [field: SerializeField]
    public UIEffect StarIconRightEffect { get; private set; }

    [field: SerializeField, Header("SFX")]
    public AudioSource SfxAudioSource { get; private set; }

    [field: SerializeField]
    public AudioClip ShowPanelSfx { get; private set; }

    [field: SerializeField]
    public AudioClip AnimateStarSfx { get; private set; }

    public bool IsPanelVisible { get; private set; }

    public void Awake() {
      ResetPanel();
    }

    public void ResetPanel() {
      PanelCanvasGroup.alpha = 0f;
      PanelCanvasGroup.blocksRaycasts = false;
      IsPanelVisible = false;
    }

    public void ShowPanel() {
      Panel.DOComplete(withCallbacks: true);

      IsPanelVisible = true;
      PanelCanvasGroup.blocksRaycasts = true;

      DOTween.Sequence()
          .SetTarget(Panel)
          .Insert(0f, SfxAudioSource.DOPlayOneShot(ShowPanelSfx))
          .Insert(0f, PanelCanvasGroup.DOFade(1f, 0.25f))
          .Insert(0.15f, FadeMoveImage(QuoteBackground, new(0f, 10f, 0f), 1f))
          .Insert(0.35f, FadeMoveImage(QuoteIcon, new(-10f, 0f, 0f), 1f))
          .Insert(0.55f, FadeMoveImage(QuoteText, new(0f, 5f, 0f), 1f))
          .Insert(1.00f, FadeMoveImage(StarsBackground, new(0f, 5f, 0f), 0.5f))
          .Insert(1.50f, FadeStarIn(StarIconLeftEffect, SfxAudioSource, AnimateStarSfx))
          .Insert(2.25f, FadeStarIn(StarIconCenterEffect, SfxAudioSource, AnimateStarSfx))
          .Insert(3.00f, FadeStarIn(StarIconRightEffect, SfxAudioSource, AnimateStarSfx));

      static Tween FadeMoveImage(Graphic image, Vector3 offset, float duration) {
        return DOTween.Sequence()
            .Insert(0f, image.DOFade(1f, duration).From(0f, true))
            .Insert(0f, image.transform.DOMove(offset, duration).From(isRelative: true));
      }

      static Tween FadeStarIn(UIEffect effect, AudioSource audioSource, AudioClip sfxAudioClip) {
        return DOTween.Sequence()
            .Insert(0f, effect.GetComponent<Image>().DOFade(1f, 1f).From(0f, true))
            .Insert(0f, DOTween.To(() => effect.effectFactor, x => effect.effectFactor = x, 0f, 0.75f).From(1f, true))
            .Insert(0f, audioSource.DOPlayOneShot(sfxAudioClip))
            .Insert(0f, effect.transform.DOPunchScale(Vector3.one * 1.05f, 1.5f, 5, 0f));
      }
    }

    public void HidePanel() {
      Panel.DOComplete(withCallbacks: true);

      IsPanelVisible = false;
      PanelCanvasGroup.blocksRaycasts = false;

      DOTween.Sequence()
          .SetTarget(Panel)
          .Insert(0f, PanelCanvasGroup.DOFade(0f, 0.25f));
    }

    public void OnStarIconClicked(UIEffect effect) {
      effect.DOComplete(withCallbacks: true);

      DOTween.Sequence()
          .SetTarget(effect)
          .Insert(0f, SfxAudioSource.DOPlayOneShot(AnimateStarSfx))
          .Insert(0f, effect.transform.DOPunchScale(Vector3.one * 0.15f, 0.5f, 10, 1f));
    }
  }
}
