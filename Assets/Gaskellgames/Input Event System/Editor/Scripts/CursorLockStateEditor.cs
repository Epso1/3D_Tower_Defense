#if UNITY_EDITOR
using Gaskellgames.EditorOnly;
using UnityEditor;
using UnityEngine;

namespace Gaskellgames.InputEventSystem.EditorOnly
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    [CustomEditor(typeof(CursorLockState)), CanEditMultipleObjects]
    public class CursorLockStateEditor : GGEditor
    {
        #region Serialized Properties / OnEnable

        private CursorLockState targetAsType;
        
        new SerializedProperty verboseLogs;
        
        private SerializedProperty setStateOnStart;
        private SerializedProperty lockMode;
        private SerializedProperty isVisible;
        
        private static int selectedTab = 0;
        private string[] tabs = new[] { "Settings", "Debug" };
        private int settingsTab = 0;
        private int debugTab = 1;
        
        private const string pathRefName = "Input Event System";
        private const string bannerRelativePath = "/Editor/Icons/inspectorBanner_InputEventSystem.png";
        private Texture banner;

        private void OnEnable()
        {
            banner = EditorWindowUtility.LoadInspectorBanner(pathRefName, bannerRelativePath);
            
            verboseLogs = serializedObject.FindProperty("verboseLogs");
            
            setStateOnStart = serializedObject.FindProperty("setStateOnStart");
            lockMode = serializedObject.FindProperty("lockMode");
            isVisible = serializedObject.FindProperty("isVisible");
        }

        #endregion

        //----------------------------------------------------------------------------------------------------
        
        #region OnInspectorGUI

        public override void OnInspectorGUI()
        {
            // get & update references
            if (!targetAsType) { targetAsType = target as CursorLockState; }
            serializedObject.Update();

            // draw banner if turned on in Gaskellgames settings
            EditorWindowUtility.TryDrawBanner(banner);
            
            // cache defaults
            bool defaultEnabled = GUI.enabled;
            
            // draw inspector
            selectedTab = GUILayout.Toolbar(selectedTab, tabs);
            EditorGUILayout.Space();
            if (selectedTab == settingsTab)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(setStateOnStart);
                GUI.enabled = setStateOnStart.boolValue;
                EditorGUILayout.PropertyField(isVisible);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.PropertyField(lockMode);
                GUI.enabled = defaultEnabled;
            }
            else if (selectedTab == debugTab)
            {
                EditorGUILayout.PropertyField(verboseLogs);
            }

            // apply reference changes
            serializedObject.ApplyModifiedProperties();
        }

        #endregion
        
    } // class end
}

#endif
