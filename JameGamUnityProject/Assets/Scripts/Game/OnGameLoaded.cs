using DG.Tweening;

using UnityEngine;

namespace JameGam {
  public class OnGameLoaded : MonoBehaviour {
    private void Start() {
      DOTween.Sequence()
          .InsertCallback(1f, () => GameManager.Instance.StartNewGame());
    }
  }
}
