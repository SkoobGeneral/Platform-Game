using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	
	public float speed = 1f;
	public float maxSpeed = 1f;
	public Canvas canvas;

	private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        rb2d.AddForce(Vector2.right * speed);
        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
		rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);
		if (rb2d.velocity.x > -0.01f && rb2d.velocity.x < 0.01f) {
			speed = -speed;
			rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
		}
		if (speed < 0.1f) {
			transform.localScale = new Vector3(1f, 1f, 1f);
		} else {
			transform.localScale = new Vector3(-1f, 1f, 1f);
		}
    }
    void OnTriggerEnter2D(Collider2D col) {
    	if (col.gameObject.tag == "Player") {
    		if (transform.position.y < col.transform.position.y) {
    			canvas.SendMessage("IncreasePoints");
    			col.SendMessage("EnemyJump");
    			Destroy(gameObject);
    		} else {
    			col.SendMessage("EnemyPunch", transform.position.x);
    		}
    	}
    }
}
