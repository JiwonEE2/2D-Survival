using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
	public Transform target;
	// 공격할 대상 위치
	// 게임 씬에 있는 타겟 대신, 랜덤 위치에 생성할 예정

	public MissileProjectile projectilePrefab;

	public float damage;
	public float projectileSpeed; // 투사체 속도
	public float projectileScale; // 투사체 크기
	public float shotInterval;    // 공격 간격

	public float maxDist;         // 최대 타겟 거리

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
