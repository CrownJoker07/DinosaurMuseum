using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityGameFramework.Runtime;
using DG.Tweening;

namespace VGame
{
	public class Customer : MonoBehaviour
	{
		private Animator m_Animator;
		private NavMeshAgent m_NavMeshAgent;
		private CustomerState m_CustomerState;
		private Transform m_NavTarget;

		private void Awake()
		{
			m_NavMeshAgent = GetComponent<NavMeshAgent>();
			m_Animator = GetComponentInChildren<Animator>();
		}

		private void Start()
		{
			GetNavTarget();
			SwitchCustomerState(CustomerState.Walk);
		}

		private void Update()
		{
			if (m_CustomerState != CustomerState.Walk)
				return;

			if (Time.frameCount % 10 == 0)
				return;

			if (CheckArrive(m_NavTarget.position))
			{
				SwitchCustomerState(CustomerState.Visit);
			}
		}

		// 如果另一个碰撞器进入了触发器，则调用 OnTriggerEnter
		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.layer != Constant.Layer.PlayerLayerId)
				return;

			if (m_CustomerState != CustomerState.Walk)
				return;

			transform.DOLookAt(other.transform.position, 0.5f);
			SwitchCustomerState(CustomerState.Interactive);
		}

		// 如果另一个碰撞器停止接触触发器，则调用 OnTriggerExit
		private void OnTriggerExit(Collider other)
		{
			if (other.gameObject.layer != Constant.Layer.PlayerLayerId)
				return;

			if (m_CustomerState != CustomerState.Interactive)
				return;

			KeepNaving();
		}

		private void SwitchCustomerState(CustomerState _CustomerState)
		{
			if (m_CustomerState == _CustomerState)
				return;

			m_CustomerState = _CustomerState;
			m_Animator.SetInteger("CustomerState", (int)m_CustomerState);

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
			m_NavMeshAgent.isStopped = false;

			if (m_NavMeshAgent.isOnNavMesh)
				m_NavMeshAgent.SetDestination(m_NavTarget.position);
		}

		private void KeepNaving()
		{
			SwitchCustomerState(CustomerState.Walk);
		}

		private void GetNavTarget()
		{
			m_NavTarget = VGameManager.Instance.GetNavPoint(BuildingArea.A);
		}

		private void InteractiveEvent()
		{
			//m_NavMeshAgent.velocity = Vector3.zero;
			m_NavMeshAgent.isStopped = true;
		}

		private void VisitEvent()
		{
			m_NavMeshAgent.velocity = Vector3.zero;

			transform.DORotate(m_NavTarget.eulerAngles, 0.5f).OnComplete(() =>
			{
				this.AttachTimer(10f, () =>
				{
					GetNavTarget();
					SwitchCustomerState(CustomerState.Walk);
				});
			});
		}

		private bool CheckArrive(Vector3 _TargetPosition)
		{
			return Vector3.SqrMagnitude(transform.position - _TargetPosition) <= Mathf.Pow(m_NavMeshAgent.stoppingDistance + 0.2f, 2)
				&& m_NavMeshAgent.pathStatus == NavMeshPathStatus.PathComplete;
		}
	}
}

