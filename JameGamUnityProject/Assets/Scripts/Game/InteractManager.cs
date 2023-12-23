using System;

using Facepunch;

using UnityEngine;

namespace JameGam {
  public sealed class InteractManager : MonoBehaviour {
    [field: SerializeField]
    public GameObject InteractAgent { get; set; }

    [field: SerializeField, Min(0f)]
    public float InteractRange { get; set; } = 4f;

    [field: SerializeField]
    public bool CanInteract { get; set; } = true;

    [field: SerializeField]
    public Interactable ClosestInteractable { get; private set; }

    static InteractManager _instance;

    public static InteractManager Instance {
      get {
        if (!_instance) {
          _instance = FindObjectOfType<InteractManager>();
        }

        return _instance;
      }
    }

    private void Awake() {
      if (_instance) {
        Destroy(this);
      } else {
        _instance = this;
      }
    }

    private void FixedUpdate() {
      Interactable interactable =
          CanInteract && InteractAgent ? GetClosestInteractable(InteractAgent.transform, InteractRange) : default;

      if (interactable != ClosestInteractable) {
        if (ClosestInteractable && ClosestInteractable.HighlightRenderer) {
          Highlight.RemoveRenderer(ClosestInteractable.HighlightRenderer);
        }
        
        if (interactable && interactable.HighlightRenderer) {
          Highlight.AddRenderer(interactable.HighlightRenderer);
        }

        Highlight.Rebuild();

        ClosestInteractable = interactable;
      }
    }

    readonly RaycastHit[] _raycastHits = new RaycastHit[20];
    readonly float[] _hitDistanceCache = new float[20];

    private Interactable GetClosestInteractable(Transform origin, float range) {
      int count =
          Physics.SphereCastNonAlloc(
              origin.position,
              0.25f,
              origin.forward,
              _raycastHits,
              range,
              -1,
              QueryTriggerInteraction.Ignore);

      if (count <= 0) {
        return default;
      }

      for (int i = 0; i < count; i++) {
        _hitDistanceCache[i] = _raycastHits[i].distance;
      }

      Array.Sort(_hitDistanceCache, _raycastHits, 0, count);

      for (int i = 0; i < count; i++) {
        RaycastHit raycastHit = _raycastHits[i];
        Interactable interactable = raycastHit.collider.GetComponentInParent<Interactable>();

        if (interactable && interactable.enabled && raycastHit.distance <= interactable.InteractRange) {
          return interactable;
        }
      }

      return default;
    }
  }
}
