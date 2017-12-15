using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

	public Transform _seeker, _target;

	MyGrid _grid;

	void Awake() {
		_grid = GetComponent<MyGrid>();
	}

	void Update() {
		FindPath(_seeker.position, _target.position);
	}

	void FindPath(Vector3 p_startPos, Vector3 p_targetPos) {
		Node startNode = _grid.GetNodeFromWorldPoint(p_startPos);
		Node targetNode = _grid.GetNodeFromWorldPoint(p_targetPos);

		List<Node> openSet = new List<Node>();
		HashSet<Node> closedSet = new HashSet<Node>();
		openSet.Add(startNode);

		while (openSet.Count > 0) {
			Node currentNode = openSet[0];

			for(int i = 1; i < openSet.Count; ++i) {
				if(openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost) {
					currentNode = openSet[i];
				}
			}

			openSet.Remove(currentNode);
			closedSet.Add(currentNode);

			if (currentNode == targetNode) {
				RetracePath(startNode, targetNode);
				return;
			}

			foreach (Node neighbour in _grid.GetNeighbours(currentNode)) {
				if (!neighbour._isWalkable || closedSet.Contains(neighbour)) {
					continue;
				}

				int newMovCostToNeighbour = currentNode.gCost + MyGetDistance(currentNode, neighbour);
				if (newMovCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
					neighbour.gCost = newMovCostToNeighbour;
					neighbour.hCost = MyGetDistance(neighbour, targetNode);
					neighbour._parent = currentNode;

					if (!openSet.Contains(neighbour)) {
						openSet.Add(neighbour);
					}
				}
			}
		}
	}

	void RetracePath(Node p_startNode, Node p_endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = p_endNode;

		while(currentNode != p_startNode) {
			path.Add(currentNode);
			currentNode = currentNode._parent;
		}
		path.Reverse();

		_grid._path = path;
	}

	int MyGetDistance(Node p_nodeA, Node p_nodeB) {
		int distanceX = Mathf.Abs(p_nodeA._gridX - p_nodeB._gridX);
		int distanceY = Mathf.Abs(p_nodeA._gridY - p_nodeB._gridY);

		if (distanceX > distanceY) {
			return 14 * distanceY + 10 * (distanceX - distanceY);
		} else {
			return 14 * distanceX + 10 * (distanceY - distanceX);
		}
	}
}
