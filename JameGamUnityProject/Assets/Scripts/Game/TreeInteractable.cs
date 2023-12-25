using DG.Tweening;

using UnityEngine;

namespace JameGam {
  public sealed class TreeInteractable : Interactable {
    [field: SerializeField, Header("SFX")]
    public AudioSource SfxAudioSource { get; private set; }

    [field: SerializeField]
    public AudioClip ShakeTreeSfx { get; private set; }

    public void ShakeTree() {
      transform.DOComplete(withCallbacks: true);

      DOTween.Sequence()
          .Insert(0f, SfxAudioSource.DOPitch(1f, 1f).From(Random.Range(0.85f, 1.15f), true))
          .Insert(0f, SfxAudioSource.DOPlayOneShot(ShakeTreeSfx))
          .Insert(0f, transform.DOBlendablePunchRotation(RandomShakeTreeRotation(), Random.Range(1f, 1.5f), 6, 1f))
          .Insert(0f, transform.DOBlendableRotateBy(new(0f, Random.Range(-35f, 35f), 0f), 1f, RotateMode.Fast))
          .SetTarget(transform);
    }

    Vector3 RandomShakeTreeRotation() {
      return new Vector3(
          Random.Range(4f, 9f) * (Random.value > 0.5 ? 1f : -1f),
          0f,
          Random.Range(4f, 9f) * (Random.value > 0.5 ? 1f : -1f));
    }
  }
}
