using UnityEditor;

namespace JameGam.Editor {
  [CustomEditor(typeof(InteractManager))]
  public sealed class InteractManagerEditor : UnityEditor.Editor {
    InteractManager _manager;

    private void OnEnable() {
      _manager = (InteractManager) target;
    }

    public override void OnInspectorGUI() {
      base.OnInspectorGUI();
      EditorGUILayout.Separator();

      EditorGUILayout.LabelField(nameof(InteractManagerEditor), EditorStyles.boldLabel);
      EditorGUILayout.Separator();

      DrawManagerControls();
    }

    private void DrawManagerControls() {
      // ...
    }
  }
}
