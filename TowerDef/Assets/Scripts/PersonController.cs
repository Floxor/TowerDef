using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour {

	#region Variables

	PersonMovement _persoMovement;

	[SerializeField]
	bool _allowedMovement = false;
	[SerializeField]
	bool _allowedRotationOnX = false;
	[SerializeField]
	bool _allowedRotationOnY = false;
	bool _canMove;
	bool _canRotateOnX;
	bool _canRotateOnY;

	float _speedMove = 5f;

	float _lookSensitivity = 3f;

	#endregion

	#region Unity_methods

	void OnEnable () {
		_persoMovement = GetComponent<PersonMovement>();
		_persoMovement.enabled = true;
		_persoMovement.PersonCamera = transform.GetChild(0).GetChild(0);

		Init();
	}

	void OnDisable() {
		_canMove = _canRotateOnX = _canRotateOnY = false;
		_persoMovement.PersonCamera = null;
		_persoMovement.enabled = false;
	}

	void Init() {
		_canMove = _allowedMovement;
		_canRotateOnX = _allowedRotationOnX;
		_canRotateOnY = _allowedRotationOnY;
	}

	void Update () {
		if (_canMove) {
			float _xMov = Input.GetAxisRaw("Horizontal");
			float _zMov = Input.GetAxisRaw("Vertical");

			Vector3 _moveHori = transform.right * _xMov;
			Vector3 _moveVert = transform.forward * _zMov;

			Vector3 _velocity = (_moveHori + _moveVert).normalized * _speedMove;

			_persoMovement.Move(_velocity);
		}

		if (_canRotateOnX) {
			float _yRot = Input.GetAxisRaw("Mouse X");
			Vector3 _Xrotation = new Vector3(0 , _yRot , 0) * _lookSensitivity;

			_persoMovement.Rotate(_Xrotation);
		}

		if (_canRotateOnY) {
			float _xRot = Input.GetAxisRaw("Mouse Y");
			Vector3 _Yrotation = new Vector3(-_xRot, 0, 0) * _lookSensitivity;

			_persoMovement.RotateCam(_Yrotation);
		}
	}

	#endregion

	#region Methods


	#endregion
}
