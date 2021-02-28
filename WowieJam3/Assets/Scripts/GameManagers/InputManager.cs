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
		public UnityEvent dash;

		// Awake is called when the script instance is being loaded.
		protected void Awake()
		{
			controls = new InputControls();
			EnableGameplayMap(true);
			
			//sign up methods
			controls.Gameplay.Movement.performed += Move;
			//have to subscribe to canceled event as well for Move vector to reset to 0
			//if don't subscribe, when player stops pressing keys, it will take last input forever
			controls.Gameplay.Movement.canceled += Move;
			
			controls.Gameplay.Dash.performed += Dash;
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
		
		//input receivers
	
		public void Move(InputAction.CallbackContext value)
		{
			var input = value.ReadValue<Vector2>();
			move?.Invoke(input);
		}
		
		public void Dash(InputAction.CallbackContext value)
		{
			dash?.Invoke();
		}
	}
}