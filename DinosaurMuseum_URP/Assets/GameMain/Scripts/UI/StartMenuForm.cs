//------------------------------------------------------------
// Author: zeweizhuang
// Email: 3462294546@qq.com
//------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using Mirror;

namespace VGame
{
	public class StartMenuForm : UGuiForm
	{
		private ProcedureMenu m_ProcedureMenu = null;
		private NetworkManager networkManager;

		protected override void OnOpen(object userData)
		{
			base.OnOpen(userData);
			m_ProcedureMenu = (ProcedureMenu)userData;
			networkManager = GameEntry.networkManager;
		}

		protected override void OnClose(bool isShutdown, object userData)
		{
			base.OnClose(isShutdown, userData);
		}

		/// <summary>
		/// 开始游戏
		/// </summary>
		public void OnClick_StartGame()
		{
			m_ProcedureMenu.StartGame();
			networkManager.StartHost();
		}

		/// <summary>
		/// 加入游戏
		/// </summary>
		public void OnClick_JoinGame()
		{
		}

		/// <summary>
		/// 退出游戏
		/// </summary>
		public void OnClick_QuitGame()
		{
			UnityGameFramework.Runtime.GameEntry.Shutdown(ShutdownType.Quit);
		}
	}
}