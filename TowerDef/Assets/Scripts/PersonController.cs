using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour {

	#region Variables

	PersonMovement _persoMovement;

	float _speedMove = 5f;

	float _lookSensitivity = 3f;

	#endregion

	#region Unity_methods

	void OnEnable () {
		_persoMovement = GetComponent<PersonMovement>();
		_persoMovement.enabled = true;
		_persoMovement._camera = transform.GetChild(0).GetChild(0);
	}

	void OnDisable() {
		_persoMovement.enabled = false;
	}

	void Update () {
		float _xMov = Input.GetAxisRaw("Horizontal");
		float _zMov = Input.GetAxisRaw("Vertical");

		Vector3 _moveHori = transform.right * _xMov;
		Vector3 _moveVert = transform.forward * _zMov;

		Vector3 _velocity = (_moveHori + _moveVert).normalized * _speedMove;

		_persoMovement.Move(_velocity);

		float _yRot = Input.GetAxisRaw("Mouse X");
		Vector3 _Xrotation = new Vector3(0 , _yRot , 0) * _lookSensitivity;
		
		float _xRot = Input.GetAxisRaw("Mouse Y");
		Vector3 _Yrotation = new Vector3(-_xRot, 0, 0) * _lookSensitivity;

		_persoMovement.Rotate(_Xrotation);
		_persoMovement.RotateCam(_Yrotation);
	}

	#endregion

	#region Methods


	#endregion
}
