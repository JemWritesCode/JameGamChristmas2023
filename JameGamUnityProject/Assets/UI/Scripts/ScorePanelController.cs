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

      DOTween.Sequence()
          .SetTarget(Panel)
          .Insert(0f, PanelCanvasGroup.DOFade(1f, 0.25f))
          .OnComplete(() => {
            PanelCanvasGroup.blocksRaycasts = true;
            IsPanelVisible = true;
          });
    }

    public void HidePanel() {
      Panel.DOComplete(withCallbacks: true);

      DOTween.Sequence()
          .SetTarget(Panel)
          .Insert(0f, PanelCanvasGroup.DOFade(0f, 0.25f))
          .OnComplete(() => {
            PanelCanvasGroup.blocksRaycasts = false;
            IsPanelVisible = false;
          });
    }

    public void SetCurrentScore(int targetScore) {
      ScoreLabel.DOComplete(withCallbacks: true);

      DOTween.Sequence()
          .SetTarget(ScoreLabel)
          .Insert(0f, ScoreLabel.DOCounter(CurrentScore, targetScore, 1f))
          .Insert(0f, ScoreLabel.transform.DOPunchPosition(new(0f, 2.5f, 0f), 1.25f, 3, 1))
          .Insert(0f, ScoreIcon.transform.DOPunchScale(Vector3.one * 0.15f, 1.25f, 5, 0f))
          .InsertCallback(0f, () => SfxAudioSource.PlayOneShot(ScoreIncreaseSfx));

      CurrentScore = targetScore;
    }
  }
}
