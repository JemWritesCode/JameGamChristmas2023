using System;

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

    [field: SerializeField, Header("Sfx")]
    public AudioSource SfxAudioSource { get; private set; }

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

    public void StartTimer(float timeInSeconds) {
      TimerLabel.DOComplete(withCallbacks: true);
      SetCurrentTimer(timeInSeconds);

      DOTween
          .To(GetCurrentTimer, SetCurrentTimer, 0f, timeInSeconds)
          .SetEase(Ease.Linear)
          .SetTarget(TimerLabel);
    }

    private readonly char[] _timerLabelChars = new char[5];
    private float _currentTimer = 0f;

    private float GetCurrentTimer() {
      return _currentTimer;
    }

    private void SetCurrentTimer(float timeInSeconds) {
      _currentTimer = timeInSeconds;

      SecondsToCharArray(_currentTimer, _timerLabelChars);
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
  }
}
