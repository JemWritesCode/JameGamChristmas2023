using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

namespace JameGam.UI {
  public sealed class StartScreenController : MonoBehaviour {
    [field: SerializeField, Header("Santa")]
    public Image Santa { get; private set; }

    [field: SerializeField, Header("Anvil")]
    public Image Anvil { get; private set; }

    [field: SerializeField, Header("SFX")]
    public AudioSource SfxAudioSource { get; private set; }

    [field: SerializeField]
    public AudioClip SantaIntroSfx { get; private set; }

    [field: SerializeField]
    public AudioClip AnvilIntroSfx { get; private set; }

    private void Awake() {
      AnimateIntro();
    }

    public void AnimateIntro() {
      DOTween.Sequence()
          .Insert(0.5f, SfxAudioSource.DOPlayOneShot(SantaIntroSfx))
          .Insert(0.5f, Santa.DOFade(1f, 1f).From(0f, true))
          .Insert(0f, Santa.transform.DOPunchPosition(new(75f, 0f, 0f), 1.5f, 0, 0))
          .Insert(1.25f, SfxAudioSource.DOPlayOneShot(AnvilIntroSfx))
          .Insert(1.25f, Anvil.DOFade(1f, 1f).From(0f, true))
          .Insert(0.75f, Anvil.transform.DOPunchPosition(new(0f, -75f, 0f), 1.5f, 0, 0))
          .SetEase(Ease.InOutQuad);
    }
  }
}
