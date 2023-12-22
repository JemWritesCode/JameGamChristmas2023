using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DG.Tweening;

using TMPro;

namespace JameGam {
  public static class DOTweenExtensions {
    public static TweenerCore<int, int, NoOptions> DOCounter(
        this TMP_Text target, int fromValue, int toValue, float duration) {
      int value = fromValue;

      int GetValue() => value;

      void SetValue(int x) {
        value = x;
        target.text = value.ToString();
      };

      TweenerCore<int, int, NoOptions> tweener = DOTween.To(GetValue, SetValue, toValue, duration);
      tweener.SetTarget(target);

      return tweener;
    }
  }
}
