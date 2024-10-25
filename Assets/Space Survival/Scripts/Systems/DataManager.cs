using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonManager<DataManager>
{
	// PlayerPrefs 클래스 : 디바이스에 저장된 게임 데이터를 불러오거나 게임 데이터를 저장하는 기능을 담당하는 클래스
	// 주로 정적 함수를 호출하여 기능 활용

	public bool clearPrefsOnStart;

	IEnumerator Start()
	{
		if (clearPrefsOnStart)
		{
			PlayerPrefs.DeleteAll();  // 모든 저장 데이터 삭제
		}
		yield return null;  // 한 프레임 쉬고
		OnLoad();
	}

	// Load
	public void OnLoad()
	{
		// 디바이스에 저장된 여러 데이터 중 같은 키에 해당하는 값을 가져옴 (키:string, 기본값:int)
		int totalKillCount = PlayerPrefs.GetInt("TotalKillCount", 0); // 저장데이터가 없어서 null 방지를 위해 기본값을 설정할 수 있는 오버로드된 함수가 있다.
		GameManager.Instance.player.totalKillCount = totalKillCount;
	}

	// Save
	public void OnSave()
	{
		int totalKillCount = GameManager.Instance.player.totalKillCount;

		// 먼저 PlayerPrefs의 캐시에 값을 입력 (키:string, 값:object)
		PlayerPrefs.SetInt("TotalKillCount", totalKillCount);

		// 마지막에 꼭 Save()를 호출해야 저장 완료
		PlayerPrefs.Save();
	}

	// 프로세스가 종료될 때 호출되는 메시지 함수
	private void OnApplicationQuit()
	{
		OnSave();
	}
}
