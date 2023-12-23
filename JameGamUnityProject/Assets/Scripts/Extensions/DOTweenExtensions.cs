using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DG.Tweening;

using TMPro;

using UnityEngine;

namespace JameGam {
  public static class DOTweenExtensions {
    public static Tweener DOCounter(this TMP_Text target, int fromValue, int toValue, float duration) {
      return DOVirtual
          .Int(fromValue, toValue, duration, x => target.SetText(x.ToString()))
          .SetTarget(target);
    }

    public static Tweener DOPlayOneShot(this AudioSource target, AudioClip audioClip) {
      float duration = audioClip.length;

      return DOVirtual
          .Float(0f, duration, duration, EmptyFloatCallback)
          .OnStart(() => target.PlayOneShot(audioClip))
          .SetTarget(target);
    }

    static void EmptyFloatCallback(float x) {
      // ...
    }
  }
}
