using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

// �⺻ ����. ����ü�� �߻��ϴ� ��ų
public class LaserGun : MonoBehaviour
{
	public Transform target;            // ����ü�� ���ؾ� �� ���⿡ �ִ� ���
	public Projectile projectilePrefab; // ����ü ������
	public ProjectilePool projPool; // Projectile Prefab���� ������� ���� ������Ʈ�� �����ϴ� ������ƮǮ

	public float damage;                // ������
	public float projectileSpeed;       // ����ü �ӵ�
	public float projectileScale;       // ����ü ũ��
	public float shotInterval;          // ���� ����

	public int projectileCount;         // ����ü ���� 1~5
	public float innerInterval;
	[Tooltip("���� Ƚ���Դϴ�\n������ ������ ���� ��� 0")]
	public int pierceCount;             // ���� Ƚ��

	protected virtual void Start()
	{
		StartCoroutine(FireCoroutine());
	}

	protected virtual void Update()
	{
		if (target == null)
		{
			return;
		}
		transform.up = target.position - transform.position;
	}

	// ���� �ڷ�ƾ
	protected virtual IEnumerator FireCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(shotInterval);
			// 1. ����ü ������ �ö󰡸� 0.05�� �������� ����ü ������ŭ �߻� �ݺ�
			for (int i = 0; i < projectileCount; i++)
			{
				Fire();
				yield return new WaitForSeconds(innerInterval);
			}
		}
	}

	protected virtual void Fire()
	{
		Projectile proj =
			// �Ϲ������� ����Ƽ���� ��ü�� ������ ��
			//Instantiate(projectilePrefab, transform.position, transform.rotation);

			// Ŀ���� ������Ʈ Ǯ�� ����� ��
			//projPool.Pop();
			//proj.transform.SetPositionAndRotation(transform.position, transform.rotation);

			// ������Ʈ Ǯ ���̺귯��(LeanPool) Ȱ��
			LeanPool.Spawn(projectilePrefab, transform.position, transform.rotation);

		proj.damage = damage;
		proj.moveSpeed = projectileSpeed;
		proj.transform.localScale *= projectileScale;
		proj.pierceCount = pierceCount;

		LeanPool.Despawn(proj, proj.duration);
	}
}
