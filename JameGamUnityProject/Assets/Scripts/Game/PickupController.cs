using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Animations;

namespace JameGam {
  public sealed class PickupController : MonoBehaviour {
    [field: SerializeField, Header("Attach")]
    public Transform AttachPoint { get; private set; }

    [field: SerializeField]
    public List<Interactable> HeldItems { get; private set; } = new();

    [field: SerializeField, Header("PutDownHeldItem")]
    public Vector3 ThrowForce { get; set; } = new(4f, 7f, 0f);

    public bool PickUpPresent(PresentInteractable interactable) {
      if (HeldItems.Count > 0) {
        return false;
      }

      ParentConstraint constraint = interactable.ParentConstraint;
      constraint.AddSource(new() { sourceTransform = AttachPoint, weight = 1f });
      constraint.constraintActive = true;

      interactable.CanInteract = false;

      if (interactable.TryGetComponent(out Collider collider)) {
        collider.enabled = false;
      }

      Debug.Log($"Picking up present: {interactable.name} ({interactable.transform.position})");
      HeldItems.Add(interactable);

      return true;
    }

    public bool PutDownHeldItem() {
      if (HeldItems.Count <= 0) {
        return false;
      }

      Interactable interactable = HeldItems[0];
      HeldItems.RemoveAt(0);

      Debug.Log($"Putting down item: {interactable.name}");

      if (interactable.TryGetComponent(out ParentConstraint constraint)) {
        constraint.RemoveSource(0);
        constraint.constraintActive = false;
      }

      interactable.CanInteract = true;

      if (interactable.TryGetComponent(out Collider collider)) {
        collider.enabled = true;
      }

      if (interactable.TryGetComponent(out Rigidbody rigidbody)) {
        rigidbody.AddForce(transform.forward * ThrowForce.x + transform.up * ThrowForce.y, ForceMode.VelocityChange);
      }

      return true;
    }
  }
}
