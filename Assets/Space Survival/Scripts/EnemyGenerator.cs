using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{
	public Enemy enemyPrefab;
	public float duration = 10f;
	public float randomCircleSize = 10f;

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(GenerateEnemy(duration));
	}

	// Update is called once per frame
	void Update()
	{

	}

	private IEnumerator GenerateEnemy(float duration)
	{
		while(true)
		{
			yield return new WaitForSeconds(duration);
			Vector2 randomPosition = Random.insideUnitCircle.normalized * randomCircleSize;
			Enemy enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
		}
	}
}
