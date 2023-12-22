using UnityEditor;

using UnityEngine;

namespace JameGam.UI.Editor {
  [CustomEditor(typeof(ScorePanelController))]
  public sealed class ScorePanelControllerEditor : UnityEditor.Editor {
    ScorePanelController _controller;

    private void OnEnable() {
      _controller = (ScorePanelController) target;
    }

    public override void OnInspectorGUI() {
      base.OnInspectorGUI();
      EditorGUILayout.Separator();

      EditorGUILayout.LabelField(nameof(ScorePanelControllerEditor), EditorStyles.boldLabel);
      EditorGUILayout.Separator();

      DrawPanelControls();
      EditorGUILayout.Separator();

      DrawScoreControls();
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

    int _targetScore;

    private void DrawScoreControls() {
      GUILayout.BeginVertical("Score Controls", GUI.skin.window);
      GUILayout.BeginHorizontal();

      _targetScore = EditorGUILayout.IntField(_targetScore, GUILayout.Width(100f));

      if (GUILayout.Button("SetCurrentScore")) {
        _controller.SetCurrentScore(_targetScore);
      }

      GUILayout.EndHorizontal();
      GUILayout.EndVertical();
    }
  }
}
