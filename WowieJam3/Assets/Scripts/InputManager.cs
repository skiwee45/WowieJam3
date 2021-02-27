using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace GameManagers
{
	[Serializable]
	public class Vector2Event : UnityEvent<Vector2>{}
	
	public class InputManager : MonoBehaviour
	{
		InputControls controls;
	
		//unityevents to call
		public Vector2Event move;

		// Awake is called when the script instance is being loaded.
		protected void Awake()
		{
			controls = new InputControls();
			EnableGameplayMap(true);
			//sign up methods
		}

		//special method to enable / disable maps
		public void EnableGameplayMap(bool enable)
		{
			if (enable)
			{
				controls.Gameplay.Enable();
			} else
			{
				controls.Gameplay.Disable();
			}
		}
	
		public void MoveInput(InputAction.CallbackContext value)
		{
			var input = value.ReadValue<Vector2>();
			move?.Invoke(input);
		}
	}
}