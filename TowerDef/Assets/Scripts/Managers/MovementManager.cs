using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour {

	#region Variables


	#endregion

	#region Unity_methods

	void OnEnable() {
		CameraManager.Instance._onChangeState += ChangeCameraState;
	}

	void OnDisable() {
		if(CameraManager.Instance != null) {
			CameraManager.Instance._onChangeState -= ChangeCameraState;
		}
	}

	#endregion

	#region Methods

	public void ChangeCameraState(CameraState p_camState) {
		switch(p_camState) {
			case CameraState.MENU:
				//Ni mouvement ni rotation
				break;

			case CameraState.EDITOR:
				//Rotation et Movement OK
				break;

			case CameraState.CASTLE:
				//Rotation OK
				break;

			case CameraState.HERO:
				//Rotation et Movement OK
				break;

			case CameraState.TURRET:
				//Rotation OK
				break;
		}
//		Debug.Log ("&&&&&&&&&&&&&&&&&&&&&&");
	}

	#endregion
}
