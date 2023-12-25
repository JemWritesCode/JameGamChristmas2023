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
          .Insert(0.15f, QuoteBackground.DOFade(1f, 1f).From(0f, true))
          .Insert(0.15f, QuoteBackground.transform.DOMoveY(10f, 1f).From(isRelative: true))
          .Insert(0.35f, QuoteIcon.DOFade(1f, 1f).From(0f, true))
          .Insert(0.35f, QuoteIcon.transform.DOMoveX(-10f, 1f).From(isRelative: true))
          .Insert(0.55f, QuoteText.DOFade(1f, 1f).From(0f, true))
          .Insert(0.55f, QuoteText.transform.DOMoveY(5f, 1f).From(isRelative: true))
          .Insert(1.00f, StarsBackground.DOFade(1f, 0.5f).From(0f, true))
          .Insert(1.00f, StarsBackground.transform.DOMoveY(5f, 0.5f).From(isRelative: true))
          .Insert(2f, FadeStarIn(StarIconLeftEffect))
          .Insert(2.75f, FadeStarIn(StarIconCenterEffect))
          .Insert(3.50f, FadeStarIn(StarIconRightEffect));

      Tween FadeStarIn(UIEffect effect) {
        return DOTween.Sequence()
            .Insert(0f, effect.GetComponent<Image>().DOFade(1f, 1f).From(0f, true))
            .Insert(0f, DOTween.To(() => effect.effectFactor, x => effect.effectFactor = x, 0f, 2f).From(1f, true))
            .Insert(0f, SfxAudioSource.DOPlayOneShot(AnimateStarSfx))
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
  }
}
