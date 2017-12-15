using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	[SerializeField]
	GameObject _tile;



	int _xLength = 25;
	int _yLength = 25;

	void Start() {
		Generate();
	}

	public void Generate() {
		for(int i = 0; i < _xLength; ++i) {
			for(int j = 0; j < _yLength; ++j){
				float xPos = i * _tile.transform.localScale.x;
				float zPos = j * _tile.transform.localScale.z;

				GameObject Tile = Instantiate(_tile , new Vector3(xPos, 0, zPos) , _tile.transform.rotation, transform);
				Tile.name = "Tile_"+ i + "_" +j;
			}
		}
	}
}
