//------------------------------------------------------------
// Author: zeweizhuang
// Email: 3462294546@qq.com
//------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TargetObject : MonoBehaviour
{
	[Header("模型")]
	[SerializeField] private GameObject Meshes;

	[SerializeField] private Ease m_Ease = Ease.Linear;

	private Sequence m_sequence;

	private void Start()
	{
		MeshFloat();
	}

	/// <summary>
	/// 模型浮动
	/// </summary>
	private void MeshFloat()
	{
		m_sequence = DOTween.Sequence();
		m_sequence.Append(Meshes.transform.DOLocalMoveZ(-0.226f, 1f).SetEase(m_Ease))
							.SetLoops(-1, LoopType.Yoyo);
	}
}