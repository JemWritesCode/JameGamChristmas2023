using UnityEditor;

namespace JameGam.Editor {
  [CustomEditor(typeof(UIManager))]
  public sealed class UIManagerEditor : UnityEditor.Editor {
    UIManager _manager;

    private void OnEnable() {
      _manager = (UIManager) target;
    }

    public override void OnInspectorGUI() {
      base.OnInspectorGUI();
      EditorGUILayout.Separator();

      EditorGUILayout.LabelField(nameof(UIManagerEditor), EditorStyles.boldLabel);
      EditorGUILayout.Separator();

      DrawManagerControls();
    }

    private void DrawManagerControls() {
      // ...
    }
  }
}
