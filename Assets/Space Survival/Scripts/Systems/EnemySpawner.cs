using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
	// 1. ���� �� �� ������ �� 1������ �ƴ� 2~10���� �����ϵ��� ���� 0
	// 2. �� ���� ��ġ�� Vector2.zero�� �ƴ�, �÷��̾� ���� Ư�� �Ÿ� �̻� ��ġ�� �����ϱ�
	[Tooltip("�ѹ��� ������ ���� ��\nx:�ּ�, y:�ִ�")]
	public Vector2Int minMaxCount;

	public GameObject enemyPrefab;      // �� ������
	public float spawnInterval;         // ���� ����

	private void Start()
	{
		StartCoroutine(SpawnCoroutine());
	}

	private IEnumerator SpawnCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(spawnInterval);
			int enemyCount = Random.Range(minMaxCount.x, minMaxCount.y);
			Spawn(enemyCount);    // ���� ����
		}
	}

	private void Spawn(int count)
	{
		for (int i = 0; i < count; i++)
		{
			Vector2 playerPos = GameManager.Instance.player.transform.position;
			Instantiate(enemyPrefab);
		}
	}
}
