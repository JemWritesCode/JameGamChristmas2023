using UnityEditor;

using UnityEngine;

namespace JameGam.UI.Editor {
  [CustomEditor(typeof(GameOverPanelController))]
  public sealed class GameOverPanelControllerEditor : UnityEditor.Editor {
    GameOverPanelController _controller;

    private void OnEnable() {
      _controller = (GameOverPanelController) target;
    }

    public override void OnInspectorGUI() {
      base.OnInspectorGUI();
      EditorGUILayout.Separator();

      EditorGUILayout.LabelField(nameof(GameOverPanelControllerEditor), EditorStyles.boldLabel);
      EditorGUILayout.Separator();

      DrawPanelControls();
      EditorGUILayout.Separator();
    }

    private void DrawPanelControls() {
      GUILayout.BeginHorizontal("Panel Controls", GUI.skin.window);

      using (new EditorGUI.DisabledScope(!Application.isPlaying)) {
        if (GUILayout.Button("Show Panel")) {
          _controller.ShowPanel();
        }

        if (GUILayout.Button("Hide Panel")) {
          _controller.HidePanel();
        }
      }

      GUILayout.EndHorizontal();
    }
  }
}
