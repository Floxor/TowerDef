using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : Singleton<TurretManager> {

	#region Variables

	[SerializeField]
	GameObject _turretPrefab;
	float _yPadding = 2;

	#endregion

	#region Unity_methods

	void Start () {
		
	}

	void Update () {
		
	}

	#endregion

	#region Methods

	public void Create(Vector3 p_position) {
		Vector3 spawnPos = new Vector3 (p_position.x, p_position.y + _yPadding, p_position.z);
		Quaternion rndRot = Quaternion.Euler(0 , Random.Range(0f , 360f) , 0);

		GameObject newTurret = Instantiate (_turretPrefab, spawnPos, rndRot);
		CameraManager.Instance.CameraSlots.Add(newTurret.transform.GetChild(0).GetComponent<CameraSlot>());
	}

	#endregion
}
