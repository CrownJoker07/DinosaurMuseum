using System;
using UnityEngine;
using Mirror;
using UnityEngine.Serialization;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;

#endif

namespace VGame
{
	public class StarterAssetsInputs : NetworkBehaviour
	{
		[Header("Character Input Values")] public Vector2 move;

		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool cursorMode;
		public bool hasMessageBoxUI;

		[Header("Movement Settings")] public bool analogMovement;

		[Header("Mouse Cursor Settings")] public bool cursorLocked = true;

		public bool cursorInputForLook = true;

		private void Awake()
		{
			VGameManager.instance.starterAssetsInputs = this;
		}

		private void Start()
		{
			SetCursorState(true);
			
			GameEntry.UI.OpenUIForm(UIFormId.NewPlayerGuideUI);
		}

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED

		public void OnMove(InputValue value)
		{
			if ((Cursor.lockState == CursorLockMode.Locked) && !cursorMode)
				MoveInput(value.Get<Vector2>());
			// else
			// 	MoveInput(Vector2.zero);
		}

		public void OnLook(InputValue value)
		{
			if (cursorInputForLook && (Cursor.lockState == CursorLockMode.Locked) && !cursorMode)
				LookInput(value.Get<Vector2>());
			
			// else
			// {
			// 	LookInput(Vector2.zero);
			// }
		}

		public void OnJump(InputValue value)
		{
			if ((Cursor.lockState == CursorLockMode.Locked) && !cursorMode)
				JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			if ((Cursor.lockState == CursorLockMode.Locked) && !cursorMode)
				SprintInput(value.isPressed);
		}

		public void OnCursorMode(InputValue value)
		{
			if (hasMessageBoxUI) return;
			
			CursorModeInput(value.isPressed);
		}

#endif

		public void MoveInput(Vector2 newMoveDirection)
		{
			if (!isLocalPlayer)
				return;

			move = newMoveDirection;
		}

		public void LookInput(Vector2 newLookDirection)
		{
			if (!isLocalPlayer)
				return;

			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			if (!isLocalPlayer)
				return;

			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			if (!isLocalPlayer)
				return;

			sprint = newSprintState;
		}

		private void CursorModeInput(bool mode)
		{
			if (!isLocalPlayer)
				return;

			cursorMode = mode;
			SetCursorState(!cursorMode);
		}

		public void UIInput(bool state)
		{
			hasMessageBoxUI = state;
			CursorModeInput(state);
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			if (hasMessageBoxUI) return;
			
			SetCursorState(hasFocus);
		}

		private void SetCursorState(bool newState)
		{
			if (!isLocalPlayer)
				return;

			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;

			if (Cursor.lockState != CursorLockMode.None) return;
			
			MoveInput(Vector2.zero);
			LookInput(Vector2.zero);
		}
	}
}