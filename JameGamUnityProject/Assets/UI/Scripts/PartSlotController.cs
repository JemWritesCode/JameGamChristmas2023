using UnityEngine;
using UnityEngine.UI;

namespace JameGam.UI {
  public class PartSlotController : MonoBehaviour {
    [field: SerializeField]
    public RectTransform PartSlot { get; private set; }

    [field: SerializeField]
    public Image SlotBackdrop { get; private set; }

    [field: SerializeField]
    public Image SlotIcon { get; private set; }
  }
}
