using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
	// Public 함수는 딱 2개
	/// <summary>
	/// 투사체 꺼내오기
	/// </summary>
	/// <returns>꺼내온 투사체</returns>
	public Projectile Pop()
	{
		return null;
	}

	/// <summary>
	/// 다 쓴 투사체 도로 집어넣기
	/// </summary>
	/// <param name="proj">다 쓴 투사체</param>
	public void Push(Projectile proj)
	{

	}

	/// <summary>
	/// 다 쓴 투사체 잠시 후에 집어넣기
	/// </summary>
	/// <param name="proj">다 쓴 투사체</param>
	/// <param name="delay">지연 시간</param>
	public void Push(Projectile proj, float delay)
	{

	}
}
