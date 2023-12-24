using DG.Tweening;

using UnityEngine;
using UnityEngine.EventSystems;

namespace JameGam.UI {
  public sealed class SfxOnHover : MonoBehaviour, IPointerEnterHandler {
    [field: SerializeField]
    public AudioSource SfxAudioSource { get; private set; }

    [field: SerializeField]
    public AudioClip OnHoverSfx { get; private set; }

    Tweener _onHoverSfxTweener;

    private void Awake() {
      _onHoverSfxTweener = SfxAudioSource.DOPlayOneShot(OnHoverSfx).Pause().SetAutoKill(false);
    }

    public void OnPointerEnter(PointerEventData eventData) {
      if (!_onHoverSfxTweener.IsPlaying()) {
        _onHoverSfxTweener.Restart();
      }
    }
  }
}
