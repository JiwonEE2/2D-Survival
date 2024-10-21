using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ü
public class Projectile : MonoBehaviour
{
	public float damage = 100f;     // ������
	public float moveSpeed = 5f;   // �̵��ӵ�
	public float duration = 3f;    // ���ӽð�

	private void Start()
	{
		// gameObject ��� this�� ����ϸ� ������Ʈ�� �ı��ȴ�
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

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.TryGetComponent<Enemy>(out Enemy enemy))
		{
			enemy.TakeDamage(damage);
			Destroy(gameObject);
		}
	}
}
