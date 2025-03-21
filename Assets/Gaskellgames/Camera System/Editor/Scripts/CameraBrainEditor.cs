#if UNITY_EDITOR
using Gaskellgames.EditorOnly;
using UnityEditor;
using UnityEngine;

namespace Gaskellgames.CameraSystem.EditorOnly
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>

    [CustomEditor(typeof(CameraBrain)), CanEditMultipleObjects]
    public class CameraBrainEditor : GGEditor
    {
        #region Serialized Properties / OnEnable

        SerializedProperty activeCamera;
        SerializedProperty previousCamera;

        SerializedProperty blendingStyle;
        SerializedProperty fadeCurve;
        SerializedProperty fadeColor;
        SerializedProperty fadeSpeed;
        SerializedProperty fadeFullScreen;
        SerializedProperty canvasGroup;
        
        SerializedProperty blendCurve;
        SerializedProperty blendSpeed;

        SerializedProperty follow;
        SerializedProperty lookAt;
        SerializedProperty lens;
        SerializedProperty cameraOrbit;
        
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
            
            activeCamera = serializedObject.FindProperty("activeCamera");
            previousCamera = serializedObject.FindProperty("previousCamera");

            blendingStyle = serializedObject.FindProperty("blendingStyle");
            fadeCurve = serializedObject.FindProperty("fadeCurve");
            fadeColor = serializedObject.FindProperty("fadeColor");
            fadeSpeed = serializedObject.FindProperty("fadeSpeed");
            fadeFullScreen = serializedObject.FindProperty("fadeFullScreen");
            canvasGroup = serializedObject.FindProperty("canvasGroup");

            blendCurve = serializedObject.FindProperty("blendCurve");
            blendSpeed = serializedObject.FindProperty("blendSpeed");

            follow = serializedObject.FindProperty("follow");
            lookAt = serializedObject.FindProperty("lookAt");
            lens = serializedObject.FindProperty("lens");
            cameraOrbit = serializedObject.FindProperty("cameraOrbit");
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region OnInspectorGUI

        public override void OnInspectorGUI()
        {
            // get & update references
            CameraBrain cameraBrain = (CameraBrain)target;
            serializedObject.Update();

            // draw banner if turned on in Gaskellgames settings
            EditorWindowUtility.TryDrawBanner(banner);

            // custom inspector
            selectedTab = GUILayout.Toolbar(selectedTab, tabs);
            EditorGUILayout.Space();
            if (selectedTab == settingsTab)
            {
                EditorGUILayout.PropertyField(activeCamera);
                
                EditorGUILayout.PropertyField(blendingStyle);
                EditorGUI.indentLevel++;
                if (blendingStyle.enumValueIndex == CameraBrain.CameraBlendStyle.FadeToColor.ToInt())
                {
                    EditorGUILayout.PropertyField(fadeCurve);
                    EditorGUILayout.PropertyField(fadeSpeed);
                    EditorGUILayout.PropertyField(fadeFullScreen);
                    EditorGUILayout.PropertyField(!fadeFullScreen.boolValue ? canvasGroup : fadeColor);
                }
                else if (blendingStyle.enumValueIndex == CameraBrain.CameraBlendStyle.MoveToPosition.ToInt())
                {
                    EditorGUILayout.PropertyField(blendCurve);
                    EditorGUILayout.PropertyField(blendSpeed);
                }
                EditorGUI.indentLevel--;
            }
            else if (selectedTab == debugTab)
            {
                EditorGUILayout.PropertyField(previousCamera);
                
                CameraRig rig = cameraBrain.ActiveCamera;
                if (!rig)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.HelpBox("No Active Camera Assigned", MessageType.Warning);
                }
                EditorGUILayout.PropertyField(follow);
                EditorGUILayout.PropertyField(lookAt);
                EditorGUILayout.PropertyField(lens);
                if (rig)
                {
                    CameraFreelookRig FreelookRig = rig.FreelookRig;
                    if (FreelookRig != null)
                    {
                        EditorGUILayout.PropertyField(cameraOrbit);
                    }
                }
            }

            // apply reference changes
            serializedObject.ApplyModifiedProperties();
        }

        #endregion
        
    } // class end
}

#endif
