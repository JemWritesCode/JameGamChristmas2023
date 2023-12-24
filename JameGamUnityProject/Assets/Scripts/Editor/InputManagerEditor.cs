using UnityEditor;

namespace JameGam.Editor {
  [CustomEditor(typeof(InputManager))]
  public sealed class InputManagerEditor : UnityEditor.Editor {
    InputManager _manager;

    private void OnEnable() {
      _manager = (InputManager) target;
    }

    public override void OnInspectorGUI() {
      base.OnInspectorGUI();
      EditorGUILayout.Separator();

      EditorGUILayout.LabelField(nameof(InputManagerEditor), EditorStyles.boldLabel);
      EditorGUILayout.Separator();

      DrawManagerControls();
    }

    private void DrawManagerControls() {
      // ...
    }
  }
}
