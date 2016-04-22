using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InfiniteTerrain : MonoBehaviour
{
	public GameObject PlayerObject;

	public GameObject tetrahedron;
	public int numberOfObjects;

	List<GameObject> tetrahedron_list_old = new List<GameObject>();
	List<GameObject> tetrahedron_list = new List<GameObject>();

	private Terrain[,] _terrainGrid = new Terrain[3,3];
	
	void Start ()
	{
		Terrain linkedTerrain = gameObject.GetComponent<Terrain>();
		
		_terrainGrid[0,0] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
		_terrainGrid[0,1] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
		_terrainGrid[0,2] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
		_terrainGrid[1,0] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
		_terrainGrid[1,1] = linkedTerrain;
		_terrainGrid[1,2] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
		_terrainGrid[2,0] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
		_terrainGrid[2,1] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
		_terrainGrid[2,2] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();

		UpdateTerrainPositionsAndNeighbors();

		for (int x = 0; x < 3; x++) {
			PlaceCubes (Mathf.FloorToInt (_terrainGrid [x, 0].transform.position.x), 
				Mathf.FloorToInt (_terrainGrid [x, 0].transform.position.x) + Mathf.FloorToInt (_terrainGrid [x, 0].terrainData.size.x),
				Mathf.FloorToInt (_terrainGrid [x, 0].transform.position.z), 
				Mathf.FloorToInt (_terrainGrid [x, 0].transform.position.z) + Mathf.FloorToInt (_terrainGrid [x, 0].terrainData.size.z));
		}
	}
	
	private void UpdateTerrainPositionsAndNeighbors()
	{
		_terrainGrid[0,0].transform.position = new Vector3(
			_terrainGrid[1,1].transform.position.x - _terrainGrid[1,1].terrainData.size.x,
			_terrainGrid[1,1].transform.position.y,
			_terrainGrid[1,1].transform.position.z + _terrainGrid[1,1].terrainData.size.z);
		_terrainGrid[0,1].transform.position = new Vector3(
			_terrainGrid[1,1].transform.position.x - _terrainGrid[1,1].terrainData.size.x,
			_terrainGrid[1,1].transform.position.y,
			_terrainGrid[1,1].transform.position.z);
		_terrainGrid[0,2].transform.position = new Vector3(
			_terrainGrid[1,1].transform.position.x - _terrainGrid[1,1].terrainData.size.x,
			_terrainGrid[1,1].transform.position.y,
			_terrainGrid[1,1].transform.position.z - _terrainGrid[1,1].terrainData.size.z);
		
		_terrainGrid[1,0].transform.position = new Vector3(
			_terrainGrid[1,1].transform.position.x,
			_terrainGrid[1,1].transform.position.y,
			_terrainGrid[1,1].transform.position.z + _terrainGrid[1,1].terrainData.size.z);
		_terrainGrid[1,2].transform.position = new Vector3(
			_terrainGrid[1,1].transform.position.x,
			_terrainGrid[1,1].transform.position.y,
			_terrainGrid[1,1].transform.position.z - _terrainGrid[1,1].terrainData.size.z);
		
		_terrainGrid[2,0].transform.position = new Vector3(
			_terrainGrid[1,1].transform.position.x + _terrainGrid[1,1].terrainData.size.x,
			_terrainGrid[1,1].transform.position.y,
			_terrainGrid[1,1].transform.position.z + _terrainGrid[1,1].terrainData.size.z);
		_terrainGrid[2,1].transform.position = new Vector3(
			_terrainGrid[1,1].transform.position.x + _terrainGrid[1,1].terrainData.size.x,
			_terrainGrid[1,1].transform.position.y,
			_terrainGrid[1,1].transform.position.z);
		_terrainGrid[2,2].transform.position = new Vector3(
			_terrainGrid[1,1].transform.position.x + _terrainGrid[1,1].terrainData.size.x,
			_terrainGrid[1,1].transform.position.y,
			_terrainGrid[1,1].transform.position.z - _terrainGrid[1,1].terrainData.size.z);
		
		_terrainGrid[0,0].SetNeighbors(             null,              null, _terrainGrid[1,0], _terrainGrid[0,1]);
		_terrainGrid[0,1].SetNeighbors(             null, _terrainGrid[0,0], _terrainGrid[1,1], _terrainGrid[0,2]);
		_terrainGrid[0,2].SetNeighbors(             null, _terrainGrid[0,1], _terrainGrid[1,2],              null);
		_terrainGrid[1,0].SetNeighbors(_terrainGrid[0,0],              null, _terrainGrid[2,0], _terrainGrid[1,1]);
		_terrainGrid[1,1].SetNeighbors(_terrainGrid[0,1], _terrainGrid[1,0], _terrainGrid[2,1], _terrainGrid[1,2]);
		_terrainGrid[1,2].SetNeighbors(_terrainGrid[0,2], _terrainGrid[1,1], _terrainGrid[2,2],              null);
		_terrainGrid[2,0].SetNeighbors(_terrainGrid[1,0],              null,              null, _terrainGrid[2,1]);
		_terrainGrid[2,1].SetNeighbors(_terrainGrid[1,1], _terrainGrid[2,0],              null, _terrainGrid[2,2]);
		_terrainGrid[2,2].SetNeighbors(_terrainGrid[1,2], _terrainGrid[2,1],              null,              null);
	}
	
	void Update ()
	{
		Vector3 playerPosition = new Vector3(PlayerObject.transform.position.x, PlayerObject.transform.position.y, PlayerObject.transform.position.z);
		Terrain playerTerrain = null;
		int xOffset = 0;
		int yOffset = 0;
		for (int x = 0; x < 3; x++)
		{
			for (int y = 0; y < 3; y++)
			{
				if ((playerPosition.x >= _terrainGrid[x,y].transform.position.x) &&
					(playerPosition.x <= (_terrainGrid[x,y].transform.position.x + _terrainGrid[x,y].terrainData.size.x)) &&
					(playerPosition.z >= _terrainGrid[x,y].transform.position.z) &&
					(playerPosition.z <= (_terrainGrid[x,y].transform.position.z + _terrainGrid[x,y].terrainData.size.z)))
				{
					playerTerrain = _terrainGrid[x,y];
					xOffset = 1 - x;
					yOffset = 1 - y;
					break;
				}
			}
			if (playerTerrain != null)
				break;
		}
		
		if (playerTerrain != _terrainGrid[1,1])
		{
			bool addObstacles = false;

			Terrain[,] newTerrainGrid = new Terrain[3,3];
			for (int x = 0; x < 3; x++)
				for (int y = 0; y < 3; y++)
				{
					int newX = x + xOffset;
					if (newX < 0)
						newX = 2;
					else if (newX > 2)
						newX = 0;
					int newY = y + yOffset;
					if (newY < 0)
						newY = 2;
					else if (newY > 2)
						newY = 0;

					// Only add obstacles if plane moved up one terrain
					if(y != newY) {
						addObstacles = true;
					}

					newTerrainGrid[newX, newY] = _terrainGrid[x,y];
				}
			_terrainGrid = newTerrainGrid;
			UpdateTerrainPositionsAndNeighbors();


			if(addObstacles) {
				for (int x = 0; x < 3; x++) {
					PlaceCubes (Mathf.FloorToInt (_terrainGrid [x, 0].transform.position.x), 
						Mathf.FloorToInt (_terrainGrid [x, 0].transform.position.x) + Mathf.FloorToInt (_terrainGrid [x, 0].terrainData.size.x),
						Mathf.FloorToInt (_terrainGrid [x, 0].transform.position.z), 
						Mathf.FloorToInt (_terrainGrid [x, 0].transform.position.z) + Mathf.FloorToInt (_terrainGrid [x, 0].terrainData.size.z));
				}
			}

			for(int i = tetrahedron_list.Count - 1; i>=0; i--) {
				if(tetrahedron_list[i].transform.position.z < PlayerObject.transform.position.z) {
					Destroy (tetrahedron_list[i]);
					tetrahedron_list.RemoveAt(i);
				}
			}
			
		}
	}

	void PlaceCubes(int min_x, int max_x, int min_z, int max_z) {
		for(int i = 0; i < numberOfObjects; i++) {
			Object obj = Instantiate (tetrahedron, GeneratedPosition(min_x, max_x, min_z, max_z), tetrahedron.transform.rotation );
			tetrahedron_list.Add ((GameObject)obj);
		}
	}

	Vector3 GeneratedPosition(int min_x, int max_x, int min_z, int max_z) {
		int x, y, z;
		x = Random.Range (min_x, max_x);
		y = 0;
		z = Random.Range (min_z, max_z);

		return new Vector3 (x, y, z);
	}
}
