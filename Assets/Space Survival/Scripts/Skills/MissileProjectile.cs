using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProjectile : MonoBehaviour
{
	public float damage;    // 데미지
	public float moveSpeed; // 투사체 속도
	public float duration;

	CircleCollider2D coll;
	private void Awake()
	{
		coll = GetComponent<CircleCollider2D>();
	}


}
