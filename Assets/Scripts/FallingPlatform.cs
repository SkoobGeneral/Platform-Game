using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {
	public float fallDelay = 0.5f;
	public float respawnDelay = 3f;
	private Rigidbody2D rb2d;
	private PolygonCollider2D pc2d;
	private Vector3 start;
    // Start is called before the first frame update
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        pc2d = GetComponent<PolygonCollider2D>();
        start = transform.position;
    }
    // Update is called once per frame
    void Update() {
        
    }
    void OnCollisionEnter2D(Collision2D col) {
    	if (col.gameObject.CompareTag("Player")) {
    		Invoke("FallPlatform", fallDelay);
    		Invoke("RespawnPlatform", fallDelay + respawnDelay);
    	}
    }
    void FallPlatform() {
    	rb2d.isKinematic = false;
		pc2d.isTrigger = true;
    }
    void RespawnPlatform() {
    	transform.position = start;
    	rb2d.isKinematic = true;
    	rb2d.velocity = Vector3.zero;
		pc2d.isTrigger = false;
    }
}
