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
	[Tooltip("������ �� �÷��̾�κ����� �ִ�/�ּ� �Ÿ�\nx:�ּ�, y:�ִ�")]
	public Vector2 minMaxDist;

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
			// Random.insideUnitCircle : ���̰� 1�� �� �ȿ��� ���� ��ġ�� ��ǥ�� ��ȯ
			Vector2 spawnPos;
			int loopCount = 0;
			float circleSize;
			// 1. �ϴ� ���� ��ǥ�� ���� ��, �Ÿ��� 3 �̳��� �ٽ� ������ ����
			circleSize = Random.Range(minMaxDist.x, minMaxDist.y);
			// �÷��̾�κ����� �Ÿ��� 0~minMaxDist.y ������ ��ǥ�� ����
			spawnPos = Random.insideUnitCircle * circleSize;

			// �� ���� �����ϴ� ��쵵 ������ ������ ���� �ʴ� ��ǥ�� ���ͼ� ���� ���� �󵵰� ���� ���δ�.
			print($"Random Loop Count : {loopCount}");

			// �������� ���� ��ǥ�� ������ ���ǿ� �°� �����ϵ��� �����ؼ� Ȱ���غ���
			Instantiate(enemyPrefab, playerPos + spawnPos, Quaternion.identity);
		}
	}
}
