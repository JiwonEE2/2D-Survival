using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
	public Transform background;
	public Transform handle;

	private const float MAX_DIST = 200f;  // handle�� �̵��� �� �ִ� �ִ� �Ÿ�

	public float x => handle.localPosition.x / MAX_DIST;
	public float y => handle.localPosition.y / MAX_DIST;

	private void Start()
	{
		background.gameObject.SetActive(false);
#if UNITY_EDITOR
		Input.simulateMouseWithTouches = true;
#endif
	}

	private void Update()
	{
		//print(Input.touchCount);
		// ��ġ �Է��� ������ ����̽� (��ǥ������ ����Ʈ��)�� ��ġ �Է��� ���� ��� �ش� ��ġ ������ŭ ī��Ʈ��
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			//background.gameObject.SetActive(true);
			//background.position = touch.position;

			switch (touch.phase)
			{
				case TouchPhase.Began:  // ��ġ�� ���۵� ��
					background.gameObject.SetActive(true);
					background.position = touch.position;
					break;
				case TouchPhase.Moved:  // ��ġ �� �հ����� ������ ��
					handle.position = touch.position;
					handle.localPosition = Vector3.ClampMagnitude(handle.localPosition, MAX_DIST);
					break;
				case TouchPhase.Ended:  // ��ġ�� ���� �� (�հ����� �� ��)
				case TouchPhase.Canceled: // �ý��� �Ǵ� ȭ�� ������ �հ����� ������ �� ��ġ�� ��ҵ� ��
					handle.localPosition = Vector3.zero;
					background.gameObject.SetActive(false);
					break;
			}
		}
	}
}
