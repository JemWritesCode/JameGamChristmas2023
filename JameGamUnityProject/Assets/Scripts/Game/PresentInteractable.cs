using UnityEngine;
using UnityEngine.Animations;

namespace JameGam {
  public sealed class PresentInteractable : Interactable {
    [field: SerializeField, Header("Pickup")]
    public ParentConstraint ParentConstraint { get; private set; }

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
        pickupController.PickUpPresent(this);
      }
    }
  }
}
