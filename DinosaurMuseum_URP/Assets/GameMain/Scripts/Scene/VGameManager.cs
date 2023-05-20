using System;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace VGame
{
	public partial class VGameManager : MonoBehaviour
	{
		public static VGameManager instance;

		public CinemachineVirtualCamera m_CinemachineVirtualCamera;
		public CinemachineVirtualCamera m_MiniMapVirtualCamera;
		public Camera m_MiniMapCamera;
		public NetworkManager m_NetworkManager;
		public StarterAssetsInputs starterAssetsInputs;

		[HideInInspector] public Player m_Player;

		private void Awake()
		{
			if (instance == null)
				instance = this;
		}

		private void OnDestroy()
		{
			Destroy(m_NetworkManager.gameObject);
			instance = null;
		}

		public void StartNetwork()
		{
			switch (ProcedureMain.gameMode)
			{
				case GameMode.Server:
					m_NetworkManager.StartHost();
					break;
				case GameMode.Client:
					m_NetworkManager.StartClient();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		// public void HideMiniMapComponent()
		// {
		// 	m_MiniMapCamera.gameObject.SetActive(false);
		// 	m_MiniMapVirtualCamera.gameObject.SetActive(false);
		// }

		public void SetNetworkAddress(string networkAddress)
		{
			m_NetworkManager.networkAddress = networkAddress;
		}
	}
}