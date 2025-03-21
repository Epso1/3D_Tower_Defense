#if UNITY_EDITOR
using Gaskellgames.EditorOnly;
using UnityEditor;
using UnityEngine;

namespace Gaskellgames.InputEventSystem.EditorOnly
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    [CustomEditor(typeof(GamepadCursor)), CanEditMultipleObjects]
    public class GamepadCursorEditor : GGEditor
    {
        #region Serialized Properties / OnEnable

        private GamepadCursor targetAsType;
        
        new SerializedProperty verboseLogs;
        
        private SerializedProperty autoHideCursor;
        private SerializedProperty cursorLockState;
        private SerializedProperty inputEventManager;
        
        private SerializedProperty cursorCanvas;
        private SerializedProperty cursorOffset;
        private SerializedProperty cursorImage;
        private SerializedProperty cursorSpeed;
        private SerializedProperty scrollSpeed;
        
        private SerializedProperty moveAxis;
        private SerializedProperty scrollAxis;
        private SerializedProperty leftButton;
        private SerializedProperty middleButton;
        private SerializedProperty rightButton;
        private SerializedProperty backButton;
        private SerializedProperty forwardButton;
        
        private static int selectedTab = 0;
        private string[] tabs = new[] { "Settings", "Debug" };
        private int settingsTab = 0;
        private int debugTab = 1;
        
        private const string pathRefName = "Input Event System";
        private const string bannerRelativePath = "/Editor/Icons/inspectorBanner_InputEventSystem.png";
        private Texture banner;

        private void OnEnable()
        {
            if (!targetAsType) { targetAsType = target as GamepadCursor; }
            banner = EditorWindowUtility.LoadInspectorBanner(pathRefName, bannerRelativePath);
            
            verboseLogs = serializedObject.FindProperty("verboseLogs");
            
            autoHideCursor = serializedObject.FindProperty("autoHideCursor");
            cursorLockState = serializedObject.FindProperty("cursorLockState");
            inputEventManager = serializedObject.FindProperty("inputEventManager");
            
            cursorCanvas = serializedObject.FindProperty("cursorCanvas");
            cursorOffset = serializedObject.FindProperty("cursorOffset");
            cursorImage = serializedObject.FindProperty("cursorImage");
            
            cursorSpeed = serializedObject.FindProperty("cursorSpeed");
            scrollSpeed = serializedObject.FindProperty("scrollSpeed");
            
            moveAxis = serializedObject.FindProperty("moveAxis");
            scrollAxis = serializedObject.FindProperty("scrollAxis");
            leftButton = serializedObject.FindProperty("leftButton");
            middleButton = serializedObject.FindProperty("middleButton");
            rightButton = serializedObject.FindProperty("rightButton");
            backButton = serializedObject.FindProperty("backButton");
            forwardButton = serializedObject.FindProperty("forwardButton");
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
                EditorGUILayout.PropertyField(autoHideCursor);
                EditorGUILayout.PropertyField(cursorLockState);
                EditorGUILayout.PropertyField(cursorCanvas);
                EditorGUILayout.PropertyField(cursorOffset);
                EditorGUILayout.PropertyField(cursorImage);
                EditorGUILayout.PropertyField(cursorSpeed);
                EditorGUILayout.PropertyField(scrollSpeed);
                EditorGUILayout.PropertyField(moveAxis);
                EditorGUILayout.PropertyField(scrollAxis);
                EditorGUILayout.PropertyField(leftButton);
                EditorGUILayout.PropertyField(middleButton);
                EditorGUILayout.PropertyField(rightButton);
                EditorGUILayout.PropertyField(backButton);
                EditorGUILayout.PropertyField(forwardButton);
                
            }
            else if (selectedTab == debugTab)
            {
                EditorGUILayout.PropertyField(verboseLogs);
                EditorGUILayout.PropertyField(inputEventManager);
            }

            // apply reference changes
            serializedObject.ApplyModifiedProperties();
        }

        #endregion
        
    } // class end
}

#endif
