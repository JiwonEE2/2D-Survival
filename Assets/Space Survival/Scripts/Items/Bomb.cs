using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Item
{
	// 특정 메시지 함수가 없는 Component는 Enable/Disable이 동작하지 않음.
	//private void Start() { }
	//private void Update() { }
	//private void Awake() { }
	// enabled 여부에 관계없이 호출되는 메시지 함수
	public override void Contact()
	{
		print("폭탄 습득");
		// 게임 매니저에게 모든 적들을 없애달라고 부탁하자
		GameManager.Instance.RemoveAllEnemies();
		base.Contact();   // Destroy 대신 부모의 Contact 호출
	}
}
