using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {

	public GameObject player;
	public GameObject RoadPrefab;
	public GameObject HousePrefab;
	public GameObject CoinPrefab;

	public bool genereteHouses;
	public bool genereteCoins;

	[Range(5, 20)]
	public int lookahead = 10;

	[Range(5, 10)]
	public float coinPosMinX = 10;
	[Range(5, 15)]
	public float coinPosMaxX = 10;


	[Range(10, 20)]
	public float housePosMinX = 10;
	[Range(20, 35)]
	public float housePosMaxX = 10;


	[Range(0.5f, 5)]
	public float houseScaleMinX;
	[Range(0.5f, 4)]
	public float houseScaleMaxX;
	[Range(0.5f, 4)]
	public float houseScaleMinY;
	[Range(0.5f, 4)]
	public float houseScaleMaxY;
	[Range(0.5f, 4)]
	public float houseScaleMinZ;
	[Range(0.5f, 4)]
	public float houseScaleMaxZ;



	int z = 0;
	List<GameObject> levelParts = new List<GameObject>();

	// Use this for initialization
	void Start () {
	
		player = GameObject.FindGameObjectWithTag("Player");
		GenerateRoad(lookahead);

	}
	
	// Update is called once per frame
	void Update () {
	
		if(player.transform.position.z > levelParts[1].transform.position.z){
			GameObject road = levelParts[0];
			levelParts.RemoveAt(0);
			GameObject.Destroy(road);
			GenerateRoad(1);
		}

	}


	void GenerateRoad(int num){


		for(int i = 0; i < num; i++){
			Vector3 roadPos = transform.position;
			roadPos.z += (z + i) * RoadPrefab.transform.localScale.z; 
			GameObject road = GameObject.Instantiate(RoadPrefab, roadPos, Quaternion.identity) as GameObject;

			if(genereteHouses){
				GameObject houseRight = GenerateHouse(roadPos);
				GameObject houseLeft = GenerateHouse(roadPos);
				Vector3 housePos = houseLeft.transform.position;
				housePos.x *= -1;
				houseLeft.transform.position = housePos;

				houseRight.transform.parent = road.transform;
				houseLeft.transform.parent = road.transform;
			}

			if(genereteCoins){
				GameObject coin = GenerateCoin(roadPos);
				coin.transform.parent = road.transform;
				float bRange = Random.value;

				if(bRange > 0.5){
					Vector3 coinPos = coin.transform.position;
					coinPos.x *= -1;
					coin.transform.position = coinPos;
				}
			}



			levelParts.Add(road);

			road.transform.parent = transform;

		}

		z+= num;

	}

	GameObject GenerateHouse(Vector3 startPos){
		GameObject house = GameObject.Instantiate(HousePrefab);
		Vector3 housePos = new Vector3(Random.Range(housePosMinX, housePosMaxX), 0, Random.Range(startPos.z - RoadPrefab.transform.localScale.z/2, startPos.z + RoadPrefab.transform.localScale.z/2));
		house.transform.position = housePos;
		house.transform.localScale = new Vector3(Random.Range(houseScaleMinX, houseScaleMaxX), Random.Range(houseScaleMinY, houseScaleMaxY), Random.Range(houseScaleMinZ, houseScaleMaxZ));


		return house;
	}

	GameObject GenerateCoin(Vector3 startPos){
		GameObject coin = GameObject.Instantiate(CoinPrefab);

		Vector3 coinPos = new Vector3(Random.Range(coinPosMinX, coinPosMaxX), 5, Random.Range(startPos.z - RoadPrefab.transform.localScale.z/2, startPos.z + RoadPrefab.transform.localScale.z/2));
		coin.transform.position = coinPos;

		return coin;
	}

}
