using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGame
{
	public class Player : MonoBehaviour
	{
		[HideInInspector] public bool HasCustomerInteractive = false;

		public void OnInit(bool _isLocalPlayer)
		{
			if (_isLocalPlayer)
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

