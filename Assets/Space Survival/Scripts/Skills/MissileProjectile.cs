using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProjectile : MonoBehaviour
{
	public float damage;    // ������
	public float moveSpeed; // ����ü �ӵ�
	public float duration;

	CircleCollider2D coll;
	private void Awake()
	{
		coll = GetComponent<CircleCollider2D>();
	}


}
