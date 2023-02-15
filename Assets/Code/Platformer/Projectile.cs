using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer {
    public class Projectile : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.velocity = transform.right * 10f;
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(gameObject);
        }

    }
}

