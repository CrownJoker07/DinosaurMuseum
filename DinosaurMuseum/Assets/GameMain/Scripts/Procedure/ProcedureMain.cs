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
		private const float GameOverDelayedSeconds = 2f;

		private readonly Dictionary<GameMode, GameBase> m_Games = new Dictionary<GameMode, GameBase>();
		private GameBase m_CurrentGame = null;
		private bool m_LeaveGame = false;
		private UIMainForm m_UIMainForm = null;

		public override bool UseNativeDialog
		{
			get
			{
				return false;
			}
		}

		public void GotoMenu()
		{
			m_LeaveGame = true;
		}

		protected override void OnInit(ProcedureOwner procedureOwner)
		{
			base.OnInit(procedureOwner);

			m_Games.Add(GameMode.Survival, new SurvivalGame());
		}

		protected override void OnDestroy(ProcedureOwner procedureOwner)
		{
			base.OnDestroy(procedureOwner);

			m_Games.Clear();
		}

		protected override void OnEnter(ProcedureOwner procedureOwner)
		{
			base.OnEnter(procedureOwner);

			m_LeaveGame = false;
			GameMode gameMode = (GameMode)procedureOwner.GetData<VarByte>("GameMode").Value;
			m_CurrentGame = m_Games[gameMode];
			m_CurrentGame.Initialize();

			GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
			GameEntry.UI.OpenUIForm(UIFormId.UIMainForm, this);
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

			if (m_UIMainForm != null)
			{
				m_UIMainForm.Close(isShutdown);
				m_UIMainForm = null;
			}
		}

		protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
		{
			base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

			if (m_LeaveGame)
			{
				procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.StartMenu"));
				ChangeState<ProcedureChangeScene>(procedureOwner);
			}
		}

		private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
		{
			OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
			if (ne.UserData != this)
			{
				return;
			}

			m_UIMainForm = (UIMainForm)ne.UIForm.Logic;
		}
	}
}