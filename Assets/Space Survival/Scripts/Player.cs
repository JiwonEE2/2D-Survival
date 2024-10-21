using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float hp = 100f;        // ü��
	public float damage = 5f;      // ���ݷ�
	public float moveSpeed = 5f;   // �̵��ӵ�

	public Projectile projectilePrefab;   // ����ü ������

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		Vector2 dir = new Vector2(x, y);

		Move(dir);

		if (Input.GetButtonDown("Fire1"))
		{
			// Vector3 -> Vector2�� ĳ������ �� z���� ����
			Vector2 mousePos = Input.mousePosition;
			Vector2 mouseScreenPos = Camera.main.ScreenToWorldPoint(mousePos);
			Vector2 fireDir = mouseScreenPos - (Vector2)transform.position;
			Fire(fireDir);
		}
	}

	/// <summary>
	/// Transform�� ���� ���� ������Ʈ�� �����̴� �Լ�
	/// </summary>
	/// <param name="dir"></param>
	public void Move(Vector2 dir)
	{
		transform.Translate(dir * moveSpeed * Time.deltaTime);
	}

	/// <summary>
	/// ����ü�� �߻�
	/// </summary>
	public void Fire(Vector2 dir)
	{
		Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

		projectile.transform.up = dir;
		projectile.damage = damage;
	}
}
