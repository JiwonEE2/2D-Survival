using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public PlayerDataSO playerData;

	public int level = 0;   // 레벨
	public int exp = 0;     // 경험치

	// 현업에서 개발되는 대부분의 게임은 exp값을 빼지 않음
	// 계속 exp를 누적하는 대신에 현재 exp를 레벨로 환산하면 몇 레벨에 해당하는 지 계산

	private int[] levelupSteps = { 100, 200, 300, 400 };  // 최대 레벨 5까지의 경험치 단계
	private int currentMaxExp;  // 현재 레벨에서 레벨업 하기까지 필요한 경험치 량

	private float maxHp;
	public float hp = 100f; //체력
	public float damage = 5f; //공격력
	public float moveSpeed = 5f; //이동속도

	//public Projectile projectilePrefab; //투사체 프리팹

	public float HpAmount { get => hp / maxHp; }    // 현재 체력 비율

	public int killCount = 0;
	public int totalKillCount = 0;  // 이전 게임에서의 누적 카운트

	//public Text killCountText;
	//public Image hpBarImage;
	//public Text levelText;
	//public Text expText;

	private Transform moveDir;
	private Transform fireDir;

	private Rigidbody2D rb;

	public new SpriteRenderer renderer;

	public Animator anim;

	// 플레이어가 Fire 기능을 사용하는 대신 Skill들을 관리하여 공격 기능을 수행하도록
	public List<Skill> skills;

	private void Awake()
	{
		moveDir = transform.Find("MoveDir");
		fireDir = transform.Find("FireDir");
		rb = GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		hp = playerData.hp;
		damage = playerData.damage;
		moveSpeed = playerData.moveSpeed;
		name = playerData.characterName;
		renderer.sprite = playerData.sprite;
		//Instantiate(playerData.startSkillPrefab, fireDir, true);
		// GameObject 활성화/비활성화 : SetActive(bool);
		// Component 활성화/비활성화 : enabled = bool;
		// 두 경우 모두 OnEnabled/OnDisabled 메시지 함수가 호출
		renderer.GetComponent<Rotater>().enabled = playerData.rotateRenderer;

		maxHp = hp;   // 최대 체력 지정
		currentMaxExp = levelupSteps[0];  // 최대 경험치

		UIManager.Instance.levelText.text = (level + 1).ToString();
		UIManager.Instance.expText.text = exp.ToString();
		GameManager.Instance.player = this;

		// 리턴이 있는 함수를 호출할 때, 리턴을 사용하지 않는다면
		// 아예 반환을 위한 메모리를 점유하지 않고 함수만 호출
		//_ = StartCoroutine(FireCoroutine());

		foreach (Skill skill in skills)
		{
			GameObject skillObj = Instantiate(skill.skillPrefabs[skill.skillLevel], transform, false); // 스킬 오브젝트 생성
			skillObj.name = skill.skillName;                                        // 오브젝트 이름 변경
			skillObj.transform.localPosition = Vector2.zero;                        // 스킬 위치를 플레이어의 위치로 가져옴
			if (skill.isTargetting)
			{
				skillObj.transform.SetParent(fireDir);  // 항상 적을 향하는 오브젝트 자식으로 만듦
			}
			skill.currentSkillObject = skillObj;
		}
	}

	void Update()
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		Vector2 moveDir = new Vector2(x, y);

		//this.moveDir.gameObject.SetActive(moveDir != Vector2.zero);

		anim.SetBool("IsMoving", moveDir.magnitude > 0.1f);

		// 마우스 위치로 사격 방향을 향해야 할 때
		//Vector2 mousePos = Input.mousePosition;
		//Vector2 mouseScreenPos = Camera.main.ScreenToWorldPoint(mousePos);
		//Vector2 fireDir = mouseScreenPos - (Vector2)transform.position;
		//Vector3 -> Vector2로 캐스팅 할 때 : z값이 생략

		// 가장 가까운 적을 탐색하여 사격 방향을 정할 때
		Enemy targetEnemy = null;   // 대상으로 지정된 적
		float targetDistance = float.MaxValue;    // 대상과의 거리

		//if (GameManager.Instance.enemies.Count == 0)
		//{
		//	// 발사 절차를 생략
		//	isFiring = false;
		//}
		//else
		//{
		//	isFiring = true;
		//}

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

		// 마우스 좌클릭 또는 왼쪽 ctrl 키로 발사
		//if (Input.GetButtonDown("Fire1"))
		//{
		//	Fire(fireDir);
		//}

		UIManager.Instance.killCountText.text = killCount.ToString();
		UIManager.Instance.totalKillCountText.text = totalKillCount.ToString();
		UIManager.Instance.hpBarImage.fillAmount = HpAmount;

		// tail 원상태로 돌아간 뒤에 사라지는 문제 해결을 위함 
		if (moveDir.magnitude > 0.1f)
		{
			// transform.up/right/forward에 방향 벡터를 대입할 때는 방향 벡터의 magnitude를 굳이 1로 제한하지 않아도 된다
			this.moveDir.up = moveDir;
		}
		this.fireDir.up = fireDir;

		//print(this.moveDir.up);		// normalized되어 magnitude가 1로 고정된 방향 벡터가 반환됨
	}

	/// <summary>
	/// Transform을 통해 게임 오브젝트를 움직이는 함수.
	/// </summary>
	/// <param name="dir">이동 방향</param>
	public void Move(Vector2 dir)
	{
		//transform.Translate(dir * moveSpeed * Time.deltaTime);
		Vector2 movePos = rb.position + (dir * moveSpeed * Time.fixedDeltaTime);
		rb.MovePosition(movePos);
	}

	/// <summary>
	/// 투사체를 발사.
	/// </summary>
	//public void Fire()
	//{
	//	Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

	//	projectile.transform.up = fireDir.up;
	//	projectile.damage = damage;
	//}

	//public float fireInterval;
	//public bool isFiring;

	// 자동으로 투사체 발사 코루틴
	//private IEnumerator FireCoroutine()
	//{
	//	while (true)
	//	{
	//		yield return new WaitForSeconds(fireInterval);
	//		if (isFiring)
	//		{
	//			Fire();
	//		}
	//	}
	//}

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
		anim.SetTrigger("Hit");
		if (damage < 0)
		{
			//TakeHeal(-damage);	// 대신 힐 하도록 처리
			damage = 0;
		}
		hp -= damage;
		if (hp <= 0)
		{
			// 게임 오버 처리
			hp = 0;
			//TODO:게임오버처리
			GameManager.Instance.GameOver();
		}
	}

	// 경험치 습득마다 호출
	public void GainExp(int exp)
	{
		this.exp += exp;    // 습득 경험치 더함
		if (level < levelupSteps.Length && this.exp >= currentMaxExp)  // 경험치 습득 후 레벨업을 위한 경험치에 도달하면
		{
			// 레벨업
			OnLevelUp();
			// 레벨업하면 레벨업 이펙트, UI, 얻게된 스킬도
			//DoLevelUp();
		}

		UIManager.Instance.levelText.text = (level + 1).ToString();
		UIManager.Instance.expText.text = this.exp.ToString();
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

	public void OnLevelUp()
	{
		level++;
		exp -= currentMaxExp;
		if (level < levelupSteps.Length)  // 아직 최대 레벨에 도달하지 않아서 레벨업이 되었을 때
		{
			currentMaxExp = levelupSteps[level];

			UIManager.Instance.levelupPanel.LevelUpPanelOpen(skills, OnSkillLevelUp);

			//int skillNum = Random.Range(0, skills.Count);
			//OnSkillLevelUp(skills[skillNum]); // (임시) 레벨업하면 랜덤 스킬 1개를 레벨업
		}
	}

	// 파라미터로 넘어온 스킬의 레벨을 상승시키고, 다음 레벨의 프리팹으로 교체
	public void OnSkillLevelUp(Skill skill)
	{
		if (skill.skillLevel >= skill.skillPrefabs.Length - 1)
		{
			// 유효하지 않은 스킬이 넘어왔다.
			Debug.LogWarning($"최대 레벨에 도달한 스킬 레벨 업을 시도함{skill.skillName}");
			return;
		}
		skill.skillLevel++;                 // 스킬 레벨 상승
		Destroy(skill.currentSkillObject);  // 기존에 있던 스킬 오브젝트를 제거
		skill.currentSkillObject = Instantiate(skill.skillPrefabs[skill.skillLevel], transform, false);
		skill.currentSkillObject.name = skill.skillPrefabs[skill.skillLevel].name;
		skill.currentSkillObject.transform.localPosition = Vector2.zero;
		if (skill.isTargetting)
		{
			skill.currentSkillObject.transform.SetParent(fireDir);
		}
	}
}
