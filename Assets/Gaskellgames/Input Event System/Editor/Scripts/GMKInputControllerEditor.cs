#if UNITY_EDITOR
using Gaskellgames.EditorOnly;
using UnityEditor;
using UnityEngine;

namespace Gaskellgames.InputEventSystem.EditorOnly
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    [CustomEditor(typeof(GMKInputController)), CanEditMultipleObjects]
    public class GMKInputControllerEditor : GGEditor
    {
        #region Serialized Properties / OnEnable

        private GMKInputController targetAsType;
        
        private SerializedProperty south;
        private SerializedProperty east;
        private SerializedProperty west;
        private SerializedProperty north;
        private SerializedProperty leftShoulder;
        private SerializedProperty rightShoulder;
        private SerializedProperty select;
        private SerializedProperty start;
        private SerializedProperty leftStickPress;
        private SerializedProperty rightStickPress;
        private SerializedProperty touchpadPress;
        private SerializedProperty leftStick;
        private SerializedProperty rightStick;
        private SerializedProperty dPad;
        private SerializedProperty leftTrigger;
        private SerializedProperty rightTrigger;
        
        private SerializedProperty inputs;
        
        private const string pathRefName = "Input Event System";
        private const string bannerRelativePath = "/Editor/Icons/inspectorBanner_InputEventSystem.png";
        private Texture banner;

        private static int selectedTab = 0;
        private string[] tabs = new[] { "Settings", "Debug" };
        private int settingsTab = 0;
        private int debugTab = 1;

        private void OnEnable()
        {
            banner = EditorWindowUtility.LoadInspectorBanner(pathRefName, bannerRelativePath);
            
            south = serializedObject.FindProperty("south");
            east = serializedObject.FindProperty("east");
            west = serializedObject.FindProperty("west");
            north = serializedObject.FindProperty("north");
            leftShoulder = serializedObject.FindProperty("leftShoulder");
            rightShoulder = serializedObject.FindProperty("rightShoulder");
            select = serializedObject.FindProperty("select");
            start = serializedObject.FindProperty("start");
            leftStickPress = serializedObject.FindProperty("leftStickPress");
            rightStickPress = serializedObject.FindProperty("rightStickPress");
            touchpadPress = serializedObject.FindProperty("touchpadPress");
            leftStick = serializedObject.FindProperty("leftStick");
            rightStick = serializedObject.FindProperty("rightStick");
            dPad = serializedObject.FindProperty("dPad");
            leftTrigger = serializedObject.FindProperty("leftTrigger");
            rightTrigger = serializedObject.FindProperty("rightTrigger");
            
            inputs = serializedObject.FindProperty("inputs");
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region OnInspectorGUI

        public override void OnInspectorGUI()
        {
            // get & update references
            if(!targetAsType) { targetAsType = target as GMKInputController; }
            serializedObject.Update();

            // draw banner if turned on in Gaskellgames settings
            EditorWindowUtility.TryDrawBanner(banner);

            // draw inspector
            selectedTab = GUILayout.Toolbar(selectedTab, tabs);
            EditorGUILayout.Space();
            if (selectedTab == settingsTab)
            {
                EditorGUILayout.PropertyField(south);
                EditorGUILayout.PropertyField(east);
                EditorGUILayout.PropertyField(west);
                EditorGUILayout.PropertyField(north);
                EditorGUILayout.PropertyField(leftShoulder);
                EditorGUILayout.PropertyField(rightShoulder);
                EditorGUILayout.PropertyField(select);
                EditorGUILayout.PropertyField(start);
                EditorGUILayout.PropertyField(leftStickPress);
                EditorGUILayout.PropertyField(rightStickPress);
                EditorGUILayout.PropertyField(touchpadPress);
                EditorGUILayout.PropertyField(leftStick);
                EditorGUILayout.PropertyField(rightStick);
                EditorGUILayout.PropertyField(dPad);
                EditorGUILayout.PropertyField(leftTrigger);
                EditorGUILayout.PropertyField(rightTrigger);
            }
            else if (selectedTab == debugTab)
            {
                EditorGUILayout.PropertyField(inputs);
            }

            // apply reference changes
            serializedObject.ApplyModifiedProperties();
        }

        #endregion

    } // class end
}

#endif
