using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
			m_NavTarget = VGameManager.Instance.GetNavPoint(BuildingArea.A);
		}

		private void InteractiveEvent()
		{

		}

		private void VisitEvent()
		{

		}
	}
}

