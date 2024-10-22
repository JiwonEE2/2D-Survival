using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 전체 진행을 총괄하는 오브젝트
public class GameManager : MonoBehaviour
{
	// 게임 전체에 하나만 존재해야 한다

	private void Start()
	{
		MyClass myClass = MyClass.GetMyClass();		// 기본 생성자가 private이므로 GetMyClass로만 인스턴스에 접근 가능

		// 만약 myClass가 필요없어져서 null을 대입하는 등 참조를 잃으면 GC에 의해 객체 증발
	}
}

// 기본적인 객체지향형 언어에서 싱글톤 객체를 만드는 방법
public class MyClass
{
	private static MyClass nonCollectableMyClass;   // 참조를 잃으면 안되는 myclass 인스턴스를 저장
	private MyClass() { }
	public int processCount;		// 전역변수(non-static). 단일책임 원칙

	public static MyClass GetMyClass()
	{
		if(nonCollectableMyClass == null)   // GetMyClass()가 최초 호출되었을 경우에만 true
		{
			nonCollectableMyClass = new MyClass();
			return nonCollectableMyClass;
		}
		else
		{
			return nonCollectableMyClass;
		}
	}
}