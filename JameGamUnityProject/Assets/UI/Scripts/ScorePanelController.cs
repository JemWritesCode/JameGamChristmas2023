using DG.Tweening;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace JameGam.UI {
  public sealed class ScorePanelController : MonoBehaviour {
    [field: SerializeField, Header("Panel")]
    public RectTransform Panel { get; private set; }

    [field: SerializeField]
    public CanvasGroup PanelCanvasGroup { get; private set; }

    [field: SerializeField, Header("Score")]
    public TMP_Text ScoreLabel { get; private set; }

    [field: SerializeField]
    public Image ScoreIcon { get; private set; }

    [field: SerializeField, Header("Sfx")]
    public AudioSource SfxAudioSource { get; private set; }

    [field: SerializeField]
    public AudioClip ScoreIncreaseSfx { get; private set; }

    [field: SerializeField]
    public AudioClip ScoreIconClickedSfx { get; private set; }

    public bool IsPanelVisible { get; private set; }
    public int CurrentScore { get; private set; }

    private void Awake() {
      ResetPanel();
    }

    private void Start() {
      ShowPanel();
    }

    public void ResetPanel() {
      PanelCanvasGroup.alpha = 0f;
      PanelCanvasGroup.blocksRaycasts = false;
      IsPanelVisible = false;

      ScoreLabel.text = "0";
      CurrentScore = 0;
    }

    public void ShowPanel() {
      Panel.DOComplete(withCallbacks: true);

      IsPanelVisible = true;
      PanelCanvasGroup.blocksRaycasts = true;

      DOTween.Sequence()
          .SetTarget(Panel)
          .Insert(0f, PanelCanvasGroup.DOFade(1f, 0.25f));
    }

    public void HidePanel() {
      Panel.DOComplete(withCallbacks: true);

      IsPanelVisible = false;
      PanelCanvasGroup.blocksRaycasts = false;

      DOTween.Sequence()
          .SetTarget(Panel)
          .Insert(0f, PanelCanvasGroup.DOFade(0f, 0.25f));
    }

    public void SetCurrentScore(int targetScore) {
      ScoreLabel.DOComplete(withCallbacks: true);

      DOTween.Sequence()
          .SetTarget(ScoreLabel)
          .Insert(0f, ScoreLabel.DOCounter(CurrentScore, targetScore, 0.5f))
          .Insert(0f, ScoreLabel.transform.DOPunchPosition(new(0f, 2.5f, 0f), 1f, 3, 1f))
          .Insert(0f, ScoreIcon.transform.DOPunchScale(Vector3.one * 0.15f, 1f, 5, 0f))
          .Insert(0f, SfxAudioSource.DOPlayOneShot(ScoreIncreaseSfx));

      CurrentScore = targetScore;
    }

    public void OnScoreIconClicked() {
      ScoreIcon.DOComplete(withCallbacks: true);

      DOTween.Sequence()
          .SetTarget(ScoreIcon)
          .Insert(0f, ScoreIcon.transform.DOPunchScale(Vector3.one * 0.15f, 0.5f, 10, 1f))
          .Insert(0f, SfxAudioSource.DOPlayOneShot(ScoreIconClickedSfx));
    }
  }
}
