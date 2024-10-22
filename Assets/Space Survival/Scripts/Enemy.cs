using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	//public float maxHp = 10f;//하수
	private float maxHp;
	public float hp = 10f; //체력
	public float damage = 10f; //공격력
	public float moveSpeed = 3f; //이동 속도

	//초고수
	public float hpAmount { get { return hp / maxHp; } } //자주 계산되는 항목은 프로퍼티로 만들기

	//Getter/Setter

	private Transform target; //추적할 대상

	public Image hpBar;

	private void Start()
	{
		GameManager.Instance.enemies.Add(this);   // 적 리스트에 자기 자신을 추가
		target = GameManager.Instance.player.transform;
		maxHp = hp;
	}

	private void Update()
	{
		Vector2 moveDir = target.position - transform.position;
		Move(moveDir.normalized);
		//print(moveDir.magnitude);//vector.magnitude:해당 벡터가 "방향벡터"로 간주될 때, 벡터의 길이
		//print(moveDir.normalized);//방향을 유지한채 길이가 1로 고정된 벡터.
		hpBar.fillAmount = hpAmount;
	}

	public void Move(Vector2 dir)//dir 값이 커져도 1로 고정을 하고 싶을경우=>normalized
	{
		transform.Translate(dir * moveSpeed * Time.deltaTime);
	}

	//OnHit, ...
	public void TakeDamage(float damage)
	{
		hp -= damage;

		if (hp <= 0) //으앙 쥬금
		{
			Die();
		}
	}

	private void Die()
	{
		GameManager.Instance.enemies.Remove(this);
		GameManager.Instance.player.killCount++;
		Destroy(gameObject);
	}

	public float damageInterval;    // 데미지 간격
	private float preDamageTime;		// 이전에 데미지를 준 시간(Time.time)

	private void OnCollisionStay2D(Collision2D collision)
	{
		// 플레이어에게 데미지 주는 간격 조정하기
		if (collision.collider.CompareTag("Player"))
		{
			collision.collider.GetComponent<Player>().TakeDamage(damage);
		}
	}
}
