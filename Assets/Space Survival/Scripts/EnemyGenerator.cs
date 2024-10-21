using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
	public Enemy enemyPrefab;
	public float duration = 10f;
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
		for (int i = 0; i < 10; i++)
		{
			yield return new WaitForSeconds(duration);
			Enemy enemy = Instantiate(enemyPrefab, new Vector2(0, 10), Quaternion.identity);

		}

	}
}
