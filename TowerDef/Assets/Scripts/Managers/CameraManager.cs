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
	List<CameraSlot> _cameraSlots = new List<CameraSlot>();

	public List<CameraSlot> CameraSlots {
		get { return _cameraSlots; }
		set { _cameraSlots = value; }
	}
	public GameObject[] _camSlots;
	RaycastHit _hit;

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

		_camSlots = GameObject.FindGameObjectsWithTag(Tags._cameraSlot);

		for(int i = 0; i < _camSlots.Length; ++i) {
			for(int j = 0; j < _cameraSlots.Count; ++j) {
				if (_cameraSlots.Contains(_camSlots[i].GetComponent<CameraSlot>())) {
					break;
				} else {
					_camSlots[i].GetComponent<CameraSlot>()._index = i;
					_cameraSlots.Add(_camSlots[i].GetComponent<CameraSlot>());
				}
			}
		}
	}

	void FixedUpdate() {
	}

	void Update() {
		if(!_onTranslation) {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				StartCoroutine(ChangeSlot(0));
			}
			if(Input.GetKeyDown(KeyCode.Space)) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out _hit)) {
					if (_hit.collider.transform.childCount > 0) {
						Debug.Log(_hit.transform.name);
						StartCoroutine(ChangeSlot(CheckPossibilities(_hit)));
					}
				}
			}
		}
	}


	#endregion

	#region Methods

	public void Init() {
		StartCoroutine(ChangeSlot((int)_startCameraState));
	}

	int CheckPossibilities(RaycastHit p_hit) {
		if (p_hit.collider.transform.childCount > 0) {
			if(p_hit.collider.transform.GetChild(0).GetComponent<CameraSlot>()) {
				return p_hit.collider.transform.GetChild(0).GetComponent<CameraSlot>()._index;
			}
		}
		return -1;
	}

	IEnumerator ChangeSlot(int p_index) {
		if (p_index < 0 || p_index > _cameraSlots.Count) {
			yield return null;
		} else {
			Vector3 tempPos = _mainCamera.position;
			Quaternion tempRot = _mainCamera.rotation;

			ChangeCameraState(CameraState.NONE);

			_onTranslation = true;

			StartCoroutine(PersonalMethods.MyLerp(_mainCamera, _cameraSlots[p_index].transform.position, _translationTime));
			StartCoroutine(PersonalMethods.MySlerp(_mainCamera, _cameraSlots[p_index].transform.rotation, _translationTime));
			_mainCamera.SetParent(_cameraSlots[p_index].transform);
			
			yield return new WaitForSeconds(_translationTime * 0.75f);

			ChangeCameraState(_cameraSlots[p_index]._slotType);

			StopCoroutine("PersonalMethods.MyLerp");
			StopCoroutine("PersonalMethods.MySlerp");
			
			if(_previousSlotIndex != 0) {
				_cameraSlots[_previousSlotIndex].transform.position = tempPos;
				_cameraSlots[_previousSlotIndex].transform.rotation = tempRot;
			}
			_previousSlotIndex = _actualSlotIndex;

			_onTranslation = false;
		}

		StopCoroutine("ChangeSlot");
	}

	public CameraState GetCameraState() {
		return CameraManager.Instance._currentCameraState;
	}

	void BeforeChangeState(CameraState p_camState) {
		switch(_currentCameraState) {
			case CameraState.MENU:
				break;

			case CameraState.EDITOR:
				break;

			case CameraState.CASTLE:
				GameManager.Instance.ChangeStateComponent(_mainCamera.parent.parent.gameObject, "PersonController");
				break;

			case CameraState.HERO:
				GameManager.Instance.ChangeStateComponent(_mainCamera.parent.parent.gameObject, "PersonController");
				break;

			case CameraState.TURRET:
				GameManager.Instance.ChangeStateComponent(_mainCamera.parent.parent.gameObject, "PersonController");
				break;

			default :
				break;
		}
	}

	public void ChangeCameraState(CameraState p_camState) {

		if(p_camState == _currentCameraState) {
			return;
		}

		if(_onChangeState != null) {
			_onChangeState(p_camState);
		}

		BeforeChangeState(_currentCameraState);

		_currentCameraState = p_camState;

		switch(_currentCameraState) {
			case CameraState.MENU:
				//Debug.Log ("CameraState de MENU");
				// UI de Manager
				// Camera Déplacement OK
				// Camera Rotation NOPE
				Cursor.lockState = CursorLockMode.Confined;
				break;

			case CameraState.EDITOR:
				// Debug.Log("CameraState de EDITOR");
				break;

			case CameraState.CASTLE:
				//Debug.Log ("CameraState de CASTLE");
				// UI de Castle stats et compétences
				// Camera Déplacement NOPE
				// Camera Rotation OK
				GameManager.Instance.ChangeStateComponent(_mainCamera.parent.parent.gameObject, "PersonController");
				break;

			case CameraState.HERO:
				//Debug.Log ("CameraState de HERO");
				// UI de Hero stats et compétences
				GameManager.Instance.ChangeStateComponent(_mainCamera.parent.parent.gameObject, "PersonController");
				break;

			case CameraState.TURRET:
				//Debug.Log ("CameraState de TURRET");
				// UI de Turret stats et compétences
				// Camera Déplacement NOPE
				// Camera Rotation OK
				GameManager.Instance.ChangeStateComponent(_mainCamera.parent.parent.gameObject, "PersonController");
				break;

			default :
				if(_currentCameraState != CameraState.NONE) {
					Debug.Log ("Tag : " + _currentCameraState + " est non reconnu");
				}
				break;
		}
	//		Debug.Log ("---------------------------");
	}

	#endregion
}
