using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ��ü ������ �Ѱ��ϴ� ������Ʈ
public class GameManager : MonoBehaviour
{
	// ���� ��ü�� �ϳ��� �����ؾ� �Ѵ�

	private void Start()
	{
		MyClass myClass = MyClass.GetMyClass();		// �⺻ �����ڰ� private�̹Ƿ� GetMyClass�θ� �ν��Ͻ��� ���� ����

		// ���� myClass�� �ʿ�������� null�� �����ϴ� �� ������ ������ GC�� ���� ��ü ����
	}
}

// �⺻���� ��ü������ ���� �̱��� ��ü�� ����� ���
public class MyClass
{
	private static MyClass nonCollectableMyClass;   // ������ ������ �ȵǴ� myclass �ν��Ͻ��� ����
	private MyClass() { }
	public int processCount;		// ��������(non-static). ����å�� ��Ģ

	public static MyClass GetMyClass()
	{
		if(nonCollectableMyClass == null)   // GetMyClass()�� ���� ȣ��Ǿ��� ��쿡�� true
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