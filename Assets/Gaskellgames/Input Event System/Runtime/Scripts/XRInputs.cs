using UnityEngine;

namespace Gaskellgames.InputEventSystem
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>

    [System.Serializable]
    public class XRInputs
    {
        #region Variables

        public ButtonInputsTouch primaryButton;
        
        public ButtonInputsTouch secondaryButton;
        
        public ButtonInputsTouch joystickButton;
        
        public ButtonInputs menuButton;

        [Graph(-1, 1)]
        public Vector2 joystick;
        
        public bool joystickTouch;
        
        [Range(0, 1)]
        public float triggerAxis;
        
        public bool triggerTouch;
        
        [Range(0, 1)]
        public float gripAxis;

        #endregion

    } // class end
}
