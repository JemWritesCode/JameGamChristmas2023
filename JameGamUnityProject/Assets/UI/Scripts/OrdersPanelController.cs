using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;

namespace JameGam.UI {
  public sealed class OrdersPanelController : MonoBehaviour {
    [field: SerializeField, Header("Panel")]
    public RectTransform Panel { get; private set; }

    [field: SerializeField]
    public CanvasGroup PanelCanvasGroup { get; private set; }

    [field: SerializeField, Header("RequestPanel")]
    public RequestPanelController RequestPanelTemplate { get; private set; }

    [field: SerializeField, Header("SFX")]
    public AudioSource SfxAudioSource { get; private set; }

    [field: SerializeField]
    public AudioClip AddRequestSfx { get; private set; }

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

      RequestPanelTemplate.gameObject.SetActive(false);
    }

    public void ShowPanel() {
      Panel.DOComplete(withCallbacks: true);

      PanelCanvasGroup.blocksRaycasts = true;
      IsPanelVisible = true;

      DOTween.Sequence()
          .SetTarget(Panel)
          .Insert(0f, PanelCanvasGroup.DOFade(1f, 0.25f));
    }

    public void HidePanel() {
      Panel.DOComplete(withCallbacks: true);

      PanelCanvasGroup.blocksRaycasts = false;
      IsPanelVisible = false;

      DOTween.Sequence()
          .SetTarget(Panel)
          .Insert(0f, PanelCanvasGroup.DOFade(0f, 0.25f));
    }

    public List<RequestPanelController> ProductRequests { get; private set; }

    public RequestPanelController AddProductRequest(
        string productTitle,
        Sprite productIcon,
        Sprite[] requestPartIcons) {
      RequestPanelController request = Instantiate(RequestPanelTemplate, Panel.transform);

      request.ProductTitle.text = productTitle;
      request.ProductIcon.sprite = productIcon;
      request.SetPartSlots(requestPartIcons.Length);

      for (int i = 0; i < requestPartIcons.Length; i++) {
        PartSlotController partSlot = request.PartSlots[i];
        partSlot.SlotIcon.sprite = requestPartIcons[i];
      }

      request.gameObject.SetActive(true);
      request.ShowPanel();

      SfxAudioSource.PlayOneShot(AddRequestSfx);

      return request;
    }
  }
}
