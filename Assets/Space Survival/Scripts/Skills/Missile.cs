using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
		StartCoroutine(FireCoroutine());
	}

	private IEnumerator FireCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(shotInterval);
			Fire();
		}
	}

	private void Fire()
	{
		// ���� Vector2 �������� ���ؼ� ����ü�� ����
		Vector2 pos = Random.insideUnitCircle * maxDist;

		// ���� ��ġ�� �� ������Ʈ ���� �� transform�� �������� ���� �ۼ�
		//target.position = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
		MissileProjectile proj = Instantiate(projectilePrefab, pos, Quaternion.identity);
		proj.damage = damage;
		proj.duration = 1 / projectileSpeed;
	}
}
