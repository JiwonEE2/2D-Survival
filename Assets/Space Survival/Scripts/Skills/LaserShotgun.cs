using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShotgun : LaserGun
{
	public Transform[] shotPoints;

	protected override IEnumerator FireCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(shotInterval);
			Fire();
		}
	}

	protected override void Fire()
	{
		foreach (Transform shotPoint in shotPoints)
		{
			Projectile proj = //Instantiate(projectilePrefab, transform.position, Quaternion.identity);
												//projPool.Pop();
												//proj.transform.position = transform.position;
			LeanPool.Spawn(projectilePrefab, transform.position, Quaternion.identity);

			proj.damage = damage;
			proj.moveSpeed = projectileSpeed;
			proj.transform.localScale *= projectileScale;

			// ����ü�� tranform.up �������� �����ϹǷ� up������ �߻� �������� ���ϵ��� transform.up�� ���� ���͸� ����
			proj.transform.up = shotPoint.position - transform.position;
			proj.pierceCount = pierceCount; // ���� Ƚ���� ��ų�� �Էµ� ��ġ�� ����

			LeanPool.Despawn(proj, proj.duration);
		}
	}
}
