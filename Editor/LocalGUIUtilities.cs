using System;
using UnityEditor;
using UnityEngine;

namespace Internal.Editor
{
    public static class LocalGUIUtilities
    {
        private const int Spacing = 20;
        
        public static void Space() => GUILayout.Space(Spacing);
        
        public static bool FriendlyReminder(string text) => EditorUtility.DisplayDialog("Friendly Reminder", text, "Yes", "No");
        
        public static void ProgressBar(string text, float value) => EditorUtility.DisplayProgressBar("Progress", text, value);

        public static void FinishProgressBar(string text)
        {
            EditorUtility.DisplayDialog("Progress", text, "Ok");
            EditorUtility.ClearProgressBar();
        }

        public static void BeginScroll(ref Vector2 position) => position = GUILayout.BeginScrollView(position);
        
        public static void EndScroll() => GUILayout.EndScrollView();
        
        public static void DrawSprite(Sprite sprite) => GUI.DrawTexture(sprite.rect, sprite.texture);
        
        public static bool Button(string text, Func<bool> enableCallback = null)
        {
            GUI.enabled = enableCallback == null || enableCallback.Invoke();
            bool clickedButton = GUILayout.Button(text);
            GUI.enabled = true;
            return clickedButton;
        }
        
        public static void Label(string text, bool isBold = true)
        {
            if (isBold)
                EditorGUILayout.LabelField(text, EditorStyles.boldLabel);
            else
                EditorGUILayout.LabelField(text);
        }

        public static void DrawArrayFields(ref bool[] array, Func<int, string> labelCallback, int startDelta = 0, int endDelta = 0)
        {
            for (int fieldIndex = startDelta; fieldIndex < array.Length - endDelta; fieldIndex++)
                DrawArrayField(ref array, fieldIndex, labelCallback);
        }

        private static void DrawArrayField(ref bool[] array, int index, Func<int, string> labelCallback) =>
            array[index] = EditorGUILayout.Toggle(labelCallback.Invoke(index), array[index]);
    }
}