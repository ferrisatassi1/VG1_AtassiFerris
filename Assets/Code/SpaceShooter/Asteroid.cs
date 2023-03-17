using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Asteroid : MonoBehaviour
    {
        Rigidbody2D _rb;

        float randomSpeed;

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            randomSpeed =  UnityEngine.Random.Range(0.5f, 3f);
        }
        void Update()
        {
            _rb.velocity = Vector2.left * randomSpeed;
        }

        void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}