using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Gaskellgames.InputEventSystem
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    [AddComponentMenu("Gaskellgames/Input Event System/Input Event")]
    public class InputEvent : GGMonoBehaviour
    {
        #region Variables
        
        [SerializeField, Required]
        [Tooltip("User input reference.")]
        private InputActionReference inputAction;
        
        [SerializeField, ReadOnly]
        [Tooltip("The output value type for the referenced inputAction.")]
        private InputSystemExtensions.InputActionValueType valueType = InputSystemExtensions.InputActionValueType.Unknown;
        
        [SerializeField, Range(0, 1)]
        [Tooltip("The required output value, of the axis inputAction being treated as a button, to be classed as pressed.")]
        private float threshold = 0.9f;
        
        [SerializeField, ReadOnly]
        [Tooltip("True if the user input button is held.")]
        private bool isPressed;
        
        [SerializeField]
        public GgEvent<InputEvent> OnPressed;
        
        [SerializeField, Space]
        public GgEvent<InputEvent> OnHeld;
        
        [SerializeField, Space]
        public GgEvent<InputEvent> OnReleased;
        
        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Editor Loop

        private void OnValidate()
        {
            valueType = inputAction != null ? inputAction.action.ValueType() : InputSystemExtensions.InputActionValueType.Unknown;
        }

        #endregion
        
        //----------------------------------------------------------------------------------------------------

        #region Game loop

        private void OnEnable()
        {
            if (!inputAction) { return; }
            
            // subscribe to callbacks only if input action is correct value type
            valueType = inputAction.action.ValueType();
            if (valueType == InputSystemExtensions.InputActionValueType.Bool)
            {
                inputAction.action.performed += InputActionCallback_Performed;
                inputAction.action.canceled += InputActionCallback_Canceled;
            }
            
            // subscribe to input system update loop
            InputSystem.onAfterUpdate += InputSystem_OnAfterUpdate;
        }

        private void OnDisable()
        {
            if (!inputAction) { return; }
            
            // unsubscribe from callbacks
            inputAction.action.performed -= InputActionCallback_Performed;
            inputAction.action.canceled -= InputActionCallback_Canceled;
            
            // unsubscribe from input system update loop
            InputSystem.onAfterUpdate -= InputSystem_OnAfterUpdate;
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Input Action Callbacks

        private void InputActionCallback_Performed(InputAction.CallbackContext context)
        {
            OnPressed.Invoke(this);
            isPressed = true;
        }

        private void InputActionCallback_Canceled(InputAction.CallbackContext context)
        {
            OnReleased.Invoke(this);
            isPressed = false;
        }

        private void InputSystem_OnAfterUpdate()
        {
            if (InputSystemExtensions.PollInput_AxisAsButton(inputAction, threshold, out bool axisIsPressed))
            {
                isPressed = axisIsPressed;
            }
            
            if (!isPressed) { return; }
            OnHeld.Invoke(this);
        }

        #endregion

    } // class end
}
