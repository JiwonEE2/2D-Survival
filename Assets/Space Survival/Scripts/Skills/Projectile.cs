using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//투사체
public class Projectile : MonoBehaviour
{
	public float damage = 10;   //데미지
	public float moveSpeed = 5; //이동속도
	public float duration = 3;  //지속시간

	public int pierceCount = 0; // 관통횟수
	private CircleCollider2D coll;

	private void Awake()
	{
		coll = GetComponent<CircleCollider2D>();
		coll.enabled = false;
	}

	private void OnEnable()
	{
		//Destroy(gameObject, duration); //3초 후에 오브젝트 제거
		ProjectilePool.pool.Push(this, duration);
	}

	List<Collider2D> contactedColls = new();  // OverlabCircle 함수를 통해 감지한 적이 있는 콜라이더를 담을 List

	private void Update()
	{
		Move(Vector2.up);
		Collider2D contactedColl = Physics2D.OverlapCircle(transform.position, coll.radius);
		if (contactedColl != null)
		{
			if (contactedColl.CompareTag("Enemy"))
			{
				if (false == contactedColls.Contains(contactedColl))
				{
					// 유효한 타격이 발생
					print($"Contacted Collider : {contactedColl.name}");
					contactedColls.Add(contactedColl);

					pierceCount--;
					if (pierceCount == 0)
					{
						// 관통 횟수가 모두 소모되면 Destroy
						//Destroy(gameObject);
						ProjectilePool.pool.Push(this);
					}
				}
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, coll.radius);
	}

	public void Move(Vector2 dir)
	{
		transform.Translate(dir * moveSpeed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent<Enemy>(out Enemy enemy))
		{
			enemy.TakeDamage(damage);
			//Destroy(gameObject);
			ProjectilePool.pool.Push(this);
		}
	}

	private void OnDisable()
	{
		contactedColls.Clear();
	}
}
