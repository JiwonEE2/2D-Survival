using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	private float maxHp;
	public float hp = 100f; //체력
	public float damage = 5f; //공격력
	public float moveSpeed = 5f; //이동속도

	public Projectile projectilePrefab; //투사체 프리팹

	public float HpAmount { get => hp / maxHp; }    // 현재 체력 비율

	public int killCount = 0;
	public Text killCountText;
	public Image hpBarImage;

	private Transform moveDir;
	private Transform fireDir;

	private void Awake()
	{
		moveDir = transform.Find("MoveDir");
		fireDir = transform.Find("FireDir");
	}

	void Start()
	{
		maxHp = hp;   // 최대 체력 지정
		GameManager.Instance.player = this;
	}

	void Update()
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		Vector2 moveDir = new Vector2(x, y);

		// 마우스 위치로 사격 방향을 향해야 할 때
		//Vector2 mousePos = Input.mousePosition;
		//Vector2 mouseScreenPos = Camera.main.ScreenToWorldPoint(mousePos);
		//Vector2 fireDir = mouseScreenPos - (Vector2)transform.position;
		//Vector3 -> Vector2로 캐스팅 할 때 : z값이 생략

		// 가장 가까운 적을 탐색하여 사격 방향을 정할 때
		Enemy targetEnemy = null;   // 대상으로 지정된 적
		float targetDistance = float.MaxValue;    // 대상과의 거리
		foreach (Enemy enemy in GameManager.Instance.enemies)
		{
			float distance = Vector3.Distance(enemy.transform.position, transform.position);
			if (distance < targetDistance)    // 이전에 비교한 적보다 가까우면
			{
				targetDistance = distance;
				targetEnemy = enemy;
			}

		}
		Vector2 fireDir = Vector2.zero;
		if (targetEnemy != null)
		{
			fireDir = targetEnemy.transform.position - transform.position;
		}

		Move(moveDir);

		if (Input.GetButtonDown("Fire1"))
		{
			Fire(fireDir);
		}

		killCountText.text = killCount.ToString();
		hpBarImage.fillAmount = HpAmount;

		// transform.up/right/forward에 방향 벡터를 대입할 때는 방향 벡터의 magnitude를 굳이 1로 제한하지 않아도 된다
		this.moveDir.up = moveDir;
		this.fireDir.up = fireDir;

		//print(this.moveDir.up);		// normalized되어 magnitude가 1로 고정된 방향 벡터가 반환됨
	}

	/// <summary>
	/// Transform을 통해 게임 오브젝트를 움직이는 함수.
	/// </summary>
	/// <param name="dir">이동 방향</param>
	public void Move(Vector2 dir)
	{
		transform.Translate(dir * moveSpeed * Time.deltaTime);
	}

	/// <summary>
	/// 투사체를 발사.
	/// </summary>
	public void Fire(Vector2 dir)
	{
		Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

		projectile.transform.up = dir;
		projectile.damage = damage;
	}

	public void TakeDamage(float damage)
	{
		print($"아야! : {damage}");
		hp -= damage;
		if (hp <= 0)
		{
			// 게임 오버 처리

		}
	}
}
