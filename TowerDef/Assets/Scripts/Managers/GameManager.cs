using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager> {

	#region Variables

	[SerializeField]
	GameObject _turretPrefab;
	RaycastHit _hit;

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
			//Debug.Log ("Field");
			// UI Création d'unité
			TurretManager.Instance.Create(_hit.point);
			break;
		default:
			Debug.Log ("Tag : " + p_tag + " est non reconnu");
			break;
		}
	}

	public void ChangeStateComponent(GameObject p_obj , string p_classType) {
		switch(p_classType) {
			case "PersonController":
				if (p_obj.GetComponent(p_classType) != null) {
					p_obj.GetComponent<PersonController>().enabled = !p_obj.GetComponent<PersonController>().enabled;
				}
				break;
			default:
				Debug.Log("Type : " + p_classType + " n'est pas gérer");
				break;
		}
	}

	#endregion
}
