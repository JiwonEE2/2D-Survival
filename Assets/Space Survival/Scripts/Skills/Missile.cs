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
	public float projectileSpeed; // ����ü �ӵ�
	public float projectileScale; // ����ü ũ��
	public float shotInterval;    // ���� ����

	public float maxDist;         // �ִ� Ÿ�� �Ÿ�

	private void Start()
	{

	}

	protected virtual IEnumerator FireCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(shotInterval);
			Fire();
		}
	}

	protected virtual void Fire()
	{
		MissileProjectile proj = Instantiate(projectilePrefab);
	}
}
