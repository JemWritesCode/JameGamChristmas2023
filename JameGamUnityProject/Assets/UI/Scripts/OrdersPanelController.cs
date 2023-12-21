using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;

namespace JameGam.UI {
  public sealed class OrdersPanelController : MonoBehaviour {
    [field: SerializeField, Header("OrdersPanel")]
    public RectTransform Panel { get; private set; }

    [field: SerializeField]
    public CanvasGroup PanelCanvasGroup { get; private set; }

    [field: SerializeField, Header("RequestPanel")]
    public RequestPanelController RequestPanelTemplate { get; private set; }

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

    public List<RequestPanelController> ProductRequests { get; private set; }

    public RequestPanelController AddProductRequest() {
      RequestPanelController request = Instantiate(RequestPanelTemplate, Panel.transform);
      request.gameObject.SetActive(true);

      return request;
    }
  }
}
