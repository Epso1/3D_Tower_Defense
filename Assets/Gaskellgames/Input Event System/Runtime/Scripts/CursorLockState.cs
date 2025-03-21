using UnityEngine;

namespace Gaskellgames.InputEventSystem
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>

    [AddComponentMenu("Gaskellgames/Input Event System/Cursor Lock State")]
    public class CursorLockState : GGMonoBehaviour
    {
        #region Variables

        [SerializeField]
        [Tooltip("Set the state of the hardware cursor on Start.")]
        private bool setStateOnStart;

        [SerializeField]
        [Tooltip("The CursorLockMode to be set.")]
        private CursorLockMode lockMode = CursorLockMode.None;

        [SerializeField]
        [Tooltip("Toggle whether the hardware cursor is visible.")]
        private bool isVisible = true;

        private CursorLockMode currentLockMode;
        private bool currentVisibility;
        
        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Game Loop

        private void Start()
        {
            // try lock cursor to screen
            if (!setStateOnStart) { return; }
            SetCursorLockState(lockMode, isVisible);
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Public Functions

        /// <summary>
        /// Set the state of the hardware cursor
        /// </summary>
        /// <param name="cursorVisible"></param>
        public void SetCursorLockState(bool cursorVisible)
        {
            SetCursorLockState(currentLockMode, cursorVisible);
        }
        
        /// <summary>
        /// Set the state of the hardware cursor
        /// </summary>
        /// <param name="cursorLockMode"></param>
        /// <param name="cursorVisible"></param>
        public void SetCursorLockState(CursorLockMode cursorLockMode, bool cursorVisible)
        {
            // cache values
            currentLockMode = cursorLockMode;
            currentVisibility = cursorVisible;
            
            Cursor.lockState = currentLockMode;
            Cursor.visible = currentVisibility;

            Log($"Hardware cursor set to lockState = {cursorLockMode} and visible = {cursorVisible}.", LogType.Log);
        }

        #endregion

    } // class end
}