//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace VGame
{
	/// <summary>
	/// 界面编号。
	/// </summary>
	public enum UIFormId : byte
	{
		Undefined = 0,

		/// <summary>
		/// 弹出框。
		/// </summary>
		DialogForm = 1,

		/// <summary>
		/// 主界面UI。
		/// </summary>
		UIMainForm = 100,

		/// <summary>
		/// 开始界面。
		/// </summary>
		StartMenuForm = 101,

		/// <summary>
		/// 关于。
		/// </summary>
		AboutForm = 102,
	}
}