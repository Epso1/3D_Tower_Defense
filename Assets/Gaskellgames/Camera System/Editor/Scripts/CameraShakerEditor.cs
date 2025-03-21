#if UNITY_EDITOR
using Gaskellgames.EditorOnly;
using UnityEditor;
using UnityEngine;

namespace Gaskellgames.CameraSystem.EditorOnly
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    [CustomEditor(typeof(CameraShaker)), CanEditMultipleObjects]
    public class CameraShakerEditor : GGEditor
    {
        #region Serialized Properties / OnEnable

        private CameraShaker targetAsType;

        SerializedProperty gizmosOnSelected;
        SerializedProperty gizmoColor;
        SerializedProperty intensity;
        SerializedProperty range;
        
        private static int selectedTab = 0;
        private string[] tabs = new[] { "Settings", "Debug" };
        private int settingsTab = 0;
        private int debugTab = 1;
        
        private const string pathRefName = "Camera System";
        private const string bannerRelativePath = "/Editor/Icons/InspectorBanner_CameraSystem.png";
        private Texture banner;

        private void OnEnable()
        {
            banner = EditorWindowUtility.LoadInspectorBanner(pathRefName, bannerRelativePath);
            
            gizmosOnSelected = serializedObject.FindProperty("gizmosOnSelected");
            gizmoColor = serializedObject.FindProperty("gizmoColor");
            intensity = serializedObject.FindProperty("intensity");
            range = serializedObject.FindProperty("range");
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region OnInspectorGUI

        public override void OnInspectorGUI()
        {
            // get & update references
            if (!targetAsType) { targetAsType = target as CameraShaker; }
            serializedObject.Update();

            // draw banner if turned on in Gaskellgames settings
            EditorWindowUtility.TryDrawBanner(banner);

            // custom inspector
            selectedTab = GUILayout.Toolbar(selectedTab, tabs);
            EditorGUILayout.Space();
            if (selectedTab == settingsTab)
            {
                EditorGUILayout.PropertyField(intensity);
                EditorGUILayout.PropertyField(range);
            }
            else if (selectedTab == debugTab)
            {
                EditorGUILayout.PropertyField(gizmosOnSelected);
                EditorGUILayout.PropertyField(gizmoColor);
            }

            // apply reference changes
            serializedObject.ApplyModifiedProperties();
        }

        #endregion
        
    } // class end
}

#endif
