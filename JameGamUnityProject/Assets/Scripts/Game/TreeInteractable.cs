using DG.Tweening;

using UnityEngine;

namespace JameGam {
  public sealed class TreeInteractable : Interactable {
    public void ShakeTree() {
      transform.DOComplete(withCallbacks: true);

      Vector3 rotation = new(
          Random.Range(8f, 16f) * (Random.value > 0.5 ? 1f : -1f),
          0f,
          Random.Range(8f, 16f) * (Random.value > 0.5 ? 1f : -1f));

      transform.DOPunchRotation(rotation, Random.Range(1f, 2f), Random.Range(6, 10), 1f);
    }
  }
}
