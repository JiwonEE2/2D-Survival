using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProjectile : MonoBehaviour
{
	public float damage;    // 데미지
	public float moveSpeed; // 투사체 속도
	public float duration;

	public Vector3 rendererStartPos;  // 폭탄이 생성되었을 때, 스프라이트 렌더러가 있을 위치

	CircleCollider2D coll;
	Transform rendererTransform;

	private void Awake()
	{
		coll = GetComponent<CircleCollider2D>();
		coll.enabled = false;
		rendererTransform = transform.Find("Renderer");
	}

	private void Start()
	{
		StartCoroutine(Explosion());
	}

	// 생성된 위치에서 일정 시간 후에 범위 내의 적에게 데미지를 주고 사라짐
	IEnumerator Explosion()
	{
		float startTime = Time.time;
		float endTime = startTime + duration;
		rendererTransform.localPosition = rendererStartPos;

		// while문 전체가 WaitnewSecond역할을 한다
		while (Time.time < endTime)
		{
			yield return null;  // 프레임마다 1회씩 반복
			float currentTime = Time.time - startTime; // 이 오브젝트가 생성된 이후 지속된 시간
			float duration = this.duration;
			float t = currentTime / duration;
			Vector2 curRendPos = Vector2.Lerp(rendererStartPos, Vector2.zero, t);
			rendererTransform.localPosition = curRendPos;
		}

		// 폭발
		// overlap 뭐였지 전혀 기억이 안난다..
		Collider2D[] contactedColls = Physics2D.OverlapCircleAll(transform.position, coll.radius);
		foreach (Collider2D contactedColl in contactedColls)
		{
			if (contactedColl.CompareTag("Enemy"))
			{
				print($"Exploded Collider : {contactedColl.name}");
			}
		}
		Destroy(gameObject);
	}
}
