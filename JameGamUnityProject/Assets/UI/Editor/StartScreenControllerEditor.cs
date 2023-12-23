using UnityEditor;

using UnityEngine;

namespace JameGam.UI.Editor {
  [CustomEditor(typeof(StartScreenController))]
  public sealed class StartScreenControllerEditor : UnityEditor.Editor {
    StartScreenController _controller;

    private void OnEnable() {
      _controller = (StartScreenController) target;
    }

    public override void OnInspectorGUI() {
      base.OnInspectorGUI();
      EditorGUILayout.Separator();

      EditorGUILayout.LabelField(nameof(StartScreenController), EditorStyles.boldLabel);
      EditorGUILayout.Separator();

      if (GUILayout.Button("AnimateIntro")) {
        _controller.AnimateIntro();
      }
    }
  }
}
