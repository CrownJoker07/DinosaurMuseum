using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGame
{
	public class NavPoint : MonoBehaviour
	{
		[SerializeField] private BuildingArea m_BuildingArea;

		private void Awake()
		{
			VGameManager.Instance.AddNavPoint(m_BuildingArea, this.transform);
		}
	}
}

