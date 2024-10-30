using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveController : MonoBehaviour
{
	public Joystick joystick;
	// 기존에 Input을 이용해서 이동시키려면
	public float moveSpeed;

	private void Update()
	{
		// 전처리 지시문 if : 컴파일 단계에서 정의된 심볼에 따라 일부 코드를 완전히 무시하고 컴파일 함
#if UNITY_STANDALONE_WIN
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
#elif UNITY_ANDROID
		float x = joystick.x;
		float y = joystick.y;
#endif
		Vector3 moveDir = new Vector3(x, y) * Time.deltaTime * moveSpeed;
		transform.Translate(moveDir);
	}
}
