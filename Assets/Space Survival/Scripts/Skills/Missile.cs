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
	public float projectileSpeed;       // 투사체 속도
	public float projectileScale;       // 투사체 크기
	public float shotInterval;          // 공격 간격
}
