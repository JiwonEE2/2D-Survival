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
				projPool.Pop();
			proj.transform.position = transform.position;

			proj.damage = damage;
			proj.moveSpeed = projectileSpeed;
			proj.transform.localScale *= projectileScale;

			// 투사체는 tranform.up 방향으로 진행하므로 up방향이 발사 방향으로 향하도록 transform.up에 방향 벡터를 대입
			proj.transform.up = shotPoint.position - transform.position;
			proj.pierceCount = pierceCount;
		}
	}
}
