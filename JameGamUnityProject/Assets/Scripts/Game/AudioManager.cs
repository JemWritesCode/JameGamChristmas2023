using UnityEngine;

namespace JameGam {
  public sealed class AudioManager : MonoBehaviour {
    [field: SerializeField]
    public AudioListener AudioListener { get; private set; }

    private static AudioManager _instance;

    public static AudioManager Instance {
      get {
        if (!_instance) {
          _instance = FindObjectOfType<AudioManager>();

          if (!_instance) {
            GameObject audioManager = new("AudioManager");
            _instance = audioManager.AddComponent<AudioManager>();  
          }
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
