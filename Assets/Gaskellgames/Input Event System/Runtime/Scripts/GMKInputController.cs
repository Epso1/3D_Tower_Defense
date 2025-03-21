using UnityEngine;
using UnityEngine.InputSystem;

namespace Gaskellgames.InputEventSystem
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    [AddComponentMenu("Gaskellgames/Input Event System/GMK Input Controller")]
    public class GMKInputController : GGMonoBehaviour
    {
        #region Variables
        
        [SerializeField, Required]
        [Tooltip("Default: Space / Button South")]
        private InputActionReference south; // button0

        [SerializeField, Required]
        [Tooltip("Default: L Ctrl / Button East")]
        private InputActionReference east; // button1

        [SerializeField, Required]
        [Tooltip("Default: L Alt / Button West")]
        private InputActionReference west; // button2

        [SerializeField, Required]
        [Tooltip("Default: C / Button North")]
        private InputActionReference north; // button3

        [SerializeField, Required]
        [Tooltip("Default: L Shift / Left Shoulder")]
        private InputActionReference leftShoulder; // button4

        [SerializeField, Required]
        [Tooltip("Default: F / Right Shoulder")]
        private InputActionReference rightShoulder; // button5

        [SerializeField, Required]
        [Tooltip("Default: P / Select")]
        private InputActionReference select; // button6

        [SerializeField, Required]
        [Tooltip("Default: Esc / Start")]
        private InputActionReference start; // button7

        [SerializeField, Required]
        [Tooltip("Default: Q / Left Stick Press")]
        private InputActionReference leftStickPress; // button8

        [SerializeField, Required]
        [Tooltip("Default: E / Right Stick Press")]
        private InputActionReference rightStickPress; // button9

        [SerializeField, Required]
        [Tooltip("Default: Middle Mouse / Touchpad Press")]
        private InputActionReference touchpadPress; // button10

        [SerializeField, Required]
        [Tooltip("Default: WASD / Left Stick")]
        private InputActionReference leftStick; // axisXY

        [SerializeField, Required]
        [Tooltip("Default: Arrows / Right Stick")]
        private InputActionReference rightStick; // axis45

        [SerializeField, Required]
        [Tooltip("Default: 1, 2, 3, 4 / D-Pad")]
        private InputActionReference dPad; // axis67

        [SerializeField, Required]
        [Tooltip("Default: R Mouse / L Trigger")]
        private InputActionReference leftTrigger; // axis 9

        [SerializeField, Required]
        [Tooltip("Default: L Mouse / R Trigger")]
        private InputActionReference rightTrigger; // axis 10
        
        // -----
        
        [SerializeField, ReadOnly]
        [Tooltip("The User Inputs for this frame.")]
        private GMKInputs inputs;
        
        #endregion
        
        //----------------------------------------------------------------------------------------------------
        
        #region Game Loop

        private void OnEnable()
        {
            // subscribe to input system update loop
            InputSystem.onAfterUpdate += InputSystem_OnAfterUpdate;
        }

        private void OnDisable()
        {
            // unsubscribe from input system update loop
            InputSystem.onAfterUpdate -= InputSystem_OnAfterUpdate;
        }

        private void InputSystem_OnAfterUpdate()
        {
            UpdateButtonInput();
            UpdateAxisInput();
        }

        #endregion
        
        //----------------------------------------------------------------------------------------------------
        
        #region Private Functions

        private void UpdateButtonInput()
        {
            if (south)
            {
                inputs.south.keydown = south.action.WasPressedThisFrame();
                inputs.south.keypressed = south.action.IsPressed();
                inputs.south.keyreleased = south.action.WasReleasedThisFrame();
            }

            if (east)
            {
                inputs.east.keydown = east.action.WasPressedThisFrame();
                inputs.east.keypressed = east.action.IsPressed();
                inputs.east.keyreleased = east.action.WasReleasedThisFrame();
            }

            if (west)
            {
                inputs.west.keydown = west.action.WasPressedThisFrame();
                inputs.west.keypressed = west.action.IsPressed();
                inputs.west.keyreleased = west.action.WasReleasedThisFrame();
            }

            if (north)
            {
                inputs.north.keydown = north.action.WasPressedThisFrame();
                inputs.north.keypressed = north.action.IsPressed();
                inputs.north.keyreleased = north.action.WasReleasedThisFrame();
            }

            if (leftShoulder)
            {
                inputs.leftShoulder.keydown = leftShoulder.action.WasPressedThisFrame();
                inputs.leftShoulder.keypressed = leftShoulder.action.IsPressed();
                inputs.leftShoulder.keyreleased = leftShoulder.action.WasReleasedThisFrame();
            }

            if (rightShoulder)
            {
                inputs.rightShoulder.keydown = rightShoulder.action.WasPressedThisFrame();
                inputs.rightShoulder.keypressed = rightShoulder.action.IsPressed();
                inputs.rightShoulder.keyreleased = rightShoulder.action.WasReleasedThisFrame();
            }

            if (start)
            {
                inputs.start.keydown = start.action.WasPressedThisFrame();
                inputs.start.keypressed = start.action.IsPressed();
                inputs.start.keyreleased = start.action.WasReleasedThisFrame();
            }

            if (select)
            {
                inputs.select.keydown = select.action.WasPressedThisFrame();
                inputs.select.keypressed = select.action.IsPressed();
                inputs.select.keyreleased = select.action.WasReleasedThisFrame();
            }

            if (leftStickPress)
            {
                inputs.leftStickPress.keydown = leftStickPress.action.WasPressedThisFrame();
                inputs.leftStickPress.keypressed = leftStickPress.action.IsPressed();
                inputs.leftStickPress.keyreleased = leftStickPress.action.WasReleasedThisFrame();
            }

            if (rightStickPress)
            {
                inputs.rightStickPress.keydown = rightStickPress.action.WasPressedThisFrame();
                inputs.rightStickPress.keypressed = rightStickPress.action.IsPressed();
                inputs.rightStickPress.keyreleased = rightStickPress.action.WasReleasedThisFrame();
            }

            if (touchpadPress)
            {
                inputs.touchpadPress.keydown = touchpadPress.action.WasPressedThisFrame();
                inputs.touchpadPress.keypressed = touchpadPress.action.IsPressed();
                inputs.touchpadPress.keyreleased = touchpadPress.action.WasReleasedThisFrame();
            }
        }

        private void UpdateAxisInput()
        {
            if (leftStick) { inputs.leftStick = leftStick.action.ReadValue<Vector2>(); }
            if (rightStick) { inputs.rightStick = rightStick.action.ReadValue<Vector2>(); }
            if (dPad) { inputs.dPad = dPad.action.ReadValue<Vector2>(); }
            if (leftTrigger) { inputs.leftTrigger = leftTrigger.action.ReadValue<float>(); }
            if (rightTrigger) { inputs.rightTrigger = rightTrigger.action.ReadValue<float>(); }
        }

        #endregion
        
        //----------------------------------------------------------------------------------------------------
        
        #region Getter / Setter

        public GMKInputs Inputs => inputs;

        #endregion
        
    } // class end
}