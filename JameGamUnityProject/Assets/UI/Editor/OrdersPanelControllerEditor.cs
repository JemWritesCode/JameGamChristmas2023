using System.Linq;

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

      DrawPanelControls();
      EditorGUILayout.Separator();

      DrawProductRequestControls();
    }

    private void DrawPanelControls() {
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

    Recipe _recipe;

    private void DrawProductRequestControls() {
      GUILayout.BeginVertical("Product Request Controls", GUI.skin.window);

      _recipe = SingleLineObjectField("Recipe", _recipe);

      using (new EditorGUI.DisabledScope(!Application.isPlaying)) {
        if (GUILayout.Button("Add Request")) {
          _controller.AddProductRequest(
              _recipe.RecipeName, _recipe.RecipeIcon, _recipe.ItemsNeeded.Select(item => item.ItemIcon).ToArray());
        }
      }

      GUILayout.EndVertical();
    }

    private T SingleLineObjectField<T>(string fieldLabel, T value) where T : Object {
      return (T) EditorGUILayout.ObjectField(
          fieldLabel, value, typeof(T), true, GUILayout.Height(EditorGUIUtility.singleLineHeight));
    }
  }
}
