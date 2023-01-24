using System;

namespace VGame
{
	public class Singleton<T> where T : class, new()
	{
		private static T s_instance;

		protected Singleton()
		{
		}

		public static T Instance
		{
			get
			{
				if (Singleton<T>.s_instance == null)
				{
					Singleton<T>.CreateInstance();
				}
				return Singleton<T>.s_instance;
			}
		}

		#region 对外辅助接口

		public static void CreateInstance()
		{
			if (Singleton<T>.s_instance == null)
			{
				Singleton<T>.s_instance = Activator.CreateInstance<T>();
			}
		}

		public static void DestroyInstance()
		{
			if (Singleton<T>.s_instance != null)
			{
				Singleton<T>.s_instance = null;
			}
		}

		public static bool HasInstance()
		{
			return (Singleton<T>.s_instance != null);
		}

		#endregion 对外辅助接口
	}
}