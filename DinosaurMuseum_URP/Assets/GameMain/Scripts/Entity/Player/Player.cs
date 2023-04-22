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
				VGameManager.instance.m_Player = this;
				GameEntry.UI.OpenUIForm(UIFormId.NewPlayerGuideUI);
			}
			else
			{
				this.gameObject.layer = Constant.Layer.OtherPlayerLayerId;
			}
		}
	}
}

