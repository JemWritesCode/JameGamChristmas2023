using UnityEditor;

using UnityEngine;

namespace JameGam.UI.Editor {
  [CustomEditor(typeof(OrdersPanelController))]
  public sealed class OrdersPanelControllerEditor : UnityEditor.Editor {
    OrdersPanelController _controller;

    private void OnEnable() {
      _controller = (OrdersPanelController) target;
    }

    public override void OnInspectorGUI() {
      base.OnInspectorGUI();

      EditorGUILayout.Separator();
      EditorGUILayout.LabelField(nameof(OrdersPanelControllerEditor), EditorStyles.boldLabel);
      EditorGUILayout.Separator();

      GUILayout.BeginHorizontal("PanelControls", GUI.skin.window);

      using (new EditorGUI.DisabledScope(!Application.isPlaying)) {
        if (GUILayout.Button("ShowPanel")) {
          _controller.ShowPanel();
        }

        if (GUILayout.Button("HidePanel")) {
          _controller.HidePanel();
        }
      }

      GUILayout.EndHorizontal();
    }
  }
}
