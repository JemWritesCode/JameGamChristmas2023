using Coffee.UIEffects;

using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

namespace JameGam.UI {
  public sealed class StartScreenController : MonoBehaviour {
    [field: SerializeField, Header("Background")]
    public Image Background { get; private set; }

    [field: SerializeField]
    public Image Mountains { get; private set; }

    [field: SerializeField, Header("Santa")]
    public Image Santa { get; private set; }

    [field: SerializeField, Header("Anvil")]
    public Image Anvil { get; private set; }

    [field: SerializeField, Header("SFX")]
    public AudioSource SfxAudioSource { get; private set; }

    [field: SerializeField]
    public AudioClip SantaIntroSfx { get; private set; }

    [field: SerializeField]
    public AudioClip SantaClickedSfx { get; private set; }

    [field: SerializeField]
    public AudioClip AnvilIntroSfx { get; private set; }

    private void Awake() {
      AnimateIntro();
    }

    public void AnimateIntro() {
      DOTween.Sequence()
          .Insert(0f, Background.DOFade(1f, 0.5f).From(0f, true))
          .Insert(0.5f, Mountains.DOFade(1f, 1f).From(0f, true))
          .Insert(0.5f, Santa.transform.DOPunchPosition(new(75f, 0f, 0f), 1.5f, 0, 0f))
          .Insert(1.0f, SfxAudioSource.DOPlayOneShot(SantaIntroSfx))
          .Insert(1.0f, Santa.DOFade(1f, 1f).From(0f, true))
          .Insert(1.5f, Anvil.transform.DOPunchPosition(new(0f, -75f, 0f), 1.5f, 0, 0f))
          .Insert(2f, SfxAudioSource.DOPlayOneShot(AnvilIntroSfx))
          .Insert(2f, Anvil.DOFade(1f, 1f).From(0f, true))
          .SetEase(Ease.InOutQuad);
    }

    public void OnSantaClicked() {
      Santa.DOComplete(withCallbacks: true);

      DOTween.Sequence()
          .SetTarget(Santa)
          .Insert(0f, Santa.transform.DOPunchScale(Vector3.one * 0.025f, 0.5f, 10, 1f))
          .Insert(0f, SfxAudioSource.DOPlayOneShot(SantaClickedSfx));
    }
  }
}
