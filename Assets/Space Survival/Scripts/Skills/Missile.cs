using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
		// 랜덤 Vector2 포지션을 정해서 투사체를 생성
		Vector2 pos = Random.insideUnitCircle * maxDist;

		// 랜덤 위치에 빈 오브젝트 생성 후 transform을 가져오는 로직 작성
		//target.position = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
		MissileProjectile proj = Instantiate(projectilePrefab, pos, Quaternion.identity);
		proj.damage = damage;
		proj.duration = 1 / projectileSpeed;
	}
}
