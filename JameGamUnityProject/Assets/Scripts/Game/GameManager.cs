using UnityEngine;

namespace JameGam {
  public sealed class GameManager : MonoBehaviour {
    [field: SerializeField, Header("Player")]
    public GameObject CurrentPlayer { get; private set; }

    static GameManager _instance;

    public static GameManager Instance {
      get {
        if (!_instance) {
          _instance = FindObjectOfType<GameManager>();
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
  }
}