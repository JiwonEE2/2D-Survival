using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
	// 1. 적이 한 번 스폰될 때 1마리가 아닌 2~10마리 스폰하도록 변경 0
	// 2. 적 스폰 위치를 Vector2.zero가 아닌, 플레이어 기준 특정 거리 이상 위치에 스폰하기
	[Tooltip("한번에 스폰될 적의 수\nx:최소, y:최대")]
	public Vector2Int minMaxCount;
	[Tooltip("스폰될 때 플레이어로부터의 최대/최소 거리\nx:최소, y:최대")]
	public Vector2 minMaxDist;

	public GameObject enemyPrefab;      // 적 프리팹
	public float spawnInterval;         // 생성 간격

	public EnemyDataSO[] enemyDatas;

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
			Spawn(enemyCount);    // 몬스터 스폰
		}
	}

	private void Spawn(int count)
	{
		for (int i = 0; i < count; i++)
		{
			Vector2 playerPos = GameManager.Instance.player.transform.position;
			// Random.insideUnitCircle : 길이가 1인 원 안에서 랜덤 위치의 좌표를 반환
			Vector2 spawnPos;
			//int loopCount = 0;
			//float circleSize;
			//// 1. 일단 랜덤 좌표를 받은 후, 거리가 3 이내면 다시 랜덤을 받음
			//circleSize = Random.Range(minMaxDist.x, minMaxDist.y);
			//// 플레이어로부터의 거리가 0~minMaxDist.y 사이의 좌표를 받음
			//spawnPos = Random.insideUnitCircle * circleSize;

			//// 한 번에 스폰하는 경우도 있지만 조건이 맞지 않는 좌표가 나와서 루프 도는 빈도가 높아 보인다.
			//print($"Random Loop Count : {loopCount}");

			// 랜덤으로 나온 좌표를 무조건 조건에 맞게 가공하도록 연산해서 활용해보기

			// 랜덤 좌표를 하나 찍는다.
			Vector2 ranPos = Random.insideUnitCircle;
			// 찍힌 랜덤좌표 방향의 길이가 1인 벡터를 구한다.
			Vector2 normalizedPos = ranPos.normalized;
			// 중심 기준으로 ranPos가 확장할 범위를 구한다
			float moveRad = minMaxCount.y - minMaxDist.x; // 5-3=2

			// 범위만큼 움직인 좌표를 구한다.
			Vector2 notSpawnAreaVector = normalizedPos * minMaxDist.x;
			// minDist안에는 좌표가 찍히면 안되므로 찍힌 좌표를 해당 방향으로 minDist만큼 움직일 벡터를 구한다.
			Vector2 movedPos = ranPos * moveRad;
			// 움직인 좌표에 minDist만큼 움직인 벡터를 더한다.
			spawnPos = movedPos + notSpawnAreaVector;
			//spawnPos = (ranPos * (minMaxDist.y - minMaxDist.x)) + (ranPos.normalized * minMaxDist.x);
			//print($"spawnPos : {spawnPos}");

			Instantiate(enemyPrefab, playerPos + spawnPos, Quaternion.identity);
		}
	}
}
