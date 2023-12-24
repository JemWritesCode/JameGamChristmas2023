using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

namespace JameGam.UI {
  public sealed class StartScreenController : MonoBehaviour {
    [field: SerializeField, Header("Stars1")]
    public Image Stars1 { get; private set; }

    [field: SerializeField]
    public Image Mountains { get; private set; }

    [field: SerializeField, Header("Santa")]
    public Image Santa { get; private set; }

    [field: SerializeField, Header("Anvil")]
    public Image Anvil { get; private set; }

    [field: SerializeField, Header("StartGameButton")]
    public Image StartGameButton { get; private set; }

    [field: SerializeField, Header("TitleText")]
    public Image TitleText { get; private set; }

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

    private Sequence CreateTranslateFade(Image image, float position, Vector3 direction, float duration) {
      return DOTween.Sequence()
                .Insert(position - duration / 2,
                        image.transform.DOPunchPosition(Vector3.Scale(direction, new(-1f, -1f, -1f)), duration, 0, 0f))
                .Insert(position, image.DOFade(1.0f, duration / 2).From(0f, true));
    }

    public void AnimateIntro() {
      DOTween.Complete(gameObject, withCallbacks: true);

      /*
      DOTween.Sequence()
          .SetTarget(gameObject)
          .Insert(0f, Background.DOFade(1f, 0.5f).From(0f, true))
          .Insert(0.5f, Mountains.DOFade(1f, 1f).From(0f, true))
          .Insert(0.5f, Santa.transform.DOPunchPosition(new(75f, 0f, 0f), 1.5f, 0, 0f))
          .Insert(1.0f, SfxAudioSource.DOPlayOneShot(SantaIntroSfx))
          .Insert(1.0f, Santa.DOFade(1f, 1f).From(0f, true))
          .Insert(1.5f, Anvil.transform.DOPunchPosition(new(0f, -75f, 0f), 1.5f, 0, 0f))
          .Insert(2f, SfxAudioSource.DOPlayOneShot(AnvilIntroSfx))
          .Insert(2f, Anvil.DOFade(1f, 1f).From(0f, true))
          .Insert(2f, StartGameButton.transform.DOPunchPosition(new(-75f, 0f, 0f), 1.5f, 0, 0f))
          .Insert(2.5f, StartGameButton.DOFade(1f, 1f).From(0f, true))
          .SetEase(Ease.InOutQuad);
      */

      DOTween.Sequence()
          .SetTarget(gameObject)
          .Insert(0f, CreateTranslateFade(Stars1, 0.0f, new(0f, 0, 0f), 1.25f))
          .Insert(0f, CreateTranslateFade(Mountains, 0.0f, new(0f, 0f, 0f), 1.25f))
          .Insert(0f, CreateTranslateFade(Santa, 0.5f, new(-250f, 0f, 0f), 1.5f))
          .Insert(0.5f, SfxAudioSource.DOPlayOneShot(SantaIntroSfx))
          .Insert(0f, CreateTranslateFade(Anvil, 0.5f, new(0f, 250f, 0f), 1.5f))
          .Insert(0f, CreateTranslateFade(TitleText, 0.5f, new(0f, -250f, 0f), 1.5f))
          .Insert(0.5f, SfxAudioSource.DOPlayOneShot(AnvilIntroSfx))
          .Insert(0f, CreateTranslateFade(StartGameButton, 0.5f, new(0f, 0f, 0f), 1.5f))
          .SetEase(Ease.InOutQuad);
    }

    public void OnSantaClicked() {
      Santa.DOComplete(withCallbacks: true);

      DOTween.Sequence()
          .SetTarget(Santa)
          .Insert(0f, Santa.transform.DOPunchScale(Vector3.one * 0.025f, 0.5f, 10, 1f))
          .Insert(0f, SfxAudioSource.DOPlayOneShot(SantaClickedSfx));
    }

    public void OnStartGameButtonClicked() {

    }
  }
}
