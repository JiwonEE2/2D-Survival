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
			Projectile proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
			proj.damage = damage;
			proj.moveSpeed = projectileSpeed;
			proj.transform.localScale *= projectileScale;
		}
	}
}
