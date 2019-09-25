using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	public float speed = 75f;
	public float maxSpeed = 3f;
	public bool grounded;
	public float jumpPower = 7f;
	public AudioClip jumpClip;
	public AudioClip dieClip;
	public AudioClip pointClip;
	public AudioClip hitClip;
	public Canvas canvas;
	
	private Rigidbody2D rb2d;
	private Animator anim;
	private SpriteRenderer spr;
	private CircleCollider2D ccc2d;
	private CircleCollider2D cc2d;
	private AudioSource fxPlayer;
	private bool canJump;
	private bool doubleJump;
	private bool canMove = true;
	private float health;
	private float scale;
    private	int direction = 1;

	//private GameState gameState;

    // Start is called before the first frame update
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        cc2d = GetComponent<CircleCollider2D>();
        ccc2d = transform.Find("PlayerCollider").GetComponent<CircleCollider2D>();
        fxPlayer = GetComponent<AudioSource>();
        health = canvas.GetComponent<GameController>().health;
        //gameState = canvas.GetComponent<GameState>();
    }

    // Update is called once per frame
    void Update() {
		anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
		anim.SetBool("Grounded", grounded);
		if (grounded) {
			doubleJump = true;
		}
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) {
			if(grounded && canMove) {
				fxPlayer.clip = jumpClip;
				fxPlayer.Play();
				canJump = true;
				doubleJump = true;
			} else if (doubleJump) {
				fxPlayer.clip = jumpClip;
				fxPlayer.Play();
				canJump = true;
				doubleJump = false;
			}
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			scale = 0.8f;
		}
		else {
			scale = 1f;	
		}
    }
    void FixedUpdate() {
    	Vector3 fixedVelocity = rb2d.velocity;
    	fixedVelocity.x *= 0.75f;
    	float h = Input.GetAxis("Horizontal");
    	if (!canMove) h = 0;
    	rb2d.AddForce(Vector2.right * speed * h);
    	float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
		rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);
		if (grounded) {
			rb2d.velocity = fixedVelocity;
		}
		if (h > 0.01f) {
			direction = 1;
			//transform.localScale = new Vector3(scale, scale, 1f);
		} else if (h < -0.01f) {
			direction = -1;
			//transform.localScale = new Vector3(-scale, scale, 1f);
		}
		transform.localScale = new Vector3(direction, scale, 1f);
		if (scale != 1) {
			cc2d.radius = 0.28f;
			ccc2d.radius = 0.256f;
		} else {
			cc2d.radius = 0.35f;
			ccc2d.radius = 0.32f;
		}
		if (canJump) {
			rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
			rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
			canJump = false;
		}
    }
    void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Floor") {
			DisableMovement();
			canvas.SendMessage("GameEnded");
			doubleJump = false;
		}

    }
    //void OnBecameInvisible() {
    //	transform.position = new Vector3(18, 0, 0);

    //	canvas.SendMessage("GameEnded");
    	//transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    //}
    public void EnemyJump() {
    	canJump = true;
    	fxPlayer.clip = pointClip;
    	fxPlayer.Play();
    }
    public void EnemyPunch(float enemyPosX) {
    	//if 2 lifes remaining
    	if (health > 0) {
    		fxPlayer.clip = hitClip;
    		fxPlayer.Play();
    		canJump = true;
	    	float side = Mathf.Sign(enemyPosX - transform.position.x);
	    	rb2d.AddForce(Vector2.left * side * jumpPower, ForceMode2D.Impulse);
	    	DisableMovement();
			Invoke("EnableMovement", 0.2f);
    	} else {
    		canvas.SendMessage("GameEnded");
    		float side = Mathf.Sign(enemyPosX - transform.position.x);
	    	//rb2d.AddForce(Vector2.left * side * jumpPower, ForceMode2D.Impulse);

	    	//gameState = this.Ended;
	    	Invoke("DisableColliders", 0.2f);
	    	DisableMovement();
    	}
    	health --;
    	
    	Debug.Log("health: " + health);
    	
    }
    void EnableMovement() {
    	canMove = true;
    	spr.color = Color.white;
    }
    void DisableMovement() {
    	canMove = false;
    	spr.color = Color.red;
    }
    void DisableColliders() {
    	cc2d.enabled = false;
    	ccc2d.enabled = false;
    }
}
