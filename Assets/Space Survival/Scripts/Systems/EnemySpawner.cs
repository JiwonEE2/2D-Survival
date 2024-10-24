using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
	// 1. 적이 한 번 스폰될 때 1마리가 아닌 2~10마리 스폰하도록 변경
	// 2. 적 스폰 위치를 Vector2.zero가 아닌, 플레이어 기준 특정 거리 이상 위치에 스폰하기
	[Tooltip("한번에 스폰될 적의 수\nx:최소, y:최대")]
	public Vector2Int minMaxCount;

	public GameObject enemyPrefab;      // 적 프리팹
	public float spawnInterval;         // 생성 간격

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
			for (int i = 0; i < enemyCount; i++)
			{
				Spawn();    // 몬스터 스폰
			}
		}
	}

	private void Spawn()
	{
		Instantiate(enemyPrefab);
	}
}
