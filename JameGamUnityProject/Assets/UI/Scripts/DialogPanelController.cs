using DG.Tweening;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace JameGam.UI {
  public sealed class DialogPanelController : MonoBehaviour {
    [field: SerializeField, Header("Panel")]
    public RectTransform Panel { get; private set; }

    [field: SerializeField]
    public CanvasGroup PanelCanvasGroup { get; private set; }

    [field: SerializeField, Header("Dialog")]
    public Image DialogImage { get; private set; }

    [field: SerializeField]
    public TMP_Text DialogText { get; private set; }

    [field: SerializeField]
    public Button ConfirmButton { get; private set; }

    [field: SerializeField, Header("SFX")]
    public AudioSource SfxAudioSource { get; private set; }

    [field: SerializeField]
    public AudioClip ShowPanelSfx { get; private set; }

    public bool IsPanelVisible { get; private set; }

    private void Awake() {
      ResetPanel();
    }

    public void ResetPanel() {
      PanelCanvasGroup.alpha = 0f;
      PanelCanvasGroup.blocksRaycasts = false;
      IsPanelVisible = false;
    }

    public void ShowPanel() {
      Panel.DOComplete(withCallbacks: true);

      PanelCanvasGroup.blocksRaycasts = true;
      IsPanelVisible = true;

      DOTween.Sequence()
          .SetTarget(Panel)
          .Insert(0f, PanelCanvasGroup.DOFade(1f, 0.25f))
          .Insert(0f, DialogImage.transform.DOPunchScale(Vector3.one * 0.05f, 0.5f, 0, 0f))
          .Insert(0f, DialogText.transform.DOMoveY(10f, 0.5f).From(isRelative: true))
          .Insert(0f, SfxAudioSource.DOPlayOneShot(ShowPanelSfx));
    }

    public void HidePanel() {
      Panel.DOComplete(withCallbacks: true);

      PanelCanvasGroup.blocksRaycasts = false;
      IsPanelVisible = false;

      DOTween.Sequence()
          .SetTarget(Panel)
          .Insert(0f, PanelCanvasGroup.DOFade(0f, 0.25f));
    }

    public DialogNode CurrentDialogNode { get; private set; }

    public void ShowDialogNode(DialogNode dialogNode) {
      CurrentDialogNode = dialogNode;
      DialogText.text = CurrentDialogNode.GetNextConversationText();
      ShowPanel();
    }

    public void OnConfirmButtonClicked() {
      if (!CurrentDialogNode) {
        HidePanel();
      }

      if (CurrentDialogNode.HasConversationText()) {
        ShowDialogNode(CurrentDialogNode);
      } else {
        HidePanel();
        CurrentDialogNode.OnNodeComplete();
      }
    }
  }
}
