using DG.Tweening;

using UnityEngine;

namespace JameGam {
  public sealed class PresentInteractable : Interactable {
    [field: SerializeField, Header("SFX")]
    public AudioSource SfxAudioSource { get; private set; }

    [field: SerializeField]
    public AudioClip PickupSfx { get; private set; }

    private void Awake() {
      OnInteract.AddListener(PickupPresent);
    }

    public void PickupPresent(GameObject interactAgent) {
      if (interactAgent.TryGetComponent(out PickupController pickupController)) {
        SfxAudioSource.DOPlayOneShot(PickupSfx);
        pickupController.PickupPresent(this);
      }
    }
  }
}
