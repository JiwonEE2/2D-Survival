using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
	public Transform target;
	// ������ ��� ��ġ
	// ���� ���� �ִ� Ÿ�� ���, ���� ��ġ�� ������ ����

	public MissileProjectile projectilePrefab;

	public float damage;
	public float projectileSpeed;       // ����ü �ӵ�
	public float projectileScale;       // ����ü ũ��
	public float shotInterval;          // ���� ����
}
