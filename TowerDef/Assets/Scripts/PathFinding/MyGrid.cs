﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid : MonoBehaviour {

	public LayerMask _unwalkableMask;
	public Vector2 _gridWorldSize;
	public float _nodeRadius;
	Node[,] _grid;
	public List<Node> _path;

	float _nodeDiameter;
	int _gridSizeX, _gridSizeY;

	void Start() {
		_nodeDiameter = _nodeRadius * 2;
		_gridSizeX = Mathf.RoundToInt(_gridWorldSize.x / _nodeDiameter);
		_gridSizeY = Mathf.RoundToInt(_gridWorldSize.y / _nodeDiameter);
		CreateGrid();
	}

	void CreateGrid() {
		_grid = new Node[_gridSizeX , _gridSizeY];
		Vector3 worldBottomLeft = transform.position - (Vector3.right * _gridWorldSize.x / 2) - (Vector3.forward * _gridWorldSize.y / 2);

		for(int x = 0; x < _gridSizeX; ++x) {
			for(int y = 0; y < _gridSizeY; ++y) {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * _nodeDiameter + _nodeRadius) + Vector3.forward * (y * _nodeDiameter + _nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint , _nodeRadius, _unwalkableMask));
				_grid[x , y] = new Node(walkable, worldPoint, x, y);
			}
		}
	}

	public List<Node> GetNeighbours(Node p_node) {
		List<Node> neighbours = new List<Node>();

		for(int x = -1; x <= 1; ++x) {
			for(int y = -1; y <= 1; ++y) {
				if(x == 0 && y == 0) {
					continue;
				}

				int checkX = p_node._gridX + x;
				int checkY = p_node._gridY + y;

				if (checkX >= 0 && checkX < _gridSizeX && checkY >= 0 && checkY < _gridSizeY) {
					neighbours.Add(_grid[checkX , checkY]);
				}
			}
		}

		return neighbours;
	}

	public Node GetNodeFromWorldPoint(Vector3 p_worldPos) {
		float percentX = (p_worldPos.x + _gridWorldSize.x / 2) / _gridWorldSize.x;
		float percentY = (p_worldPos.z + _gridWorldSize.y / 2) / _gridWorldSize.y;

		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);

		return _grid[x , y];
	}

	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position , new Vector3(_gridWorldSize.x , 1 , _gridWorldSize.y));

		if (_grid != null) {
			foreach(Node node in _grid) {
				Gizmos.color = (node._isWalkable) ? Color.white : Color.red;
				if (_path != null) {
					if (_path.Contains(node)) {
						Gizmos.color = Color.black;
					}
				}
				Gizmos.DrawCube(node._worldPos , Vector3.one * (_nodeDiameter - 0.1f));
			}
		}
	}
}
