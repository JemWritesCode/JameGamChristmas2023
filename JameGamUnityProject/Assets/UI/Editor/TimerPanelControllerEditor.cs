using UnityEditor;

using UnityEngine;

namespace JameGam.UI.Editor {
  [CustomEditor(typeof(TimerPanelController))]
  public sealed class TimerPanelControllerEditor : UnityEditor.Editor {
    TimerPanelController _controller;

    private void OnEnable() {
      _controller = (TimerPanelController) target;
    }

    public override void OnInspectorGUI() {
      base.OnInspectorGUI();
      EditorGUILayout.Separator();

      EditorGUILayout.LabelField(nameof(TimerPanelControllerEditor), EditorStyles.boldLabel);
      EditorGUILayout.Separator();

      DrawPanelControls();
      EditorGUILayout.Separator();

      DrawTimerControls();
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

    float _timeInSeconds = 300f;

    private void DrawTimerControls() {
      GUILayout.BeginVertical("Timer Controls", GUI.skin.window);

      _timeInSeconds = EditorGUILayout.FloatField("timeInSeconds", _timeInSeconds);

      GUILayout.BeginHorizontal();

      using (new EditorGUI.DisabledScope(!Application.isPlaying)) {
        if (GUILayout.Button("StartTimer")) {
          _controller.StartTimer(_timeInSeconds);
        }

        if (GUILayout.Button("PauseTimer")) {
          _controller.PauseTimer();
        }

        if (GUILayout.Button("ResumeTimer")) {
          _controller.ResumeTimer();
        }
      }

      GUILayout.EndHorizontal();
      GUILayout.EndVertical();
    }
  }
}
