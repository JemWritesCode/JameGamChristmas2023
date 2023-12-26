using UnityEngine;

namespace JameGam {
  public sealed class PickupController : MonoBehaviour {
    public bool PickupPresent(PresentInteractable interactable) {
      Debug.Log($"Picking up present: {interactable.name} ({interactable.transform.position})");
      return true;
    }
  }
}
