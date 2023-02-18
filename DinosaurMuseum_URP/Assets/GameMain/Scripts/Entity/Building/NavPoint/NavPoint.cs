using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGame
{
	public class NavPoint : MonoBehaviour
	{
		private BuildingArea m_BuildingArea;
		private Area m_Area;

		private void Awake()
		{
			m_Area = GetComponentInParent<Area>();
			m_BuildingArea = m_Area.m_BuildingArea;
			transform.SetPositionY(0);
			VGameManager.Instance.AddNavPoint(m_BuildingArea, this.transform);
		}
	}
}

