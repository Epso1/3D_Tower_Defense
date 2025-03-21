#if UNITY_EDITOR
using Gaskellgames.EditorOnly;
using UnityEditor;
using UnityEngine;

namespace Gaskellgames.InputEventSystem.EditorOnly
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    [CustomEditor(typeof(GMKVisualiser)), CanEditMultipleObjects]
    public class GMKVisualiserEditor : GGEditor
    {
        #region Serialized Properties / OnEnable

        private GMKVisualiser targetAsType;

        private SerializedProperty GMKController;
        private SerializedProperty l2Start;
        private SerializedProperty l2End;
        private SerializedProperty r2Start;
        private SerializedProperty r2End;
        private SerializedProperty l3Center;
        private SerializedProperty r3Center;
        private SerializedProperty deadzone;
        private SerializedProperty stickTravel;

        private SerializedProperty dUp;
        private SerializedProperty dLeft;
        private SerializedProperty dRight;
        private SerializedProperty dDown;
        private SerializedProperty south;
        private SerializedProperty east;
        private SerializedProperty west;
        private SerializedProperty north;
        private SerializedProperty l1;
        private SerializedProperty r1;
        private SerializedProperty l2;
        private SerializedProperty r2;
        private SerializedProperty l3;
        private SerializedProperty r3;
        private SerializedProperty start;
        private SerializedProperty select;
        private SerializedProperty touchpad;
        
        private static int selectedTab = 0;
        private string[] tabs = new[] { "Settings", "Sprites" };
        private int settingsTab = 0;
        private int spritesTab = 1;
        
        private const string pathRefName = "Input Event System";
        private const string bannerRelativePath = "/Editor/Icons/inspectorBanner_InputEventSystem.png";
        private Texture banner;

        private void OnEnable()
        {
            if (!targetAsType) { targetAsType = target as GMKVisualiser; }
            banner = EditorWindowUtility.LoadInspectorBanner(pathRefName, bannerRelativePath);
            
            GMKController = serializedObject.FindProperty("GMKController");
            l2Start = serializedObject.FindProperty("l2Start");
            l2End = serializedObject.FindProperty("l2End");
            r2Start = serializedObject.FindProperty("r2Start");
            r2End = serializedObject.FindProperty("r2End");
            l3Center = serializedObject.FindProperty("l3Center");
            r3Center = serializedObject.FindProperty("r3Center");
            deadzone = serializedObject.FindProperty("deadzone");
            stickTravel = serializedObject.FindProperty("stickTravel");
        
            dUp = serializedObject.FindProperty("dUp");
            dLeft = serializedObject.FindProperty("dLeft");
            dRight = serializedObject.FindProperty("dRight");
            dDown = serializedObject.FindProperty("dDown");
            south = serializedObject.FindProperty("south");
            east = serializedObject.FindProperty("east");
            west = serializedObject.FindProperty("west");
            north = serializedObject.FindProperty("north");
            l1 = serializedObject.FindProperty("l1");
            r1 = serializedObject.FindProperty("r1");
            l2 = serializedObject.FindProperty("l2");
            r2 = serializedObject.FindProperty("r2");
            l3 = serializedObject.FindProperty("l3");
            r3 = serializedObject.FindProperty("r3");
            start = serializedObject.FindProperty("start");
            select = serializedObject.FindProperty("select");
            touchpad = serializedObject.FindProperty("touchpad");
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
                EditorGUILayout.PropertyField(GMKController);
                EditorGUILayout.PropertyField(l2Start);
                EditorGUILayout.PropertyField(l2End);
                EditorGUILayout.PropertyField(r2Start);
                EditorGUILayout.PropertyField(r2End);
                EditorGUILayout.PropertyField(l3Center);
                EditorGUILayout.PropertyField(r3Center);
                EditorGUILayout.PropertyField(deadzone);
                EditorGUILayout.PropertyField(stickTravel);
            }
            else if (selectedTab == spritesTab)
            {
                EditorGUILayout.PropertyField(dUp);
                EditorGUILayout.PropertyField(dLeft);
                EditorGUILayout.PropertyField(dRight);
                EditorGUILayout.PropertyField(dDown);
                EditorGUILayout.PropertyField(south);
                EditorGUILayout.PropertyField(east);
                EditorGUILayout.PropertyField(west);
                EditorGUILayout.PropertyField(north);
                EditorGUILayout.PropertyField(l1);
                EditorGUILayout.PropertyField(r1);
                EditorGUILayout.PropertyField(l2);
                EditorGUILayout.PropertyField(r2);
                EditorGUILayout.PropertyField(l3);
                EditorGUILayout.PropertyField(r3);
                EditorGUILayout.PropertyField(start);
                EditorGUILayout.PropertyField(select);
                EditorGUILayout.PropertyField(touchpad);
            }

            // apply reference changes
            serializedObject.ApplyModifiedProperties();
        }

        #endregion
        
    } // class end
}

#endif
