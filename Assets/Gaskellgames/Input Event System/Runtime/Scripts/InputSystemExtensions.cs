using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gaskellgames.InputEventSystem
{
    /// <summary>
	/// Code created by Gaskellgames
	/// </summary>

	public static class InputSystemExtensions
	{
		private enum InputActionControlType
		{
			Any,
			Analog,
			Axis,
			Bone,
			Button,
			Delta,
			Digital,
			Double,
			Dpad,
			Eyes,
			Integer,
			Pose,
			Quaternion,
			Stick,
			Touch,
			Vector2,
			Vector3
		}
		
		public enum InputActionValueType
		{
			/// <summary>Used for any other or unknown value type. (Likely not yet supported by <see cref="Gaskellgames.InputEventSystem"/>)</summary>
			Unknown,
			
			/// <summary>The value cannot be anything other than 0 or 1.</summary>
			Bool,
			
			/// <summary>A 1D floating-point axis.</summary>
			Float,
			
			/// <summary>A 2D floating-point vector.</summary>
			Vector2,
		}
		
		private static InputActionControlType ControlType(this InputAction inputAction)
		{
			string expectedControlType = inputAction.expectedControlType;
			
			if (expectedControlType == "Analog") { return InputActionControlType.Analog; }
			else if (expectedControlType == "Axis") { return InputActionControlType.Axis; }
			else if (expectedControlType == "Bone") { return InputActionControlType.Bone; }
			else if (expectedControlType == "Button") { return InputActionControlType.Button; }
			else if (expectedControlType == "Delta") { return InputActionControlType.Delta; }
			else if (expectedControlType == "Digital") { return InputActionControlType.Digital; }
			else if (expectedControlType == "Double") { return InputActionControlType.Double; }
			else if (expectedControlType == "Dpad") { return InputActionControlType.Dpad; }
			else if (expectedControlType == "Eyes") { return InputActionControlType.Eyes; }
			else if (expectedControlType == "Integer") { return InputActionControlType.Integer; }
			else if (expectedControlType == "Pose") { return InputActionControlType.Pose; }
			else if (expectedControlType == "Quaternion") { return InputActionControlType.Quaternion; }
			else if (expectedControlType == "Stick") { return InputActionControlType.Stick; }
			else if (expectedControlType == "Touch") { return InputActionControlType.Touch; }
			else if (expectedControlType == "Vector2") { return InputActionControlType.Vector2; }
			else if (expectedControlType == "Vector3") { return InputActionControlType.Vector3; }
			else { return InputActionControlType.Any; } // if (expectedControlType == "Any")
		}
		
		/// <summary>
		/// The output value type for this InputAction
		/// </summary>
		/// <param name="inputAction"></param>
		/// <returns></returns>
		public static InputActionValueType ValueType(this InputAction inputAction)
		{
			switch (inputAction.ControlType())
			{
				case InputActionControlType.Button:
					return InputActionValueType.Bool;
				
				case InputActionControlType.Axis:
					return InputActionValueType.Float;
				
				case InputActionControlType.Vector2:
					return InputActionValueType.Vector2;
				
				// other values (not yet used / supported by Gaskellgames.InputEventSystem)
				case InputActionControlType.Any:
				case InputActionControlType.Analog:
				case InputActionControlType.Bone:
				case InputActionControlType.Delta:
				case InputActionControlType.Digital:
				case InputActionControlType.Double:
				case InputActionControlType.Dpad:
				case InputActionControlType.Eyes:
				case InputActionControlType.Integer:
				case InputActionControlType.Pose:
				case InputActionControlType.Quaternion:
				case InputActionControlType.Stick:
				case InputActionControlType.Touch:
				case InputActionControlType.Vector3:
				default:
					return InputActionValueType.Unknown;
			}
		}

		/// <summary>
		/// Check if an axis input is being pressed
		/// </summary>
		/// <param name="inputAction"></param>
		/// <param name="threshold"></param>
		/// <param name="axisAsButton"></param>
		/// <returns>True is InputAction is being polled (InputAction ValueType is float or Vector2)</returns>
		public static bool PollInput_AxisAsButton(InputAction inputAction, float threshold, out bool axisAsButton)
		{
			switch (inputAction.ValueType())
			{
				case InputActionValueType.Bool:
					// handled by callbacks
					axisAsButton = false;
					return false;
                
				case InputActionValueType.Float:
					float floatValue = inputAction.ReadValue<float>();
					axisAsButton = threshold < floatValue;
					return true;
                
				case InputActionValueType.Vector2:
					Vector2 vector2Value = inputAction.ReadValue<Vector2>();
					axisAsButton = (threshold < Mathf.Abs(vector2Value.x)) || (threshold < Mathf.Abs(vector2Value.y));
					return true;
                
				default: // InputSystemExtensions.InputActionValueType.Unknown
					axisAsButton = false;
					return false;
			}
		}
		
		/// <summary>
		/// Get the valid InputControlScheme for a specified InputDevice from a list of InputActionAssets
		/// </summary>
		/// <param name="inputActionAssets"></param>
		/// <param name="inputDevice"></param>
		/// <param name="controlScheme"></param>
		/// <returns>True if InputControlScheme, false otherwise</returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static bool TryFindControlScheme(List<InputActionAsset> inputActionAssets, InputDevice inputDevice, out InputControlScheme controlScheme)
		{
			// try to get the control scheme directly from the device
			foreach (InputActionAsset inputActionAsset in inputActionAssets)
			{
				InputControlScheme? foundControlScheme = InputControlScheme.FindControlSchemeForDevice(inputDevice, inputActionAsset.controlSchemes);
				controlScheme = foundControlScheme ?? inputActionAsset.controlSchemes[0];
				if (foundControlScheme.HasValue) { return true; }
			}
            
			// try to get the control scheme from inputActionAsset's controlSchemes' supported devices
			foreach (var inputActionAsset in inputActionAssets)
			{
				foreach (InputControlScheme inputControlScheme in inputActionAsset.controlSchemes)
				{
					if (inputControlScheme.SupportsDevice(inputDevice))
					{
						controlScheme = inputControlScheme;
						return true;
					}
				}
			}

			// unable to find control scheme
			if (inputActionAssets[0] == null) { throw new ArgumentNullException(nameof(inputActionAssets), "inputAction [0] is null."); }
			controlScheme = inputActionAssets[0].controlSchemes[0];
			return false;
		}
		
	} // class end
}