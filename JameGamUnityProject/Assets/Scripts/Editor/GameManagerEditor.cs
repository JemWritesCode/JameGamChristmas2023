using UnityEditor;

using UnityEngine;

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

      DrawManagerControls();
    }

    readonly GameSession _gameSession =
        new() {
          CurrentScore = 0,
          CurrentTimer = 300f,
        };

    private void DrawManagerControls() {
      _gameSession.CurrentScore = EditorGUILayout.FloatField("CurrentScore", _gameSession.CurrentScore);
      _gameSession.CurrentTimer = EditorGUILayout.FloatField("CurrentTimer", _gameSession.CurrentTimer);

      EditorGUILayout.Separator();

      if (GUILayout.Button("StartNewGame")) {
        _manager.StartNewGame(_gameSession);
      }

      EditorGUILayout.BeginHorizontal();

      if (GUILayout.Button("PauseGame")) {
        _manager.PauseGame();
      }

      if (GUILayout.Button("ResumeGame")) {
        _manager.ResumeGame();
      }

      EditorGUILayout.EndHorizontal();
    }
  }
}
