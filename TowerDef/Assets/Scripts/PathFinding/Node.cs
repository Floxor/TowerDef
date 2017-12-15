using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node  {

	public bool _isWalkable;
	public Vector3 _worldPos;
	public int _gridX;
	public int _gridY;
	public Node _parent;

	public int gCost;
	public int hCost;

	public int fCost {
		get { return gCost + hCost; }
	}

	public Node(bool p_isWalkable , Vector3 p_worldPos, int p_gridX, int p_gridY) {
		_isWalkable = p_isWalkable;
		_worldPos = p_worldPos;
		_gridX = p_gridX;
		_gridY = p_gridY;
	}

}
