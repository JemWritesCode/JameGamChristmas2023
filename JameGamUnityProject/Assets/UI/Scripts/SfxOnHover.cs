using UnityEngine;
using UnityEngine.EventSystems;

namespace JameGam.UI {
  public sealed class SfxOnHover : MonoBehaviour, IPointerEnterHandler {
    [field: SerializeField]
    public AudioSource SfxAudioSource { get; private set; }

    [field: SerializeField]
    public AudioClip OnHoverSfx { get; private set; }

    public void OnPointerEnter(PointerEventData eventData) {
      SfxAudioSource.PlayOneShot(OnHoverSfx);
    }
  }
}
