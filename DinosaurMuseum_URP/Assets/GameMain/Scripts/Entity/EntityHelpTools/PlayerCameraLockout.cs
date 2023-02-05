using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Mirror;

namespace VGame
{
	public class PlayerCameraLockout : MonoBehaviour
	{
		private CinemachineVirtualCamera m_CinemachineVirtualCamera => VGameManager.Instance.m_CinemachineVirtualCamera;

		public void SetCameraFollowPlayer(bool isLocalPlayer)
		{
			if (isLocalPlayer)
			{
				m_CinemachineVirtualCamera.Follow = this.transform;
			}
		}
	}
}