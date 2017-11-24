using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour {

	#region Variables

	GameObject _canvas;

	public GameObject _menuRadial;

	#endregion

	#region Unity_methods

	void OnEnable() {
		CameraManager.Instance._onChangeState += ChangeCameraState;

		_canvas = GameObject.FindWithTag(Tags._canvas);
	}

	void OnDisable() {
		if (CameraManager.Instance != null) {
			CameraManager.Instance._onChangeState -= ChangeCameraState;
		}
	}

	void Update() {
		//if(CameraManager.Instance._currentCameraState == CameraState.MENU) {
			if(Input.GetKeyDown(KeyCode.Mouse0)) {
				SetRadial(true);
			} else if (Input.anyKeyDown) {
				SetRadial(false);
			}
		//}
	}

	#endregion

	#region Methods

	public void SetRadial(bool p_active) {
		if (p_active) {
			Vector2 pos;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, Input.mousePosition, _canvas.GetComponent<Canvas>().worldCamera, out pos);
			_menuRadial.transform.position = _canvas.transform.TransformPoint(pos);
		}

		_menuRadial.gameObject.SetActive(p_active);
	}

	public void ChangeCameraState(CameraState p_camState) {
		switch (p_camState) {
		case CameraState.MENU:
//			Debug.Log ("UI de MENU");
			// UI de Manager
			break;

		case CameraState.EDITOR:
//			Debug.Log ("UI de EDITOR");
			// UI de Editor stats et compétences
			break;

		case CameraState.CASTLE:
//			Debug.Log ("UI de CASTLE");
			// UI de Castle stats et compétences
			break;

		case CameraState.HERO:
//			Debug.Log ("UI de HERO");
			// UI de Hero stats et compétences
			break;

		case CameraState.TURRET:
//			Debug.Log ("UI de TURRET");
			// UI de Turret stats et compétences
			break;
		}
//		Debug.Log ("&&&&&&&&&&&&&&&&&&&&&&");
	}

	#endregion
}
