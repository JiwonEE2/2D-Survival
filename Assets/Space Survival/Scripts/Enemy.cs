using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float hp = 10f;        // 체력
	public float damage = 10f;    // 공격력
	public float moveSpeed = 3f;  // 이동속도

	public Transform target;      // 추적할 대상

	private void Start()
	{
		target = GameObject.Find("Player").transform;
	}
}
