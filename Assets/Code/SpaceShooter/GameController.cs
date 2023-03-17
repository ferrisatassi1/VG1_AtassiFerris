using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {

	public class GameController : MonoBehaviour
	{
		public static GameController instance;
		public Transform[] spawnPoints;
		public GameObject[] asteroidPrefabs;
		public GameObject explosionPrefab;

		public float maxAsteroidDelay = 2f;
		public float minAsteroidDelay = 0.2f;

		public float timeElapsed;	
		public float asteroidDelay;
    	// Start is called before the first frame update
    	void Awake(){
			instance = this;
		}

		void Update(){
			timeElapsed += Time.deltaTime;

			float decreaseDelayOverTime = maxAsteroidDelay - ((maxAsteroidDelay - minAsteroidDelay)/30f * timeElapsed);
			asteroidDelay = Mathf.Clamp(decreaseDelayOverTime, minAsteroidDelay, maxAsteroidDelay);
		}

		void SpawnAsteroid(){
			int randomSpawnIndex = UnityEngine.Random.Range(0,spawnPoints.Length);
			Transform randomSpawnPoints = spawnPoints[randomSpawnIndex];
			int randomAsteroidIndex = UnityEngine.Random.Range(0, asteroidPrefabs.Length);
			GameObject randomAsteroidPrefab = asteroidPrefabs[randomAsteroidIndex];

			Instantiate(randomAsteroidPrefab, randomSpawnPoints.position, Quaternion.identity);
		}

		void Start(){
			StartCoroutine("AsteroidSpawnTimer");
		}	

		IEnumerator AsteroidSpawnTimer(){
			yield return new WaitForSeconds(asteroidDelay);
			SpawnAsteroid();
			StartCoroutine("AsteroidSpawnTimer");
		}	

	}

}
