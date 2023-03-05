using UnityEngine;


namespace VGame
{
	public class Player : MonoBehaviour
	{
		[HideInInspector] public bool hasCustomerInteractive;

		public void OnInit(bool isLocalPlayer)
		{
			if (isLocalPlayer)
			{
				VGameManager.Instance.m_Player = this;
			}
			else
			{
				this.gameObject.layer = Constant.Layer.OtherPlayerLayerId;
			}
		}
	}
}

