using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Mirror;

namespace VGame
{
	public class PlayerCameraLockout : MonoBehaviour
	{
		private CinemachineVirtualCamera m_CinemachineVirtualCamera => VGameManager.instance.m_CinemachineVirtualCamera;
		private CinemachineVirtualCamera m_MiniMapVirtualCamera => VGameManager.instance.m_MiniMapVirtualCamera;

		public void SetCameraFollowPlayer(bool isLocalPlayer)
		{
			if (isLocalPlayer)
			{
				m_CinemachineVirtualCamera.Follow = this.transform;
				m_MiniMapVirtualCamera.Follow = this.transform;
				Vector3 temPosition = new Vector3(transform.position.x, m_MiniMapVirtualCamera.transform.position.y, transform.position.z);
				m_MiniMapVirtualCamera.transform.position = temPosition;
			}
			else
			{
				VGameManager.instance.HideMiniMapComponent();
			}
		}
	}
}