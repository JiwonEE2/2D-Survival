using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	//public float maxHp = 10f;		// �ϼ�
	private float maxHp;
	public float hp = 10f;        // ü��
	public float damage = 10f;    // ���ݷ�
	public float moveSpeed = 3f;  // �̵��ӵ�

	// �ʰ��
	public float hpAmount { get { return hp / maxHp; } }    // ���� ���Ǵ� �׸��� ������Ƽ�� �����

	// Getter/Setter

	public Transform target;      // ������ ���
	public Image hpBar;

	private void Start()
	{
		target = GameObject.Find("Player").transform;
		maxHp = hp;
	}

	private void Update()
	{
		Vector2 moveDir = target.position - transform.position;
		Move(moveDir.normalized);
		//print(moveDir.magnitude);   // vector.magnitude : �ش� ���Ͱ� "���⺤��"�� ���ֵ� �� ������ ����
		//print(moveDir.normalized);	// ������ ������ ä ���̰� 1�� ������ ����

		hpBar.fillAmount = hpAmount;
	}

	private void Move(Vector2 dir)    // dir ���� Ŀ���� 1�� ������ �ϰ� ���� ���
	{
		transform.Translate(dir * moveSpeed * Time.deltaTime);
	}

	// OnHit, ...
	public void TakeDamage(float damage)
	{
		hp -= damage;
		if (hp <= 0)    // ���
		{
			Destroy(gameObject);
		}
	}
}
