//------------------------------------------------------------
// Author: zeweizhuang
// Email: 3462294546@qq.com
//------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace VGame
{
	public class UIMainForm : UGuiForm
	{
		private ProcedureMain m_ProcedureMain = null;

		protected override void OnOpen(object userData)
		{
			base.OnOpen(userData);
			m_ProcedureMain = (ProcedureMain)userData;
		}

		protected override void OnClose(bool isShutdown, object userData)
		{
			base.OnClose(isShutdown, userData);
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