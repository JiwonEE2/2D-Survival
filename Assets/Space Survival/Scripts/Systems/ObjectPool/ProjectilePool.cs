using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
	public static ProjectilePool pool;
	public Projectile projPrefab;

	private void Awake()
	{
		pool = this;
	}

	List<Projectile> poolList = new();  // 비활성화된 객체 리스트

	// Public 함수는 딱 2개
	/// <summary>
	/// 투사체 꺼내오기
	/// </summary>
	/// <returns>꺼내온 투사체</returns>
	public Projectile Pop()
	{
		if (poolList.Count <= 0)  // 꺼낼 객체 없음
		{
			Push(Instantiate(projPrefab));
		}
		Projectile proj = poolList[0];
		poolList.Remove(proj);
		proj.gameObject.SetActive(true);
		proj.transform.SetParent(null);
		return proj;
	}

	/// <summary>
	/// 다 쓴 투사체 도로 집어넣기
	/// </summary>
	/// <param name="proj">다 쓴 투사체</param>
	public void Push(Projectile proj)
	{
		poolList.Add(proj);
		proj.gameObject.SetActive(false);
		proj.transform.SetParent(transform, false);
	}

	/// <summary>
	/// 다 쓴 투사체 잠시 후에 집어넣기
	/// </summary>
	/// <param name="proj">다 쓴 투사체</param>
	/// <param name="delay">지연 시간</param>
	public void Push(Projectile proj, float delay)
	{
		StartCoroutine(PushCoroutine(proj, delay));
	}

	IEnumerator PushCoroutine(Projectile proj, float delay)
	{
		yield return new WaitForSeconds(delay);
		Push(proj);
	}
}
