using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace VGame
{
	public class Tramcar : MonoBehaviour
	{
		[SerializeField] private List<Transform> navPointTransforms;
		private List<Vector3> m_NavPoints;
		private Vector3 m_DefaultPosition;
		private Vector3 m_DefaultEulerAngles;

		private void Awake()
		{
			var transform1 = transform;
			m_DefaultPosition = transform1.position;
			m_DefaultEulerAngles = transform1.eulerAngles;
			m_NavPoints = new List<Vector3>();

			m_NavPoints.Add(m_DefaultPosition);
			foreach (var item in navPointTransforms)
			{
				m_NavPoints.Add(item.position);
			}
		}

		private void OnEnable()
		{
			transform.DOKill();
			transform.DOLocalPath(m_NavPoints.ToArray(), 10f).SetLookAt(0, Vector3.forward).SetEase(Ease.Linear)
				.SetLoops(-1, LoopType.Restart);
		}

		private void OnDisable()
		{
			var transform1 = transform;
			transform1.DOKill();
			transform1.position = m_DefaultPosition;
			transform1.eulerAngles = m_DefaultEulerAngles;
		}
	}
}