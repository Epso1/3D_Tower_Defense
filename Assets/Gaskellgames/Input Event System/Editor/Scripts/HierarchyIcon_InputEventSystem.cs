#if UNITY_EDITOR
using Gaskellgames.EditorOnly;
using UnityEditor;
using UnityEngine;

namespace Gaskellgames.InputEventSystem.EditorOnly
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    [InitializeOnLoad]
    public class HierarchyIcon_InputEventSystem
    {
        #region Variables

        private static readonly Texture2D icon_InputActionManager;
        private static readonly Texture2D icon_GamepadCursor;
        private static readonly Texture2D icon_GMKInputController;
        private static readonly Texture2D icon_GMKVisualiser;
        private static readonly Texture2D icon_XRInputController;
        private static readonly Texture2D icon_InputEvent;
        private static readonly Texture2D icon_InputSequencer;

        private const string pathRefName = "Input Event System";
        private const string relativePath = "/Editor/Icons/";
        
        #endregion
        
        //----------------------------------------------------------------------------------------------------

        #region Editor Loop

        static HierarchyIcon_InputEventSystem()
        {
            if (!GgPathRef.TryGetFullFilePath(pathRefName, relativePath, out string filePath)) { return; }
            
            icon_InputActionManager = AssetDatabase.LoadAssetAtPath(filePath + "Icon_InputActionManager.png", typeof(Texture2D)) as Texture2D;
            EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyIcon_InputEventManager;
            
            icon_GamepadCursor = AssetDatabase.LoadAssetAtPath(filePath + "Icon_GamepadCursor.png", typeof(Texture2D)) as Texture2D;
            EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyIcon_GamepadCursor;
            
            icon_GMKInputController = AssetDatabase.LoadAssetAtPath(filePath + "Icon_GMKInputController.png", typeof(Texture2D)) as Texture2D;
            EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyIcon_GMKInputController;
            
            icon_GMKVisualiser = AssetDatabase.LoadAssetAtPath(filePath + "Icon_GMKVisualiser.png", typeof(Texture2D)) as Texture2D;
            EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyIcon_GMKVisualiser;
            
            icon_XRInputController = AssetDatabase.LoadAssetAtPath(filePath + "Icon_XRInputController.png", typeof(Texture2D)) as Texture2D;
            EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyIcon_XRInputController;
            
            icon_InputEvent = AssetDatabase.LoadAssetAtPath(filePath + "Icon_InputEvent.png", typeof(Texture2D)) as Texture2D;
            EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyIcon_InputEvent;
            
            icon_InputSequencer = AssetDatabase.LoadAssetAtPath(filePath + "Icon_InputSequencer.png", typeof(Texture2D)) as Texture2D;
            EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyIcon_InputSequencer;
        }

        #endregion
        
        //----------------------------------------------------------------------------------------------------

        #region Private Functions

        private static void DrawHierarchyIcon_InputEventManager(int instanceID, Rect position)
        {
            int offset = HierarchyUtility.CheckForHierarchyIconOffset<Comment>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<LockToLayer>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<SelectionTarget>(instanceID);
            HierarchyUtility.DrawHierarchyIcon<InputEventManager>(instanceID, position, icon_InputActionManager, offset);
        }

        private static void DrawHierarchyIcon_GamepadCursor(int instanceID, Rect position)
        {
            int offset = HierarchyUtility.CheckForHierarchyIconOffset<Comment>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<LockToLayer>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<SelectionTarget>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<InputEventManager>(instanceID);
            HierarchyUtility.DrawHierarchyIcon<GamepadCursor>(instanceID, position, icon_GamepadCursor, offset);
        }

        private static void DrawHierarchyIcon_GMKInputController(int instanceID, Rect position)
        {
            int offset = HierarchyUtility.CheckForHierarchyIconOffset<Comment>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<LockToLayer>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<SelectionTarget>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<InputEventManager>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<GamepadCursor>(instanceID);
            HierarchyUtility.DrawHierarchyIcon<GMKInputController>(instanceID, position, icon_GMKInputController, offset);
        }

        private static void DrawHierarchyIcon_GMKVisualiser(int instanceID, Rect position)
        {
            int offset = HierarchyUtility.CheckForHierarchyIconOffset<Comment>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<LockToLayer>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<SelectionTarget>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<InputEventManager>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<GamepadCursor>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<GMKInputController>(instanceID);
            HierarchyUtility.DrawHierarchyIcon<GMKVisualiser>(instanceID, position, icon_GMKVisualiser, offset);
        }

        private static void DrawHierarchyIcon_XRInputController(int instanceID, Rect position)
        {
            int offset = HierarchyUtility.CheckForHierarchyIconOffset<Comment>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<LockToLayer>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<SelectionTarget>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<InputEventManager>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<GamepadCursor>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<GMKInputController>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<GMKVisualiser>(instanceID);
            HierarchyUtility.DrawHierarchyIcon<XRInputController>(instanceID, position, icon_XRInputController, offset);
        }

        private static void DrawHierarchyIcon_InputEvent(int instanceID, Rect position)
        {
            int offset = HierarchyUtility.CheckForHierarchyIconOffset<Comment>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<LockToLayer>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<SelectionTarget>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<InputEventManager>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<GamepadCursor>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<GMKInputController>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<GMKVisualiser>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<XRInputController>(instanceID);
            HierarchyUtility.DrawHierarchyIcon<InputEvent>(instanceID, position, icon_InputEvent, offset);
        }

        private static void DrawHierarchyIcon_InputSequencer(int instanceID, Rect position)
        {
            int offset = HierarchyUtility.CheckForHierarchyIconOffset<Comment>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<LockToLayer>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<SelectionTarget>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<InputEventManager>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<GamepadCursor>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<GMKInputController>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<GMKVisualiser>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<XRInputController>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<InputEvent>(instanceID);
            HierarchyUtility.DrawHierarchyIcon<InputSequencer>(instanceID, position, icon_InputSequencer, offset);
        }

        #endregion
        
    } // class end
}

#endif