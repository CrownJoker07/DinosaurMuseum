//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using GameFramework.DataTable;
using UnityEngine;

namespace EasyARProject
{
	public class SurvivalGame : GameBase
	{
		public override GameMode GameMode
		{
			get
			{
				return GameMode.Survival;
			}
		}

		public override void Update(float elapseSeconds, float realElapseSeconds)
		{
			base.Update(elapseSeconds, realElapseSeconds);
		}
	}
}