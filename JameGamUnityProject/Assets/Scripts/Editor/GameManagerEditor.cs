using UnityEditor;

namespace JameGam.Editor {
  [CustomEditor(typeof(GameManager))]
  public sealed class GameManagerEditor : UnityEditor.Editor {
    GameManager _manager;

    private void OnEnable() {
      _manager = (GameManager) target;
    }

    public override void OnInspectorGUI() {
      base.OnInspectorGUI();
      EditorGUILayout.Separator();

      EditorGUILayout.LabelField(nameof(GameManagerEditor), EditorStyles.boldLabel);
      EditorGUILayout.Separator();

      DrawManagerControls();
    }

    private void DrawManagerControls() {
      // ...
    }
  }
}
