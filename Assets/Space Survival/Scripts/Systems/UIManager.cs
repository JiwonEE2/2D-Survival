using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// UI를 관리하는 싱글톤 오브젝트
public class UIManager : SingletonManager<UIManager>
{
	//public static T instance => public static UIManager instance;
	public Canvas mainCanvas;       // 메인 UICanvas
	public GameObject pausePanel;   // 일시정지 패널
	public GameObject levelupPanel; // 레벨업 패널

	public Text killCountText;
	public Text totalKillCountText;
	public Text levelText;
	public Text expText;
	public Image hpBarImage;

	protected override void Awake()
	{
		base.Awake();
		//mainCanvas = GetComponent<Canvas>();
		//pausePanel = transform.Find("PausePanel").gameObject;
		//levelupPanel = transform.Find("LevelupPanel").gameObject;
	}

	// Reset 메시지 함수 : 컴포넌트가 처음 부착되거나 컴포넌트 메뉴의 Reset을 선택할 경우 호출
	private void Reset()
	{
		mainCanvas = GetComponent<Canvas>();
		pausePanel = transform.Find("PausePanel")?.gameObject;
		levelupPanel = transform.Find("LevelupPanel")?.gameObject;
	}
}
