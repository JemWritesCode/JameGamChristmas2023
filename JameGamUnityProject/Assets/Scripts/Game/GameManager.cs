using System.Linq;

using JameGam.UI;

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

    private void Start() {
      if (!CurrentPlayer) {
        CurrentPlayer = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault();
      }
    }

    [field: SerializeField, Header("NewGame")]
    public float StartScore { get; private set; } = 0f;

    [field: SerializeField]
    public float StartTimer { get; private set; } = 300f;

    [field: SerializeField]
    public DialogNode StartDialogNode { get; private set; }

    public GameSession CurrentGameSession { get; private set; }

    public void StartNewGame() {
      StartNewGame(
          new() {
            CurrentScore = StartScore,
            CurrentTimer = StartTimer,
          });
    }

    public void StartNewGame(GameSession gameSession) {
      CurrentGameSession = gameSession;

      StartDialogNode.OnNodeCompleteCallbacks.Enqueue(_ => {
        UIManager.Instance.ScorePanel.SetCurrentScore(Mathf.RoundToInt(gameSession.CurrentScore));
        UIManager.Instance.TimerPanel.StartTimer(gameSession.CurrentTimer);
      });

      UIManager.Instance.DialogPanel.ShowDialogNode(StartDialogNode);
    }

    public void PauseGame() {
      UIManager.Instance.TimerPanel.PauseTimer();
    }

    public void ResumeGame() {
      UIManager.Instance.TimerPanel.ResumeTimer();
    }
  }
}
