#if UNITY_EDITOR
using Gaskellgames.EditorOnly;
using UnityEditor;
using UnityEngine;

namespace Gaskellgames.InputEventSystem.EditorOnly
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    [CustomEditor(typeof(InputEvent)), CanEditMultipleObjects]
    public class InputEventEditor : GGEditor
    {
        #region Serialized Properties / OnEnable

        private InputEvent targetAsType;
        
        private SerializedProperty inputAction;
        private SerializedProperty valueType;
        private SerializedProperty threshold;
        private SerializedProperty isPressed;
        
        private SerializedProperty OnPressed;
        private SerializedProperty OnHeld;
        private SerializedProperty OnReleased;
        
        private static int selectedTab = 0;
        private string[] tabs = new[] { "Settings", "Events" };
        private int settingsTab = 0;
        private int eventsTab = 1;
        
        private const string pathRefName = "Input Event System";
        private const string bannerRelativePath = "/Editor/Icons/inspectorBanner_InputEventSystem.png";
        private Texture banner;

        private void OnEnable()
        {
            if (!targetAsType) { targetAsType = target as InputEvent; }
            banner = EditorWindowUtility.LoadInspectorBanner(pathRefName, bannerRelativePath);
            
            inputAction = serializedObject.FindProperty("inputAction");
            valueType = serializedObject.FindProperty("valueType");
            threshold = serializedObject.FindProperty("threshold");
            isPressed = serializedObject.FindProperty("isPressed");
            
            OnPressed = serializedObject.FindProperty("OnPressed");
            OnHeld = serializedObject.FindProperty("OnHeld");
            OnReleased = serializedObject.FindProperty("OnReleased");
        }

        #endregion
        
        //----------------------------------------------------------------------------------------------------
        
        #region OnInspectorGUI

        public override void OnInspectorGUI()
        {
            // get & update references
            serializedObject.Update();

            // draw banner if turned on in Gaskellgames settings
            EditorWindowUtility.TryDrawBanner(banner);
            
            // draw inspector
            selectedTab = GUILayout.Toolbar(selectedTab, tabs);
            EditorGUILayout.Space();
            if (selectedTab == settingsTab)
            {
                EditorGUILayout.PropertyField(inputAction);
                EditorGUILayout.PropertyField(valueType);
                if (IsValueTypeAxis(valueType.enumValueIndex)) { EditorGUILayout.PropertyField(threshold); }
                EditorGUILayout.PropertyField(isPressed);
            }
            else if (selectedTab == eventsTab)
            {
                EditorGUILayout.PropertyField(OnPressed);
                EditorGUILayout.PropertyField(OnHeld);
                EditorGUILayout.PropertyField(OnReleased);
            }

            // apply reference changes
            serializedObject.ApplyModifiedProperties();
        }

        #endregion
        
        //----------------------------------------------------------------------------------------------------

        private bool IsValueTypeAxis(int enumValueIndex)
        {
            return enumValueIndex == InputSystemExtensions.InputActionValueType.Float.ToInt() ||
                   enumValueIndex == InputSystemExtensions.InputActionValueType.Vector2.ToInt();
        }
        
    } // class end
}

#endif
