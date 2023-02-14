using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityGameFramework.Runtime;

namespace VGame
{
	public class Customer : MonoBehaviour
	{
		private Animator m_Animator;
		private NavMeshAgent m_NavMeshAgent;
		private Rigidbody m_Rigidbody;
		private CustomerState m_CustomerState;
		private Transform m_NavTarget;

		private void Awake()
		{
			m_NavMeshAgent = GetComponent<NavMeshAgent>();
			m_Rigidbody = GetComponent<Rigidbody>();
			m_Animator = GetComponentInChildren<Animator>();

			SwitchCustomerState(CustomerState.Walk);
		}

		private void Update()
		{
			if (CheckArrive(m_NavTarget.position))
			{
				WalkEvent();
			}
		}

		private void SwitchCustomerState(CustomerState _CustomerState)
		{
			if (m_CustomerState == _CustomerState)
				return;

			m_CustomerState = _CustomerState;

			switch (m_CustomerState)
			{
				case CustomerState.Walk:
					WalkEvent();
					break;
				case CustomerState.Interactive:
					InteractiveEvent();
					break;
				case CustomerState.Visit:
					VisitEvent();
					break;
			}
		}

		private void WalkEvent()
		{
			GetNavTarget();

			if (m_NavMeshAgent.isOnNavMesh)
				m_NavMeshAgent.SetDestination(m_NavTarget.position);
		}

		private void GetNavTarget()
		{
			Log.Error("123");
			m_NavTarget = VGameManager.Instance.GetNavPoint(BuildingArea.A);
		}

		private void InteractiveEvent()
		{

		}

		private void VisitEvent()
		{

		}

		private bool CheckArrive(Vector3 _TargetPosition)
		{
			Log.Error("{0},{1}", Vector3.SqrMagnitude(transform.position - _TargetPosition), Mathf.Pow(m_NavMeshAgent.stoppingDistance + 0.2f, 2));
			return Vector3.SqrMagnitude(transform.position - _TargetPosition) <= Mathf.Pow(m_NavMeshAgent.stoppingDistance + 0.2f, 2)
				&& m_NavMeshAgent.pathStatus == NavMeshPathStatus.PathComplete;
		}
	}
}

