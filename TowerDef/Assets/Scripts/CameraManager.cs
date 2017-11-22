using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraManager : Singleton<CameraManager> {

	#region Variables

	public Action<CameraState> _onChangeState;

	[HideInInspector]
	public CameraState _startCameraState = CameraState.MENU;
	public CameraState _currentCameraState;

	Transform _mainCamera;
	[SerializeField]
	List<Transform> _cameraSlots = new List<Transform>();

	int _startSlotIndex = 0;
	int _previousSlotIndex;
	int _actualSlotIndex = 0;

	bool _onTranslation = false;
	[SerializeField]
	float _translationTime = 1;

	#endregion

	#region Unity_Methods

	void Awake() {
		_mainCamera = GameObject.FindGameObjectWithTag(Tags._mainCamera).transform;
		for(int i = 0; i < transform.childCount; i++) {
			_cameraSlots.Add(transform.GetChild(i));
		}
	}

	//	void Start() {
	//		ChangeSlot (_startSlotIndex);
	//		_actualSlotIndex = _startSlotIndex;
	//	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Space)) {
			if(!_onTranslation) {
				_actualSlotIndex += 1;
				if(_actualSlotIndex > _cameraSlots.Count - 1) {
					_actualSlotIndex = 0;
				}
				StartCoroutine(ChangeSlot(_actualSlotIndex));
			}
		}
	}

	#endregion

	#region Methods

	public void Init() {
		StartCoroutine(ChangeSlot((int)_startCameraState));
	}

	IEnumerator ChangeSlot(int p_index) {
		Vector3 tempPos = _mainCamera.position;
		Quaternion tempRot = _mainCamera.rotation;

		ChangeCameraState(CameraState.NONE);

		_onTranslation = true;
		
		StartCoroutine(PersonalMethods.MyLerp(_mainCamera, _cameraSlots[p_index].position, _translationTime));
		StartCoroutine(PersonalMethods.MySlerp(_mainCamera, _cameraSlots[p_index].rotation, _translationTime));
		_mainCamera.SetParent(_cameraSlots[p_index]);
		
		yield return new WaitForSeconds(_translationTime);

		ChangeCameraState((CameraState)p_index);

		StopCoroutine("PersonalMethods.MyLerp");
		StopCoroutine("PersonalMethods.MySlerp");
		
		if(_previousSlotIndex != 0) {
			_cameraSlots[_previousSlotIndex].position = tempPos;
			_cameraSlots[_previousSlotIndex].rotation = tempRot;
		}
		_previousSlotIndex = _actualSlotIndex;

		_onTranslation = false;
		
		StopCoroutine("ChangeSlot");
	}

	public CameraState GetCameraState() {
		return CameraManager.Instance._currentCameraState;
	}

	public void ChangeCameraState(CameraState p_camState) {

		if(p_camState == _currentCameraState) {
			return;
		}

		if(_onChangeState != null) {
			_onChangeState(p_camState);
		}

		_currentCameraState = p_camState;

		switch(_currentCameraState) {
			case CameraState.MENU:
				//Debug.Log ("CameraState de MENU");
				// UI de Manager
				// Camera Déplacement OK
				// Camera Rotation NOPE
				break;

			case CameraState.CASTLE:
				//Debug.Log ("CameraState de CASTLE");
				// UI de Castle stats et compétences
				// Camera Déplacement NOPE
				// Camera Rotation OK
				break;

			case CameraState.HERO:
				//Debug.Log ("CameraState de HERO");
				// UI de Hero stats et compétences
				// Camera Déplacement OK
				// Camera Rotation OK
				break;

			case CameraState.TURRET:
				//Debug.Log ("CameraState de TURRET");
				// UI de Turret stats et compétences
				// Camera Déplacement NOPE
				// Camera Rotation OK
				break;
			default :
				Debug.Log ("Tag : " + _currentCameraState + " est non reconnu");
				break;
		}
	//		Debug.Log ("---------------------------");
	}

	#endregion
}
