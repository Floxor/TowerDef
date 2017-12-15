using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : Singleton<UIManager> {

	#region Variables

	GameObject _canvas;

	public GameObject _crossHair;
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

	public void SetCrosshair(bool p_active) {
		if (p_active) {
			Vector2 pos;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, Input.mousePosition, _canvas.GetComponent<Canvas>().worldCamera, out pos);
			_crossHair.transform.position = _canvas.transform.TransformPoint(pos);
		}

		_crossHair.SetActive(p_active);
	}

	void BeforeChangeState(CameraState p_camState) {
		switch(p_camState) {
			case CameraState.MENU:
				Cursor.lockState = CursorLockMode.Locked;
				break;

			case CameraState.EDITOR:
				Cursor.lockState = CursorLockMode.Locked;
				break;

			case CameraState.CASTLE:
				Cursor.lockState = CursorLockMode.Locked;
				break;

			case CameraState.HERO:
				break;

			case CameraState.TURRET:
				break;

			default:
				break;
		}
	}

	public void ChangeCameraState(CameraState p_camState) {
		SetCrosshair(false);

        BeforeChangeState(CameraManager.Instance.CurrentCameraState);

		switch (p_camState) {
		case CameraState.MENU:
//			Debug.Log ("UI de MENU");
			// UI de Manager
			Cursor.lockState = CursorLockMode.None;
			break;

		case CameraState.EDITOR:
//			Debug.Log ("UI de EDITOR");
			// UI de Editor stats et compétences
			Cursor.lockState = CursorLockMode.None;
			break;

		case CameraState.CASTLE:
//			Debug.Log ("UI de CASTLE");
			// UI de Castle stats et compétences
			Cursor.lockState = CursorLockMode.Locked;
			SetCrosshair(true);
			break;

		case CameraState.HERO:
//			Debug.Log ("UI de HERO");
			// UI de Hero stats et compétences
			Cursor.lockState = CursorLockMode.Locked;
			SetCrosshair(true);
			break;

		case CameraState.TURRET:
//			Debug.Log ("UI de TURRET");
			// UI de Turret stats et compétences
			Cursor.lockState = CursorLockMode.Locked;
			SetCrosshair(true);
			break;
		}
	}

	#endregion
}
