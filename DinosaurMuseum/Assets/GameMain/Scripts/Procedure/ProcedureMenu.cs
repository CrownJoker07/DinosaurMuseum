//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Event;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace EasyARProject
{
	public class ProcedureMenu : ProcedureBase
	{
		private bool m_StartGame = false;
		private StartMenuForm m_StartMenuForm = null;

		public override bool UseNativeDialog
		{
			get
			{
				return false;
			}
		}

		public void StartGame()
		{
			m_StartGame = true;
		}

		protected override void OnEnter(ProcedureOwner procedureOwner)
		{
			base.OnEnter(procedureOwner);

			GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

			m_StartGame = false;
			GameEntry.UI.OpenUIForm(UIFormId.StartMenuForm, this);
		}

		protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
		{
			base.OnLeave(procedureOwner, isShutdown);

			GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

			if (m_StartMenuForm != null)
			{
				m_StartMenuForm.Close(isShutdown);
				m_StartMenuForm = null;
			}
		}

		protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
		{
			base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

			if (m_StartGame)
			{
				procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.ImageTracking_Targets"));
				procedureOwner.SetData<VarByte>("GameMode", (byte)GameMode.Survival);
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

			m_StartMenuForm = (StartMenuForm)ne.UIForm.Logic;
		}
	}
}