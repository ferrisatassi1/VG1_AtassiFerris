using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpaceShooter {

	public class GameController : MonoBehaviour
	{
		public static GameController instance;
		public Transform[] spawnPoints;
		public GameObject[] asteroidPrefabs;
		public GameObject explosionPrefab;
		public TMP_Text textScore;
		public TMP_Text textMoney;
		public TMP_Text missileSpeedUpgradeText;

		public TMP_Text bonusUpgradeText;

		public float maxAsteroidDelay = 2f;
		public float minAsteroidDelay = 0.2f;

		public float timeElapsed;	
		public float asteroidDelay;
		public int score;
		public int money;
		public float missileSpeed = 2f;

		public float firingDelay = 1f;
		public float bonusMultiplyer = 1f;

    	// Start is called before the first frame update
    	void Awake(){
			instance = this;
		}

		void Update(){
			timeElapsed += Time.deltaTime;

			float decreaseDelayOverTime = maxAsteroidDelay - ((maxAsteroidDelay - minAsteroidDelay)/30f * timeElapsed);
			asteroidDelay = Mathf.Clamp(decreaseDelayOverTime, minAsteroidDelay, maxAsteroidDelay);
			
			UpdateDisplay();
		}

		void UpdateDisplay()
		{
			textScore.text = score.ToString();
			textMoney.text = money.ToString();
		}
		public void UpgradeMissileSpeed()
		{
			int cost = 100 + Mathf.RoundToInt((1f - firingDelay) * 100f);
			if (GameController.instance.money >= cost)
			{
				GameController.instance.money -= cost;
				firingDelay -= 0.05f;
				int newCost = 100 + Mathf.RoundToInt((1f - firingDelay) * 100f);
				missileSpeedUpgradeText.text = "Fire Speed $" + newCost;
			}
		}

		public void EarnPoints(int pointAmount)
		{
			score += Mathf.RoundToInt(pointAmount * bonusMultiplyer);
			money += Mathf.RoundToInt(pointAmount * bonusMultiplyer);
		}

		public void UpgradeBonus()
		{
			int cost = Mathf.RoundToInt(100 * bonusMultiplyer);
			if (cost <= money)
			{
				money -= cost;
				bonusMultiplyer += 1f;
				bonusUpgradeText.text = "Multiplyer $" + Mathf.RoundToInt(100 * bonusMultiplyer);
			}
		}

		void SpawnAsteroid(){
			int randomSpawnIndex = UnityEngine.Random.Range(0,spawnPoints.Length);
			Transform randomSpawnPoints = spawnPoints[randomSpawnIndex];
			int randomAsteroidIndex = UnityEngine.Random.Range(0, asteroidPrefabs.Length);
			GameObject randomAsteroidPrefab = asteroidPrefabs[randomAsteroidIndex];
			Vector3 position = new Vector3(randomSpawnPoints.position.x,randomSpawnPoints.position.y,0);
			Instantiate(randomAsteroidPrefab, position , Quaternion.identity);
		}

		void Start(){
			StartCoroutine("AsteroidSpawnTimer");
			score = 0;
			money = 0;
		}	

		IEnumerator AsteroidSpawnTimer(){
			yield return new WaitForSeconds(asteroidDelay);
			SpawnAsteroid();
			StartCoroutine("AsteroidSpawnTimer");
		}	

	}

}
