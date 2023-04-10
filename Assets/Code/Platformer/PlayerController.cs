using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController instance;

        public bool isPaused;
        // Start is called before the first frame update
        Rigidbody2D _rigidBody2D;
        public Transform aimPivot;
        public GameObject projectilePrefab;
        public int jumpsLeft;
        public int score;
		SpriteRenderer sprite;
		Animator animator;
        public TMP_Text scoreUI;

        void Awake()
        {
            instance = this;
        }
        void Start()
        {
            _rigidBody2D = GetComponent<Rigidbody2D>();
			sprite = GetComponent<SpriteRenderer>();
			animator = GetComponent<Animator>();
            score = PlayerPrefs.GetInt("Score");
        }

		void FixedUpdate(){
			animator.SetFloat("Speed", _rigidBody2D.velocity.magnitude);
			if(_rigidBody2D.velocity.magnitude > 0){
				animator.speed = _rigidBody2D.velocity.magnitude/3f;
			} else {
				animator.speed = 1f;
			}
		}

        public void ResetScore()
        {
            score = 0;
            PlayerPrefs.DeleteKey("Score");
        }

        // Update is called once per frame
        void Update()
        {
            scoreUI.text = score.ToString();
            if (isPaused)
            {
                return;
            }
            if (Input.GetKey(KeyCode.A))
            {
                _rigidBody2D.AddForce(Vector2.left * 18f * Time.deltaTime, ForceMode2D.Impulse);
				sprite.flipX = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _rigidBody2D.AddForce(Vector2.right * 18f * Time.deltaTime, ForceMode2D.Impulse);
				sprite.flipX = false;
            }

            Vector3 mousePosition = Input.mousePosition;
            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

            float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
            float angleToMouse = radiansToMouse * Mathf.Rad2Deg;
            aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);

            if (Input.GetMouseButtonDown(0))
            {
                GameObject newProjectile = Instantiate(projectilePrefab);
                newProjectile.transform.position = transform.position;
                newProjectile.transform.rotation = aimPivot.rotation;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (jumpsLeft > 0)
                {
                    jumpsLeft--;
                    _rigidBody2D.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MenuController.instance.Show();
            }
            
			animator.SetInteger("JumpsLeft", jumpsLeft);
        }

        void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 0.85f);
                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit2D hit = hits[i];
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        jumpsLeft = 2;
                    }
                }
            }
        }
        
    }
}

