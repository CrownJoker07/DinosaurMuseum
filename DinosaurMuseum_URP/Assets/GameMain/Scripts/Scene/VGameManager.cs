using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace VGame
{
	public partial class VGameManager : MonoBehaviour
	{
		[HideInInspector] public static VGameManager Instance;

		public CinemachineVirtualCamera m_CinemachineVirtualCamera;
		public NetworkManager m_NetworkManager;

		public Player m_Player;

		private void Awake()
		{
			if (Instance == null)
				Instance = this;
		}

		public void StartNetwork()
		{
			if (ProcedureMain.m_GameMode == GameMode.Server)
			{
				m_NetworkManager.StartHost();
			}
			else if (ProcedureMain.m_GameMode == GameMode.Client)
			{
				m_NetworkManager.StartClient();
			}
		}
	}
}