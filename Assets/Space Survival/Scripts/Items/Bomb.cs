using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Item
{
	// Ư�� �޽��� �Լ��� ���� Component�� Enable/Disable�� �������� ����.
	//private void Start() { }
	//private void Update() { }
	//private void Awake() { }
	// enabled ���ο� ������� ȣ��Ǵ� �޽��� �Լ�
	public override void Contact()
	{
		print("��ź ����");
		// ���� �Ŵ������� ��� ������ ���ִ޶�� ��Ź����
		GameManager.Instance.RemoveAllEnemies();
		base.Contact();   // Destroy ��� �θ��� Contact ȣ��
	}
}
