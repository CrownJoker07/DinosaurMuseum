using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace VGame
{
	public class Customer : MonoBehaviour
	{
		[SerializeField] private Animator m_Animator;

		private NavMeshAgent m_NavMeshAgent;
		private Rigidbody m_Rigidbody;
		private CustomerState m_CustomerState;

		private void Awake()
		{
			m_NavMeshAgent = GetComponent<NavMeshAgent>();
			m_Rigidbody = GetComponent<Rigidbody>();
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

		}

		private void InteractiveEvent()
		{

		}

		private void VisitEvent()
		{

		}
	}
}

