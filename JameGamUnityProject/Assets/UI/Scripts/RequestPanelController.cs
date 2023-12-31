using System.Collections.Generic;

using DG.Tweening;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace JameGam.UI {
  public sealed class RequestPanelController : MonoBehaviour {
    [field: SerializeField, Header("Panel")]
    public RectTransform Panel { get; private set; }

    [field: SerializeField]
    public CanvasGroup PanelCanvasGroup { get; private set; }

    [field: SerializeField, Header("Product")]
    public RectTransform ProductTransform { get; private set; }

    [field: SerializeField]
    public TMP_Text ProductTitle { get; private set; }

    [field: SerializeField]
    public Image ProductBackdrop { get; private set; }

    [field: SerializeField]
    public Image ProductIcon { get; private set; }

    [field: SerializeField, Header("Parts")]
    public RectTransform PartsRow { get; private set; }

    [field: SerializeField]
    public PartSlotController PartSlotTemplate { get; private set; }

    public bool IsPanelVisible { get; private set; }

    private void Awake() {
      ResetPanel();
    }

    public void ResetPanel() {
      PanelCanvasGroup.alpha = 0f;
      PanelCanvasGroup.blocksRaycasts = false;
      IsPanelVisible = false;

      PartSlotTemplate.gameObject.SetActive(false);
    }

    public void ShowPanel() {
      Panel.DOComplete(withCallbacks: true);

      PanelCanvasGroup.blocksRaycasts = true;
      IsPanelVisible = true;

      DOTween.Sequence()
          .SetTarget(Panel)
          .Insert(0f, PanelCanvasGroup.DOFade(1f, 0.25f).From(0f, true))
          .Insert(0.15f, ProductTitle.DOFade(1f, 0.5f).From(0f, true))
          .Insert(0.25f, ProductIcon.DOFade(1f, 0.5f).From(0f, true));
    }

    public void HidePanel() {
      Panel.DOComplete(withCallbacks: true);

      PanelCanvasGroup.blocksRaycasts = false;
      IsPanelVisible = false;

      DOTween.Sequence()
          .SetTarget(Panel)
          .Insert(0f, PanelCanvasGroup.DOFade(0f, 0.25f));
    }

    public List<PartSlotController> PartSlots { get; private set; } = new();

    public void SetPartSlots(int count) {
      ClearPartSlots();

      for (int i = 0; i < count; i++) {
        PartSlotController partSlot = Instantiate(PartSlotTemplate, PartsRow.transform);
        partSlot.gameObject.SetActive(true);
        PartSlots.Add(partSlot);
      }

      Panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, CalculatePanelWidth());
    }

    private void ClearPartSlots() {
      for (int i = 0; i < PartSlots.Count; i++) {
        Destroy(PartSlots[i].gameObject);
      }

      PartSlots.Clear();
    }

    [field: SerializeField, Header("PanelSizing")]
    public float PanelWidthMinimum { get; private set; } = 200f;

    [field: SerializeField]
    public float PartsRowPadding { get; private set; } = 50f;

    [field: SerializeField]
    public float PartSlotWidth { get; private set; } = 60f;

    [field: SerializeField]
    public float PartSlotSpacing { get; private set; } = 10f;

    private float CalculatePanelWidth() {
      int slotCount = PartSlots.Count;
      float slotSpacing = Mathf.Max(slotCount - 1, 0) * PartSlotSpacing;

      return Mathf.Max(PanelWidthMinimum, (slotCount * PartSlotWidth) + slotSpacing + PartsRowPadding);
    }
  }
}
