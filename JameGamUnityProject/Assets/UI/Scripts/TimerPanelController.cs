using DG.Tweening;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace JameGam.UI {
  public sealed class TimerPanelController : MonoBehaviour {
    [field: SerializeField, Header("Panel")]
    public RectTransform Panel { get; private set; }

    [field: SerializeField]
    public CanvasGroup PanelCanvasGroup { get; private set; }

    [field: SerializeField, Header("Timer")]
    public TMP_Text TimerLabel { get; private set; }

    [field: SerializeField]
    public Image TimerIcon { get; private set; }

    [field: SerializeField, Header("SFX")]
    public AudioSource SfxAudioSource { get; private set; }

    [field: SerializeField]
    public AudioClip StartTimerSfx { get; private set; }

    [field: SerializeField]
    public AudioClip PauseTimerSfx { get; private set; }

    [field: SerializeField]
    public AudioClip ResumeTimerSfx { get; private set; }

    [field: SerializeField]
    public AudioClip TimerIconClickedSfx { get; private set; }

    public float CurrentTimer { get; private set; }
    public bool IsPanelVisible { get; private set; }

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

      TimerLabel.text = "-:--";
      CurrentTimer = 0f;
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

    Tweener _timerTweener;

    public void StartTimer(float timeInSeconds) {
      _timerTweener?.Complete(withCallbacks: true);

      _timerTweener =
          DOVirtual
              .Float(timeInSeconds, 0f, timeInSeconds, SetCurrentTimer)
              .SetEase(Ease.Linear);

      SfxAudioSource.PlayOneShot(StartTimerSfx);
    }

    public void PauseTimer() {
      _timerTweener?.Pause();

      TimerLabel.DOComplete(withCallbacks: true);
      TimerLabel.transform.DOPunchPosition(new(0f, 2.5f, 0f), 1f, 3, 1f);

      SfxAudioSource.PlayOneShot(PauseTimerSfx);
    }

    public void ResumeTimer() {
      _timerTweener?.Play();

      TimerLabel.DOComplete(withCallbacks: true);
      TimerLabel.transform.DOPunchPosition(new(0f, -2.5f, 0f), 1f, 3, 1f);

      SfxAudioSource.PlayOneShot(ResumeTimerSfx);
    }

    private readonly char[] _timerLabelChars = new char[5];

    private void SetCurrentTimer(float timeInSeconds) {
      CurrentTimer = timeInSeconds;
      SecondsToCharArray(CurrentTimer, _timerLabelChars);
      TimerLabel.SetText(_timerLabelChars);
    }

    // https://forum.unity.com/threads/ui-timer-without-garbage-collection-possible.663874/#post-5816905
    private void SecondsToCharArray(float timeInSeconds, char[] array) {
      int minutes = (int) (timeInSeconds / 60f);
      array[0] = (char) (48 + (minutes / 10));
      array[1] = (char) (48 + (minutes % 10));
      array[2] = ':';

      int seconds = (int) (timeInSeconds - minutes * 60);
      array[3] = (char) (48 + seconds / 10);
      array[4] = (char) (48 + seconds % 10);
    }

    public void OnTimerIconClicked() {
      TimerIcon.DOComplete(withCallbacks: true);

      DOTween.Sequence()
          .SetTarget(TimerIcon)
          .Insert(0f, TimerIcon.transform.DOPunchScale(Vector3.one * 0.15f, 0.5f, 10, 1f))
          .Insert(0f, TimerIcon.transform.DORotate(new(0f, 0f, -15f), 0.5f).SetRelative(true))
          .Insert(0f, SfxAudioSource.DOPlayOneShot(TimerIconClickedSfx));
    }
  }
}
