using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTest : MonoBehaviour
{
	public SpriteRenderer sp1;
	public SpriteRenderer sp2;

	// Resources ���� : ������Ʈ�� Assets ���� ���� Resources��� �̸��� ������ ������ ���
	// �ش� ���� ���� ���� �� ����Ƽ ���ҽ��� Ȱ�� ������(ex:Sprite, Texture, Mesh, Prefab ��) ������ �̸� �޸𸮿� �ε��Ͽ� ��Ÿ�ӿ��� ������ �� �ֵ��� �ϴ� ����
	// ���� : �̸� ������ �ش� ���ҽ��� ���ε����� �ʾƵ� ��Ÿ�ӿ��� �ε尡 ����
	// ���� : ��쿡 ���� ���ʿ��� ���ҽ��� �޸𸮸� �����ϰ� ���� �� ������, �����ڰ� ���� �����ϱ� �����
	// ������ ���ϵ��� �ε��Ͽ� Ȱ���� �� �����Ƿ�, ���� ������Ʈ�� ������Ÿ���ο� �ַ� ���̸�, ���̺� ���� ���ӿ����� ����� �����ϴ� ��
	// ��ü ���� �ý���
	// 1. Asset Bundle -> ���� ����� ���ӿ� ���� ����
	// 2. Addressable Assets -> �ֽ� ���ҽ� ���� �ý���. ���۷����� ���� �ʰ� �Ű� ��� �� ������ ���Ƽ� ���� �������� ���� �ʴ�.

	// Resources ���� �����ϴ� ��� : ��� �����ϰ� Assets���� ���� ������ �� ������ ���� ������ "/"�� �����Ͽ� ��θ� ����
	// Assets/Textures/Resources/PlayerSprites/player1.png
	// Resource.Load("PlayerSprites/player1");

	public Texture texture;
	public Mesh mesh;

	private void Start()
	{
		Sprite sprite1 = Resources.Load<Sprite>("sprite1");
		Sprite sprite2 = Resources.Load<Sprite>("sprite2");

		texture = Resources.Load<Texture>("sprite1");
		mesh = Resources.Load<Mesh>("sprite1");

		Enemy enemyResource = Resources.Load<Enemy>("Prefabs/EnemyResource");

		sp1.sprite = sprite1;
		sp2.sprite = sprite2;

		Instantiate(enemyResource);
	}
}
