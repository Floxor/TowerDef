using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager> {

	#region Variables

	[SerializeField]
	GameObject _turretPrefab;
	RaycastHit _hit;
	float _yPadding = 2;

	#endregion

	#region Unity_methods

	void Awake() {
	}

	void Start() {
		Initialisation();
	}

	void Update() {
		if (CameraManager.Instance._currentCameraState == CameraState.MENU) {
			if (Input.GetKeyDown(KeyCode.Mouse1)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast(ray, out _hit)) {
					CheckPossibilities(_hit.collider.tag);
				}
			}
		}
	}

	#endregion

	#region Methods

	void Initialisation() {
		CameraManager.Instance.Init();
	}

	void CheckPossibilities(string p_tag) {
		switch(p_tag) {
		case Tags._mainCamera:
			Debug.Log ("Main Camera");
			break;
		case Tags._turret:
			Debug.Log ("Turret");
			// UI Amélioration Turret
			break;
		case Tags._field:
			Debug.Log ("Field");
			// UI Création d'unité
			Vector3 spawnPos = new Vector3 (_hit.point.x, _hit.point.y + _yPadding, _hit.point.z);
			Instantiate (_turretPrefab, spawnPos, Quaternion.identity);
			break;
		default:
			Debug.Log ("Tag : " + p_tag + " est non reconnu");
			break;
		}
	}

	#endregion
}
