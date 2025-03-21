#if UNITY_EDITOR
using Gaskellgames.EditorOnly;
using UnityEditor;
using UnityEngine;

namespace Gaskellgames.InputEventSystem.EditorOnly
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>

    [CustomPropertyDrawer(typeof(ButtonInputs))]
    public class ButtonInputsDrawer : PropertyDrawer
    {
        #region variables

        private SerializedProperty keypressed;
        
        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Property Height

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorExtensions.singleLine + EditorExtensions.spacer;
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region OnGUI

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // get reference to SerializeFields
            keypressed = property.FindPropertyRelative("keypressed");
            
            // open property and get reference to instance
            EditorGUI.BeginProperty(position, label, property);

            // cache positional values
            float labelWidth = EditorExtensions.labelWidthForIndentLevel;
            float propertyWidth = position.width - (labelWidth + EditorExtensions.spacer);
            float propertyPosition = position.x + labelWidth;
            Rect labelRect = new Rect(position.x, position.y, labelWidth, EditorExtensions.singleLine);
            Rect propertyRect = new Rect(propertyPosition, position.y, propertyWidth, EditorExtensions.singleLine);
            
            // draw property
            EditorGUI.PrefixLabel(labelRect, label);
            EditorGUIUtility.labelWidth = propertyWidth - EditorExtensions.singleLine;
            keypressed.boolValue = EditorGUI.Toggle(propertyRect, new GUIContent(), keypressed.boolValue);
            EditorGUIUtility.labelWidth = labelWidth;

            // close property
            EditorGUI.EndProperty();
        }

        #endregion

    } // class end 
}
#endif