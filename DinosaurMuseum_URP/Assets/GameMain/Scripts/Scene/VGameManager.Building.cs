using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using GameFramework;

namespace VGame
{
	public partial class VGameManager : MonoBehaviour
	{
		private Dictionary<BuildingArea, List<Transform>> NavPoints = new Dictionary<BuildingArea, List<Transform>>();

		/// <summary>
		/// 添加地图导航点
		/// </summary>
		/// <param name="_BuildingArea"></param>
		/// <param name="_Transform"></param>
		public void AddNavPoint(BuildingArea _BuildingArea, Transform _Transform)
		{
			if (_BuildingArea == BuildingArea.None)
				return;

			if (!NavPoints.ContainsKey(_BuildingArea))
			{
				List<Transform> Tem_transforms = new List<Transform>();
				NavPoints[_BuildingArea] = Tem_transforms;
			}
			NavPoints[_BuildingArea].Add(_Transform);
		}

		public Transform GetNavPoint(BuildingArea _BuildingArea)
		{
			if (!NavPoints.ContainsKey(_BuildingArea))
				return null;

			int count = NavPoints[_BuildingArea].Count;
			count = Utility.Random.GetRandom(count);

			return NavPoints[_BuildingArea][count];
		}
	}
}