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

	public void TakeHeal(float heal)
	{
		hp += heal;
		if (hp > maxHp)
		{
			hp = maxHp;
		}
	}

	public void TakeDamage(float damage)
	{
		//print($"아야! : {damage}");
		if (damage < 0)
		{
			//TakeHeal(-damage);	// 대신 힐 하도록 처리
			damage = 0;
		}
		hp -= damage;
		if (hp <= 0)
		{
			// 게임 오버 처리

		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 아이템이랑 상호작용 할 건데... 아이템이 bomb도 있고 heal도 있어서 전부 각 객체 별로 행동을 정의했더니 소스코드도 길어지고..
		//if (collision.TryGetComponent<Bomb>(out Bomb bomb))   // 만약 상호작용한 트리거에 Bomb 컴포넌트가 있을 경우
		//{
		//	bomb.Contact();
		//}
		//if(collision.TryGetComponent<Heal>(out Heal heal))
		//{
		//	heal.Contact();
		//}

		// 이럴 때 개발자가 "다형성"을 구현하여
		// 소스코드를 효율적으로 작성할 수 있는 방법 3가지
		// 1. 부모 클래스를 상속
		// 2. 인터페이스를 구현
		// 3. 유니티의 SendMessage 사용

		// 1. 부모 클래스를 상속했을 경우
		//if(collision.TryGetComponent<Item>(out Item item))
		//{
		//	item.Contact();
		//	// 부딪힌 객체가 정확히 어떤 타입일 지는 모르겠으나 Item이라는 클래스를 상속한 것은 확실하고 그렇다면 Contact() 함수를 가지고 있으므로 호출할 수 있다.
		//}

		// 2. 만약 특정 클래스를 상속하지 않고, 공통점이 없는 여러 객체들이 경우에 따라 같은 행동을 해야할 경우, Interface를 사용할 수 있다.
		//if (collision.TryGetComponent<IContactable>(out var contact))
		//{
		//	contact.Contact();
		//	// 부딪힌 객체가 Enemy인지 Item인지조차 모르겠으나 어쨌든 IContactable 인터페이스를 구현했다면 Contact() 함수를 가지고 있으므로 호출할 수 있다.
		//}

		// 3. 게임오브젝트는 모두 SendMessage를 통해 가지고 있는 컴포넌트의 특정 이름을 가진 함수를 호출하도록 기능을 지원. UnityEngine의 내장 기능
		collision.SendMessage("Contact", /*this, */SendMessageOptions.DontRequireReceiver);
		// 문자열 참조이기 때문에 이름 변경 시 오류가 생길 수 있으며, 현재 에너미의 Contact와 item의 Contact는 연관이 없기 때문에 또 item의 Contact의 이름을 바꾸더라도 enemy에서는 안바뀐다.

		// SendMessage 주의점
		// 1. 문자열로 함수를 호출하므로 함수 이름 변경 또는 오타 발생 시 에러 찾기 힘들다.
		// 2. 해당 객체에 있는 모든 컴포넌트들이 Contact라는 함수를 가지고 있는 지 탐색을 수행하기 때문에 퍼포먼스가 효율적이라고 보기 힘들다.
		// 3. 호출할 함수의 파라미터는 0개 또는 1개로 제한됨.
		// 빠른 개발과 프로토타이핑에서 사용하기는 좋으나, 구조적으로 좋은 방식은 아니므로 팀원이 많은 개발팀이나 일정 규모 이상의 기업에서는 쓰지 않는 편
		// 마지막 파라미터는 null 일때 무시할 건지 말건지 설정할 수 있다. 그래서 이렇게 최대 3개의 파라미터를 가질 수 있다.
	}
}
