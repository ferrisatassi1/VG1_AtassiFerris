using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Ship : MonoBehaviour
    {
        public GameObject projectilePrefab;

        public float firingDelay = 1f;
        void Update()
        {
            float yPosition = Mathf.Sin(GameController.instance.timeElapsed) * 3f;
            transform.position = new Vector2(0, yPosition);
        }

        void FireProjectile()
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }

        IEnumerator FiringTimer()
        {
            yield return new WaitForSeconds(firingDelay);
            FireProjectile();
            StartCoroutine("FiringTimer");
        }

        void Start()
        {
            StartCoroutine("FiringTimer");
        }
        
    }
}
