using System.Collections.Generic;
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

    string _productTitle = string.Empty;
    Sprite _productIcon;
    readonly Sprite[] _requestPartIcons = new Sprite[4];

    private void DrawProductRequestControls() {
      GUILayout.BeginVertical("Product Request Controls", GUI.skin.window);

      _productTitle = EditorGUILayout.TextField("ProductTitle", _productTitle);
      _productIcon = SingleLineObjectField("ProductIcon", _productIcon);

      for (int i = 0; i < _requestPartIcons.Length; i++) {
        _requestPartIcons[i] = SingleLineObjectField($"PartIcon{i + 1}", _requestPartIcons[i]);
      }

      using (new EditorGUI.DisabledScope(!Application.isPlaying)) {
        if (GUILayout.Button("Add Request")) {
          _controller.AddProductRequest(_productTitle, _productIcon, _requestPartIcons.Where(icon => icon).ToArray());
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
