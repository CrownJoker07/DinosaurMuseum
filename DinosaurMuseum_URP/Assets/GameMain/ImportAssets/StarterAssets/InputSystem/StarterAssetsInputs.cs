using UnityEngine;
using Mirror;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED

using UnityEngine.InputSystem;

#endif

namespace VGame
{
	public class StarterAssetsInputs : NetworkBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;

		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;

		public bool cursorInputForLook = true;

		private void Start()
		{
			SetCursorState(true);
		}

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED

		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if (cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
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

		private void OnApplicationFocus(bool hasFocus)
		{
#if UNITY_EDITOR
			SetCursorState(hasFocus);
#else
			SetCursorState(cursorLocked);
#endif
		}

		private void SetCursorState(bool newState)
		{
			if (!isLocalPlayer)
				return;

			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
}