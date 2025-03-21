#if UNITY_EDITOR
using Gaskellgames.EditorOnly;
using UnityEditor;
using UnityEngine;

namespace Gaskellgames.CameraSystem.EditorOnly
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    [InitializeOnLoad]
    public class HierarchyIcon_CameraSystem
    {
        #region Variables

        private static readonly Texture2D icon_CameraBrain;
        private static readonly Texture2D icon_CameraRig;
        private static readonly Texture2D icon_CameraFreelookRig;
        private static readonly Texture2D icon_CameraSwitcher;
        private static readonly Texture2D icon_CameraShaker;
        private static readonly Texture2D icon_CameraTriggerZone;
        private static readonly Texture2D icon_CameraTarget;
        private static readonly Texture2D icon_CameraMultiTarget;
        private static readonly Texture2D icon_ShadowTransform;

        private const string pathRefName = "Camera System";
        private const string relativePath = "/Editor/Icons/";

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Editor Loop

        static HierarchyIcon_CameraSystem()
        {
            if (!GgPathRef.TryGetFullFilePath(pathRefName, relativePath, out string filePath)) { return; }
            
            icon_CameraBrain = AssetDatabase.LoadAssetAtPath(filePath + "Icon_CameraBrain.png", typeof(Texture2D)) as Texture2D;
            EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyIcon_CameraBrain;
            
            icon_CameraRig = AssetDatabase.LoadAssetAtPath(filePath + "Icon_CameraRig.png", typeof(Texture2D)) as Texture2D;
            EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyIcon_CameraRig;
            
            icon_CameraFreelookRig = AssetDatabase.LoadAssetAtPath(filePath + "Icon_CameraFreelookRig.png", typeof(Texture2D)) as Texture2D;
            EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyIcon_CameraFreelookRig;
            
            icon_CameraSwitcher = AssetDatabase.LoadAssetAtPath(filePath + "Icon_CameraSwitcher.png", typeof(Texture2D)) as Texture2D;
            EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyIcon_CameraSwitcher;
            
            icon_CameraShaker = AssetDatabase.LoadAssetAtPath(filePath + "Icon_CameraShaker.png", typeof(Texture2D)) as Texture2D;
            EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyIcon_CameraShaker;
            
            icon_CameraTriggerZone = AssetDatabase.LoadAssetAtPath(filePath + "Icon_CameraTriggerZone.png", typeof(Texture2D)) as Texture2D;
            EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyIcon_CameraTriggerZone;
            
            icon_CameraTarget = AssetDatabase.LoadAssetAtPath(filePath + "Icon_CameraTarget.png", typeof(Texture2D)) as Texture2D;
            EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyIcon_CameraTarget;
            
            icon_CameraMultiTarget = AssetDatabase.LoadAssetAtPath(filePath + "Icon_CameraMultiTarget.png", typeof(Texture2D)) as Texture2D;
            EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyIcon_CameraMultiTarget;
            
            icon_ShadowTransform = AssetDatabase.LoadAssetAtPath(filePath + "Icon_ShadowTransform.png", typeof(Texture2D)) as Texture2D;
            EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyIcon_ShadowTransform;
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Private Functions

        private static void DrawHierarchyIcon_ShadowTransform(int instanceID, Rect position)
        {
            int offset = HierarchyUtility.CheckForHierarchyIconOffset<Comment>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<LockToLayer>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<SelectionTarget>(instanceID);
            HierarchyUtility.DrawHierarchyIcon<ShadowTransform>(instanceID, position, icon_ShadowTransform, offset);
        }

        private static void DrawHierarchyIcon_CameraBrain(int instanceID, Rect position)
        {
            int offset = HierarchyUtility.CheckForHierarchyIconOffset<Comment>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<LockToLayer>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<SelectionTarget>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<ShadowTransform>(instanceID);
            HierarchyUtility.DrawHierarchyIcon<CameraBrain>(instanceID, position, icon_CameraBrain, offset);
        }

        private static void DrawHierarchyIcon_CameraRig(int instanceID, Rect position)
        {
            int offset = HierarchyUtility.CheckForHierarchyIconOffset<Comment>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<LockToLayer>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<SelectionTarget>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<ShadowTransform>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<InputEventSystem.GMKInputController>(instanceID);
            HierarchyUtility.DrawHierarchyIcon<CameraRig>(instanceID, position, icon_CameraRig, offset);
        }

        private static void DrawHierarchyIcon_CameraFreelookRig(int instanceID, Rect position)
        {
            int offset = HierarchyUtility.CheckForHierarchyIconOffset<Comment>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<LockToLayer>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<SelectionTarget>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<ShadowTransform>(instanceID);
            HierarchyUtility.DrawHierarchyIcon<CameraFreelookRig>(instanceID, position, icon_CameraFreelookRig, offset);
        }

        private static void DrawHierarchyIcon_CameraSwitcher(int instanceID, Rect position)
        {
            int offset = HierarchyUtility.CheckForHierarchyIconOffset<Comment>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<LockToLayer>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<SelectionTarget>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<CameraBrain>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<ShadowTransform>(instanceID);
            HierarchyUtility.DrawHierarchyIcon<CameraSwitcher>(instanceID, position, icon_CameraSwitcher, offset);
        }

        private static void DrawHierarchyIcon_CameraShaker(int instanceID, Rect position)
        {
            int offset = HierarchyUtility.CheckForHierarchyIconOffset<Comment>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<LockToLayer>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<SelectionTarget>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<ShadowTransform>(instanceID);
            HierarchyUtility.DrawHierarchyIcon<CameraShaker>(instanceID, position, icon_CameraShaker, offset);
        }

        private static void DrawHierarchyIcon_CameraTriggerZone(int instanceID, Rect position)
        {
            int offset = HierarchyUtility.CheckForHierarchyIconOffset<Comment>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<LockToLayer>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<SelectionTarget>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<ShadowTransform>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<CameraMultiTargetingRig>(instanceID);
            HierarchyUtility.DrawHierarchyIcon<CameraTriggerZone>(instanceID, position, icon_CameraTriggerZone, offset);
        }

        private static void DrawHierarchyIcon_CameraTarget(int instanceID, Rect position)
        {
            int offset = HierarchyUtility.CheckForHierarchyIconOffset<Comment>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<LockToLayer>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<SelectionTarget>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<ShadowTransform>(instanceID);
            HierarchyUtility.DrawHierarchyIcon<CameraTarget>(instanceID, position, icon_CameraTarget, offset);
        }

        private static void DrawHierarchyIcon_CameraMultiTarget(int instanceID, Rect position)
        {
            int offset = HierarchyUtility.CheckForHierarchyIconOffset<Comment>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<LockToLayer>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<SelectionTarget>(instanceID);
            offset += HierarchyUtility.CheckForHierarchyIconOffset<ShadowTransform>(instanceID);
            HierarchyUtility.DrawHierarchyIcon<CameraMultiTargetingRig>(instanceID, position, icon_CameraMultiTarget, offset);
        }

        #endregion
        
    } // class end
}

#endif