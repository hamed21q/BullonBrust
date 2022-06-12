using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class FarsiWriterEditorWindow : EditorWindow
{
    private string currentText;
    private string convertedText;

    [MenuItem("Window/Farsi Writer")]
    private static void ShowWindow()
    {
        var window = GetWindow<FarsiWriterEditorWindow>();
        window.titleContent = new GUIContent("Farsi Writer");
        window.Show();
    }

    private void OnGUI()
    {
        currentText = EditorGUILayout.TextArea(currentText, GUILayout.Height(Screen.height / 2 - 30));
        EditorGUILayout.BeginHorizontal(GUILayout.Height(30));
        if (GUILayout.Button("Convert", GUILayout.Height(30)))
        {
            convertedText = FarsiWriter.Convert(currentText);
        }
        var text = Selection.activeGameObject?.GetComponent<TMPro.TMP_Text>();
        if (text != null)
        {
            if (GUILayout.Button($"Set {text.name}", GUILayout.Height(30)))
            {
                convertedText = FarsiWriter.Convert(currentText);
                Undo.RegisterCompleteObjectUndo(text, "Set Farsi Text");
                Undo.FlushUndoRecordObjects();
                text.text = convertedText;
                EditorUtility.SetDirty(text);
            }
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.TextArea(convertedText, GUILayout.Height(Screen.height / 2 - 30));
    }

    private void OnSelectionChange()
    {
        Repaint();
    }
}