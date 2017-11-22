using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour {
	
	#region Variables


	#endregion

	#region Unity_methods

	void OnEnable() {
		CameraManager.Instance._onChangeState += ChangeCameraState;
	}

	void OnDisable() {
		if (CameraManager.Instance != null) {
			CameraManager.Instance._onChangeState -= ChangeCameraState;
		}
	}

	#endregion

	#region Methods

	public void ChangeCameraState(CameraState p_camState) {
		switch (p_camState) {
		case CameraState.MENU:
//			Debug.Log ("UI de MENU");
			// UI de Manager
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
