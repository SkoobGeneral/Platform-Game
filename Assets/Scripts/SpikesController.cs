using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D col) {
    	Debug.Log("collision: " + col.gameObject.tag);
    	if (col.gameObject.tag == "Player") {
			col.gameObject.SendMessage("EnemyPunch", transform.position.x);
    	}
    }
}