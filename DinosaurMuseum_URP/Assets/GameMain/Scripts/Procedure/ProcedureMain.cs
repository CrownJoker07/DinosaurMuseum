//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;
using UnityGameFramework.Runtime;
using GameFramework.Event;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace VGame
{
	public class ProcedureMain : ProcedureBase
	{
		public static GameMode gameMode;
		private GameBase m_CurrentGame;
		private bool m_LeaveGame;
		private UIMainForm m_UIMainForm;
		private string m_NetworkAddress;

		public override bool UseNativeDialog => false;

		public void GotoMenu()
		{
			m_LeaveGame = true;
		}

		// protected override void OnInit(ProcedureOwner procedureOwner)
		// {
		// 	base.OnInit(procedureOwner);
		// }
		//
		// protected override void OnDestroy(ProcedureOwner procedureOwner)
		// {
		// 	base.OnDestroy(procedureOwner);
		// }

		protected override void OnEnter(ProcedureOwner procedureOwner)
		{
			base.OnEnter(procedureOwner);

			m_LeaveGame = false;
			gameMode = (GameMode)procedureOwner.GetData<VarByte>("GameMode").Value;
			m_NetworkAddress = procedureOwner.GetData<VarString>("networkAddress").Value;

			m_CurrentGame?.Initialize();

			GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
			GameEntry.UI.OpenUIForm(UIFormId.UIMainForm, this);

			VGameManager.instance.SetNetworkAddress(m_NetworkAddress);
			VGameManager.instance.StartNetwork();
		}

		protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
		{
			if (m_CurrentGame != null)
			{
				m_CurrentGame.Shutdown();
				m_CurrentGame = null;
			}

			base.OnLeave(procedureOwner, isShutdown);

			GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

			if (m_UIMainForm == null) return;
			
			m_UIMainForm.Close(isShutdown);
			m_UIMainForm = null;
		}

		protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
		{
			base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

			if (!m_LeaveGame) return;
			
			procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.StartMenu"));
			ChangeState<ProcedureChangeScene>(procedureOwner);
		}

		private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
		{
			var ne = (OpenUIFormSuccessEventArgs)e;
			if (ne.UserData != this)
			{
				return;
			}

			m_UIMainForm = (UIMainForm)ne.UIForm.Logic;
		}
	}
}