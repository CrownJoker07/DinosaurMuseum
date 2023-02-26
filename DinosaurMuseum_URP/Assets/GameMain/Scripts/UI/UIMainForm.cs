//------------------------------------------------------------
// Author: zeweizhuang
// Email: 3462294546@qq.com
//------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using System.Net.NetworkInformation;

namespace VGame
{
	public class UIMainForm : UGuiForm
	{
		private ProcedureMain m_ProcedureMain = null;
		[SerializeField] private TextMeshProUGUI roomIP;

		protected override void OnOpen(object userData)
		{
			base.OnOpen(userData);
			m_ProcedureMain = (ProcedureMain)userData;
			roomIP.text = GetIP();
		}

		protected override void OnClose(bool isShutdown, object userData)
		{
			base.OnClose(isShutdown, userData);
		}


		/// <summary>
		/// 获取本机IP
		/// </summary>
		/// <returns>string :ip地址</returns>
		public string GetIP()
		{
			string output = "";

			foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
			{
				NetworkInterfaceType _type1 = NetworkInterfaceType.Wireless80211;  //无线局域网适配器 

				if ((item.NetworkInterfaceType == _type1) && item.OperationalStatus == OperationalStatus.Up)
				{
					foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
					{
						if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
						{
							output = ip.Address.ToString();
						}
					}
				}
			}
			return output;
		}

		/// <summary>
		/// 返回按钮点击事件
		/// </summary>
		public void OnClick_Back()
		{
			if (NetworkServer.active && NetworkClient.isConnected)
			{
				VGameManager.Instance.m_NetworkManager.StopHost();
				VGameManager.Instance.m_NetworkManager.StopClient();
			}
			// stop client if client-only
			else if (NetworkClient.isConnected)
			{
				VGameManager.Instance.m_NetworkManager.StopClient();
			}
			// stop server if server-only
			else if (NetworkServer.active)
			{
				VGameManager.Instance.m_NetworkManager.StopServer();
			}
			m_ProcedureMain.GotoMenu();
		}
	}
}