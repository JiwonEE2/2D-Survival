using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// UI�� �����ϴ� �̱��� ������Ʈ
public class UIManager : SingletonManager<UIManager>
{
	//public static T instance => public static UIManager instance;
	public Canvas mainCanvas;       // ���� UICanvas
	public GameObject pausePanel;   // �Ͻ����� �г�
	public GameObject levelupPanel; // ������ �г�

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

	// Reset �޽��� �Լ� : ������Ʈ�� ó�� �����ǰų� ������Ʈ �޴��� Reset�� ������ ��� ȣ��
	private void Reset()
	{
		mainCanvas = GetComponent<Canvas>();
		pausePanel = transform.Find("PausePanel")?.gameObject;
		levelupPanel = transform.Find("LevelupPanel")?.gameObject;
	}
}
