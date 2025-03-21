#if UNITY_EDITOR
using Gaskellgames.EditorOnly;
using UnityEditor;
using UnityEngine;

namespace Gaskellgames.CameraSystem.EditorOnly
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>

    [CustomEditor(typeof(CameraMultiTargetingRig)), CanEditMultipleObjects]
    public class CameraMultiTargetingRigEditor : GGEditor
    {
        #region Serialized Properties / OnEnable

        private CameraMultiTargetingRig targetAsType;

        SerializedProperty defaultColor;
        SerializedProperty trackedColor;
        
        SerializedProperty gizmosOnSelected;
        SerializedProperty zoomCamera;
        
        SerializedProperty refCamLookAt;
        SerializedProperty moveSpeed;
        SerializedProperty targetObjects;
        
        SerializedProperty cameraRig;
        SerializedProperty boundsX;
        SerializedProperty boundsY;
        SerializedProperty boundsZ;
        SerializedProperty padding;
        SerializedProperty minZoom;
        SerializedProperty maxZoom;
        SerializedProperty zoomSpeed;
        
        
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
            
            defaultColor = serializedObject.FindProperty("defaultColor");
            trackedColor = serializedObject.FindProperty("trackedColor");
            
            gizmosOnSelected = serializedObject.FindProperty("gizmosOnSelected");
            zoomCamera = serializedObject.FindProperty("zoomCamera");
            
            refCamLookAt = serializedObject.FindProperty("refCamLookAt");
            moveSpeed = serializedObject.FindProperty("moveSpeed");
            
            cameraRig = serializedObject.FindProperty("cameraRig");
            boundsX = serializedObject.FindProperty("boundsX");
            boundsY = serializedObject.FindProperty("boundsY");
            boundsZ = serializedObject.FindProperty("boundsZ");
            padding = serializedObject.FindProperty("padding");
            minZoom = serializedObject.FindProperty("minZoom");
            maxZoom = serializedObject.FindProperty("maxZoom");
            zoomSpeed = serializedObject.FindProperty("zoomSpeed");
            
            targetObjects = serializedObject.FindProperty("targetObjects");
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region OnInspectorGUI

        public override void OnInspectorGUI()
        {
            // get & update references
            if (!targetAsType) { targetAsType = target as CameraMultiTargetingRig; }
            serializedObject.Update();

            // draw banner if turned on in Gaskellgames settings
            EditorWindowUtility.TryDrawBanner(banner);

            // custom inspector
            selectedTab = GUILayout.Toolbar(selectedTab, tabs);
            EditorGUILayout.Space();
            if (selectedTab == settingsTab)
            {
                EditorGUILayout.PropertyField(zoomCamera);
                EditorGUILayout.PropertyField(refCamLookAt);
                EditorGUILayout.PropertyField(moveSpeed);
                EditorGUILayout.PropertyField(targetObjects);
                if (zoomCamera.boolValue)
                {
                    EditorGUILayout.PropertyField(cameraRig);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Calculate Bounds");
                    boundsX.boolValue = EditorGUILayout.ToggleLeft("X", boundsX.boolValue, GUILayout.Width(28), GUILayout.ExpandWidth(false));
                    boundsY.boolValue = EditorGUILayout.ToggleLeft("Y", boundsY.boolValue, GUILayout.Width(28), GUILayout.ExpandWidth(false));
                    boundsZ.boolValue = EditorGUILayout.ToggleLeft("Z", boundsZ.boolValue, GUILayout.Width(28), GUILayout.ExpandWidth(false));
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.PropertyField(padding);
                    EditorGUILayout.PropertyField(minZoom);
                    EditorGUILayout.PropertyField(maxZoom);
                    EditorGUILayout.PropertyField(zoomSpeed);
                }
            }
            else if (selectedTab == debugTab)
            {
                EditorGUILayout.PropertyField(gizmosOnSelected);
                EditorGUILayout.PropertyField(defaultColor);
                EditorGUILayout.PropertyField(trackedColor);
            }

            // apply reference changes
            serializedObject.ApplyModifiedProperties();
        }

        #endregion
        
    } // class end
}

#endif
