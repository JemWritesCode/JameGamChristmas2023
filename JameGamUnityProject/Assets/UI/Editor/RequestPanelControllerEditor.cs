using UnityEditor;

using UnityEngine;

namespace JameGam.UI.Editor {
  [CustomEditor(typeof(RequestPanelController))]
  public sealed class RequestPanelControllerEditor : UnityEditor.Editor {
    RequestPanelController _controller;

    private void OnEnable() {
      _controller = (RequestPanelController) target;
    }

    public override void OnInspectorGUI() {
      base.OnInspectorGUI();

      EditorGUILayout.Separator();
      EditorGUILayout.LabelField(nameof(RequestPanelControllerEditor), EditorStyles.boldLabel);
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

      PartSlotControls();
    }

    private void PartSlotControls() {
      GUILayout.BeginHorizontal("PartSlotControls", GUI.skin.window);

      using (new EditorGUI.DisabledScope(!Application.isPlaying)) {
        int count = EditorGUILayout.IntSlider(_controller.PartSlots.Count, 0, 4);

        if (count != _controller.PartSlots.Count) {
          _controller.SetPartSlots(count);
        }
      }

      GUILayout.EndHorizontal();
    }
  }
}
