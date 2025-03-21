#if UNITY_EDITOR
using Gaskellgames.EditorOnly;
using UnityEditor;
using UnityEngine;

namespace Gaskellgames.InputEventSystem.EditorOnly
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>

    [CustomPropertyDrawer(typeof(VisualiserIcons))]
    public class VisualiserIconsDrawer : PropertyDrawer
    {
        #region variables

        private SerializedProperty image;
        private SerializedProperty normal;
        private SerializedProperty active;
        
        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Property Height

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) - (EditorExtensions.singleLine + EditorExtensions.spacer);
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region OnGUI

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // get reference to SerializeFields
            image = property.FindPropertyRelative("image");
            normal = property.FindPropertyRelative("normal");
            active = property.FindPropertyRelative("active");
            
            // open property and get reference to instance
            EditorGUI.BeginProperty(position, label, property);

            // draw property
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(image, new GUIContent(image.displayName, image.tooltip));
            EditorGUILayout.PropertyField(normal, new GUIContent(normal.displayName, normal.tooltip));
            EditorGUILayout.PropertyField(active, new GUIContent(active.displayName, active.tooltip));
            EditorGUI.indentLevel--;

            // close property
            EditorGUI.EndProperty();
        }

        #endregion

    } // class end 
}
#endif