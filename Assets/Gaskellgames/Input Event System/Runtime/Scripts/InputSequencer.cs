using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gaskellgames.InputEventSystem
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    [AddComponentMenu("Gaskellgames/Input Event System/Input Sequencer")]
    public class InputSequencer : GGMonoBehaviour
    {
        #region Variables

        [InfoBox("Inputs must be of value type Button (bool)")]
        [SerializeField, Required]
        [Tooltip("Triggered when any input received")]
        private InputActionReference anyInput;

        [SerializeField, Required, Space]
        [Tooltip("Sequence of inputs to be listened out for")]
        private List<InputActionReference> inputSequence;
        
        [SerializeField]
        public GgEvent<InputSequencer> onSequenceProgressed;
        
        [SerializeField, Space]
        public GgEvent<InputSequencer> onSequenceComplete;
        
        [SerializeField, Space]
        public GgEvent<InputSequencer> onSequenceReset;

        [SerializeField, ReadOnly]
        [Tooltip("Track current index value of the sequence.")]
        private int index;

        [SerializeField, ReadOnly]
        [Tooltip("Has the sequence been successfully entered?")]
        private bool sequenceComplete;

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

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Input Action Callbacks

        private void InputSystem_OnAfterUpdate()
        {
            HandleInput();
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Private Functions

        private void HandleInput()
        {
            // check if pointer is within sequence ...
            if (index < inputSequence.Count)
            {
                // ... within sequence: check for correct input
                if (inputSequence[index].action.WasPerformedThisFrame())
                {
                    // correct input
                    index++;
                    onSequenceProgressed.Invoke(this);
                }
                else if (anyInput.action.WasPerformedThisFrame() && inputSequence[index] != anyInput)
                {
                    // incorrect input: reset sequence
                    index = 0;
                    sequenceComplete = false;
                    onSequenceReset.Invoke(this);
                }
            }
            else
            {
                // ... end of sequence: check for completion
                if (!sequenceComplete)
                {
                    // set complete
                    sequenceComplete = true;
                    onSequenceComplete.Invoke(this);
                }
                else if (sequenceComplete && anyInput.action.WasPerformedThisFrame())
                {
                    // complete: input: reset sequence
                    index = 0;
                    sequenceComplete = false;
                    onSequenceReset.Invoke(this);
                }
            }
        }

        #endregion

    } // class end
}
