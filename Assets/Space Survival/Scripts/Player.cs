using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float hp = 100f;        // 체력
	public float damage = 5f;      // 공격력
	public float moveSpeed = 5f;   // 이동속도

	public Projectile projectilePrefab;   // 투사체 프리팹

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
			// Vector3 -> Vector2로 캐스팅할 때 z값이 생략
			Vector2 mousePos = Input.mousePosition;
			Vector2 mouseScreenPos = Camera.main.ScreenToWorldPoint(mousePos);
			Vector2 fireDir = mouseScreenPos - (Vector2)transform.position;
			Fire(fireDir);
		}
	}

	/// <summary>
	/// Transform을 통해 게임 오브젝트를 움직이는 함수
	/// </summary>
	/// <param name="dir"></param>
	public void Move(Vector2 dir)
	{
		transform.Translate(dir * moveSpeed * Time.deltaTime);
	}

	/// <summary>
	/// 투사체를 발사
	/// </summary>
	public void Fire(Vector2 dir)
	{
		Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

		projectile.transform.up = dir;
		projectile.damage = damage;
	}
}
