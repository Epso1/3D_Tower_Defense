#if UNITY_EDITOR
using Gaskellgames.EditorOnly;
using UnityEditor;
using UnityEngine;

namespace Gaskellgames.CameraSystem.EditorOnly
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    [CustomEditor(typeof(CameraTriggerZone)), CanEditMultipleObjects]
    public class CameraTriggerZoneEditor : GGEditor
    {
        #region Serialized Properties / OnEnable

        private CameraTriggerZone targetAsType;
        
        private SerializedProperty gizmosOnSelected;
        private SerializedProperty cameraRig;
        private SerializedProperty triggerColour;
        private SerializedProperty triggerOutlineColour;
        
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
            cameraRig = serializedObject.FindProperty("cameraRig");
            triggerColour = serializedObject.FindProperty("triggerColour");
            triggerOutlineColour = serializedObject.FindProperty("triggerOutlineColour");
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region OnInspectorGUI

        public override void OnInspectorGUI()
        {
            // get & update references
            if (!targetAsType) { targetAsType = target as CameraTriggerZone; }
            serializedObject.Update();

            // draw banner if turned on in Gaskellgames settings
            EditorWindowUtility.TryDrawBanner(banner);

            // custom inspector
            selectedTab = GUILayout.Toolbar(selectedTab, tabs);
            EditorGUILayout.Space();
            if (selectedTab == settingsTab)
            {
                EditorGUILayout.PropertyField(cameraRig);
            }
            else if (selectedTab == debugTab)
            {
                EditorGUILayout.PropertyField(gizmosOnSelected);
                EditorGUILayout.PropertyField(triggerColour);
                EditorGUILayout.PropertyField(triggerOutlineColour);
            }

            // apply reference changes
            serializedObject.ApplyModifiedProperties();
        }

        #endregion
        
    } // class end
}

#endif
