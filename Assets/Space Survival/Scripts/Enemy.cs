using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float hp = 10f;        // ü��
	public float damage = 10f;    // ���ݷ�
	public float moveSpeed = 3f;  // �̵��ӵ�

	public Transform target;      // ������ ���

	private void Start()
	{
		target = GameObject.Find("Player").transform;
	}
}
