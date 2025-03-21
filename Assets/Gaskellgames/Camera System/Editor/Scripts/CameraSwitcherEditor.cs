#if UNITY_EDITOR
using Gaskellgames.EditorOnly;
using UnityEditor;
using UnityEngine;

namespace Gaskellgames.CameraSystem.EditorOnly
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    [CustomEditor(typeof(CameraSwitcher)), CanEditMultipleObjects]
    public class CameraSwitcherEditor : GGEditor
    {
        #region Serialized Properties / OnEnable

        private CameraSwitcher targetAsType;
        
        private SerializedProperty cameraBrain;
        private SerializedProperty useRegisteredList;
        private SerializedProperty switchCamera;
        private SerializedProperty customCameraRigsList;
        
        private const string pathRefName = "Camera System";
        private const string bannerRelativePath = "/Editor/Icons/InspectorBanner_CameraSystem.png";
        private Texture banner;

        private void OnEnable()
        {
            banner = EditorWindowUtility.LoadInspectorBanner(pathRefName, bannerRelativePath);
            
            cameraBrain = serializedObject.FindProperty("cameraBrain");
            useRegisteredList = serializedObject.FindProperty("useRegisteredList");
            switchCamera = serializedObject.FindProperty("switchCamera");
            customCameraRigsList = serializedObject.FindProperty("customCameraRigsList");
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region OnInspectorGUI

        public override void OnInspectorGUI()
        {
            // get & update references
            if (!targetAsType) { targetAsType = target as CameraSwitcher; }
            serializedObject.Update();

            // draw banner if turned on in Gaskellgames settings
            EditorWindowUtility.TryDrawBanner(banner);

            // custom inspector
            EditorGUILayout.PropertyField(useRegisteredList);
            EditorGUILayout.PropertyField(cameraBrain);
            EditorGUILayout.PropertyField(switchCamera);

            if (!useRegisteredList.boolValue)
            {
                EditorGUILayout.PropertyField(customCameraRigsList);
            }

            // apply reference changes
            serializedObject.ApplyModifiedProperties();
        }

        #endregion
        
    } // class end
}

#endif
