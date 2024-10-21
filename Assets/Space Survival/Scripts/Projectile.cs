using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 투사체
public class Projectile : MonoBehaviour
{
	public float damage = 10;     // 데미지
	public float moveSpeed = 5;   // 이동속도
	public float duration = 3;    // 지속시간

	private void Start()
	{
		// gameObject 대신 this를 사용하면 컴포넌트만 파괴된다
		Destroy(gameObject, duration);
	}

	private void Update()
	{
		Move(Vector2.up);
		//Physics2D.OverlapCircle();
	}

	public void Move(Vector2 dir)
	{
		transform.Translate(dir * moveSpeed * Time.deltaTime);
	}
}
