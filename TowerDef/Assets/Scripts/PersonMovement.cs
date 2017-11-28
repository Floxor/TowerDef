using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PersonMovement : MonoBehaviour {

	#region Variables

	Rigidbody _rigidbody;
	Transform _personCamera;
	public Transform PersonCamera {
		get { return _personCamera; }
		set { _personCamera = value; }
	}

	Vector3 _velocity = Vector3.zero;
	Vector3 _rotation = Vector3.zero;
	Vector3 _camRot = Vector3.zero;

	#endregion

	#region Unity_methods

	void Start () {
		_rigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		PerformMovement();
		PerformRotation();
	}

	#endregion

	#region Methods

	public void Move(Vector3 p_velocity) {
		_velocity = p_velocity;

	}

	void PerformMovement() {
		if (_velocity != Vector3.zero) {
			_rigidbody.MovePosition(_rigidbody.position + _velocity * Time.fixedDeltaTime);
		}
	}

	public void Rotate(Vector3 p_rotation) {
		_rotation = p_rotation;
	}

	public void RotateCam(Vector3 p_rotation) {
		_camRot = p_rotation;
	}

	void PerformRotation() {
		_rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(_rotation));
		if (_personCamera != null) {
			_personCamera.Rotate(_camRot);
		}
	}

	#endregion
}
