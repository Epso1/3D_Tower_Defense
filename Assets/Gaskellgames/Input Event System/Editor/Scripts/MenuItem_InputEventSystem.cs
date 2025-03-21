#if UNITY_EDITOR
using Gaskellgames.EditorOnly;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Gaskellgames.InputEventSystem.EditorOnly
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    public class MenuItem_InputEventSystem : MenuItemUtility
    {
        #region GameObject Menu
        
        [MenuItem(InputEventSystem_GameObjectMenu_Path + "/Input Event Manager", true, InputEventSystem_GameObjectMenu_Priority)]
        private static bool Gaskellgames_GameobjectMenu_InputEventManagerValidate()
        {
            return !(GameObject.FindObjectOfType<InputEventManager>());
        }
        
        [MenuItem(InputEventSystem_GameObjectMenu_Path + "/Input Event Manager", false, InputEventSystem_GameObjectMenu_Priority)]
        private static void Gaskellgames_GameobjectMenu_InputEventManager(MenuCommand menuCommand)
        {
            // create a custom gameObject, register in the undo system, parent and set position relative to context
            GameObject go = SetupMenuItemInContext(menuCommand, "InputEventManager");
            
            // Add scripts & components
            InputEventManager inputEventManager = go.AddComponent<InputEventManager>();
            inputEventManager.SetupInputActionManager();
            
            // select newly created gameObject
            Selection.activeObject = go;
        }
        
        [MenuItem(InputEventSystem_GameObjectMenu_Path + "/Gamepad Cursor", false, InputEventSystem_GameObjectMenu_Priority)]
        private static async void Gaskellgames_GameobjectMenu_GamepadCursor(MenuCommand menuCommand)
        {
            // add input action manager
            AddInputEventManager(menuCommand);
            
            // create a custom gameObject, register in the undo system, parent and set position relative to context
            GameObject go = SetupMenuItemInContext(menuCommand, "Canvas (Cursor)");
            GameObject child1 = AddChildItemInContext(go.transform, "GamepadCursor");
            GameObject child2 = AddChildItemInContext(child1.transform, "Cursor Offset");
            GameObject child3 = AddChildItemInContext(child2.transform, "Cursor Image");
            
            // Add scripts & components: parent
            go.layer = LayerMask.NameToLayer("UI");
            Canvas canvas = go.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 32000;
            go.AddComponent<CanvasScaler>();
            go.AddComponent<GraphicRaycaster>();
            
            // Add scripts & components: child 1
            child1.layer = LayerMask.NameToLayer("UI");
            RectTransform rectTransform = child1.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(50, 50);
            GamepadCursor gamepadCursor = child1.AddComponent<GamepadCursor>();
            
            // Add scripts & components: child 2
            child2.layer = child1.layer;
            RectTransform rectTransform2 = child2.AddComponent<RectTransform>();
            rectTransform2.sizeDelta = new Vector2(2, 2);
            
            // Add scripts & components: child 3
            child3.layer = child2.layer;
            rectTransform = child3.AddComponent<RectTransform>();
            rectTransform.anchoredPosition3D = new Vector3(12, -12, 0);
            rectTransform.sizeDelta = new Vector2(25, 25);
            child3.AddComponent<CanvasRenderer>();
            Image image = child3.AddComponent<Image>();
            image.raycastTarget = false;

            // assign values
            await TaskExtensions.WaitUntilNextFrame();
            gamepadCursor.CursorCanvas = canvas;
            gamepadCursor.CursorOffset = rectTransform2;
            gamepadCursor.CursorImage = image;
            
            // select newly created gameObject
            Selection.activeObject = child1;
        }
        
        [MenuItem(InputEventSystem_GameObjectMenu_Path + "/Input Event", false, InputEventSystem_GameObjectMenu_Priority)]
        private static void Gaskellgames_GameobjectMenu_InputEvent(MenuCommand menuCommand)
        {
            // add input action manager
            AddInputEventManager(menuCommand);
            
            // create a custom gameObject, register in the undo system, parent and set position relative to context
            GameObject go = SetupMenuItemInContext(menuCommand, "InputEvent");
            
            // Add scripts & components
            go.AddComponent<InputEvent>();
            
            // select newly created gameObject
            Selection.activeObject = go;
        }
        
        [MenuItem(InputEventSystem_GameObjectMenu_Path + "/Input Sequencer", false, InputEventSystem_GameObjectMenu_Priority)]
        private static void Gaskellgames_GameobjectMenu_InputSequencer(MenuCommand menuCommand)
        {
            // add input action manager
            AddInputEventManager(menuCommand);
            
            // create a custom gameObject, register in the undo system, parent and set position relative to context
            GameObject go = SetupMenuItemInContext(menuCommand, "InputSequencer");
            
            // Add scripts & components
            go.AddComponent<InputSequencer>();
            
            // select newly created gameObject
            Selection.activeObject = go;
        }
        
        [MenuItem(InputEventSystem_GameObjectMenu_Path + "/Input Controller (Gamepad, Mouse, Keyboard)", false, InputEventSystem_GameObjectMenu_Priority)]
        private static GMKInputController Gaskellgames_GameobjectMenu_GMKInputController(MenuCommand menuCommand)
        {
            // add input action manager
            AddInputEventManager(menuCommand);
            
            // create a custom gameObject, register in the undo system, parent and set position relative to context
            GameObject go = SetupMenuItemInContext(menuCommand, "InputController (GMK)");
            
            // Add scripts & components
            GMKInputController inputController = go.AddComponent<GMKInputController>();

            // select newly created gameObject
            Selection.activeObject = go;
            
            return inputController;
        }
        
        [MenuItem(InputEventSystem_GameObjectMenu_Path + "/Input Controller (Extended Reality)", false, InputEventSystem_GameObjectMenu_Priority)]
        private static void Gaskellgames_GameobjectMenu_XRInputController(MenuCommand menuCommand)
        {
            // add input action manager
            AddInputEventManager(menuCommand);
            
            // create a custom gameObject, register in the undo system, parent and set position relative to context
            GameObject go = SetupMenuItemInContext(menuCommand, "InputController (XR)");
            
            // Add scripts & components
            go.AddComponent<XRInputController>();
            
            // select newly created gameObject
            Selection.activeObject = go;
        }
        
        [MenuItem(InputEventSystem_GameObjectMenu_Path + "/Cursor Lock State", false, InputEventSystem_GameObjectMenu_Priority)]
        private static void Gaskellgames_GameobjectMenu_CursorLockState(MenuCommand menuCommand)
        {
            // add input action manager
            AddInputEventManager(menuCommand);
            
            // create a custom gameObject, register in the undo system, parent and set position relative to context
            GameObject go = SetupMenuItemInContext(menuCommand, "CursorLockState");
            
            // Add scripts & components
            go.AddComponent<CursorLockState>();
            
            // select newly created gameObject
            Selection.activeObject = go;
        }
        
        #endregion
        
        //----------------------------------------------------------------------------------------------------

        #region Public Functions

        public static void AddInputEventManager(MenuCommand menuCommand)
        {
            if (Gaskellgames_GameobjectMenu_InputEventManagerValidate())
            {
                Gaskellgames_GameobjectMenu_InputEventManager(menuCommand);
            }
        }

        public static GMKInputController AddPlayerInputController(MenuCommand menuCommand)
        {
            return Gaskellgames_GameobjectMenu_GMKInputController(menuCommand);
        }

        #endregion
        
    } // class end
}

#endif