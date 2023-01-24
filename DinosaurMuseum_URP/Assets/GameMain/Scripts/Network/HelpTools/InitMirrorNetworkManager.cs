using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace VGame
{
	public class InitMirrorNetworkManager : MonoBehaviour
	{
		private void Awake()
		{
			NetworkManager Tem_NetworkManager = GetComponent<NetworkManager>();
			GameEntry.SetnetworkManager(Tem_NetworkManager);
		}
	}
}