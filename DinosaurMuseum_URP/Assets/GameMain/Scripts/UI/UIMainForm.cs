//------------------------------------------------------------
// Author: zeweizhuang
// Email: 3462294546@qq.com
//------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
			m_ProcedureMain.GotoMenu();
		}
	}
}