#if UNITY_EDITOR
using Gaskellgames.EditorOnly;
using UnityEditor;
using UnityEngine;

namespace Gaskellgames.CameraSystem.EditorOnly
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    [CustomEditor(typeof(CameraTarget)), CanEditMultipleObjects]
    public class CameraTargetEditor : GGEditor
    {
        #region Serialized Properties / OnEnable

        private CameraTarget targetAsType;
        
        SerializedProperty targetType;
        SerializedProperty autoFindMultiTarget;
        SerializedProperty multiTarget;
        
        SerializedProperty cameraBrain;
        SerializedProperty triggerTag;
        SerializedProperty revertOnExit;
        SerializedProperty OnEnterTag;
        SerializedProperty OnExitTag;
        
        private static int selectedTab = 0;
        private string[] tabs = new[] { "Settings", "Events" };
        private int settingsTab = 0;
        private int eventsTab = 1;
        
        private const string pathRefName = "Camera System";
        private const string bannerRelativePath = "/Editor/Icons/InspectorBanner_CameraSystem.png";
        private Texture banner;

        private void OnEnable()
        {
            banner = EditorWindowUtility.LoadInspectorBanner(pathRefName, bannerRelativePath);
            
            targetType = serializedObject.FindProperty("targetType");
            autoFindMultiTarget = serializedObject.FindProperty("autoFindMultiTarget");
            multiTarget = serializedObject.FindProperty("multiTarget");
            
            cameraBrain = serializedObject.FindProperty("cameraBrain");
            triggerTag = serializedObject.FindProperty("triggerTag");
            revertOnExit = serializedObject.FindProperty("revertOnExit");
            OnEnterTag = serializedObject.FindProperty("OnEnterTag");
            OnExitTag = serializedObject.FindProperty("OnExitTag");
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region OnInspectorGUI

        public override void OnInspectorGUI()
        {
            // get & update references
            if (!targetAsType) { targetAsType = target as CameraTarget; }
            serializedObject.Update();

            // draw banner if turned on in Gaskellgames settings
            EditorWindowUtility.TryDrawBanner(banner);

            // custom inspector
            selectedTab = GUILayout.Toolbar(selectedTab, tabs);
            EditorGUILayout.Space();
            if (selectedTab == settingsTab)
            {
                EditorGUILayout.PropertyField(targetType);
                if (targetType.enumValueIndex == CameraTarget.TargetTypes.OnEnable.ToInt())
                {
                    if (autoFindMultiTarget.boolValue)
                    {
                        EditorGUILayout.HelpBox("AutoFindMultiTarget should only be used when there is a single multiTarget in the scene", MessageType.Info);
                    }
                    EditorGUILayout.PropertyField(autoFindMultiTarget);
                    if (!autoFindMultiTarget.boolValue) { EditorGUILayout.PropertyField(multiTarget); }
                }
                else if (targetType.enumValueIndex == CameraTarget.TargetTypes.OnTrigger.ToInt())
                {
                    EditorGUILayout.PropertyField(triggerTag);
                    EditorGUILayout.PropertyField(cameraBrain);
                    if (cameraBrain.objectReferenceValue) { EditorGUILayout.PropertyField(revertOnExit); }
                }
            }
            else if (selectedTab == eventsTab)
            {
                EditorGUILayout.PropertyField(OnEnterTag);
                EditorGUILayout.PropertyField(OnExitTag);
            }

            // apply reference changes
            serializedObject.ApplyModifiedProperties();
        }

        #endregion
        
    } // class end
}

#endif
