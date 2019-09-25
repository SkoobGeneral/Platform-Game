using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour {
	public GameObject follow;
    public GameObject background;
    public GameObject scoreText;
    public GameObject bestText;
	public Vector2 minCamPos, maxCamPos;
	public float smoothTime;
	private Vector2 velocity;
    private RawImage ri;
    public float parallaxSpeed = 0.01f;
    // Start is called before the first frame update
    void Start() {
        ri = background.GetComponent<RawImage>();
    }
    // Update is called once per frame
    void FixedUpdate() {
        float posX = Mathf.SmoothDamp(transform.position.x, follow.transform.position.x, ref velocity.x, smoothTime);
        float posY = Mathf.SmoothDamp(transform.position.y, follow.transform.position.y, ref velocity.y, smoothTime);
        transform.position = new Vector3(Mathf.Clamp(posX, minCamPos.x, maxCamPos.x), Mathf.Clamp(posY, minCamPos.y, maxCamPos.y), transform.position.z);
        ri.uvRect = new Rect(posX * parallaxSpeed, 0f, 1f, 1f);
        scoreText.transform.position = new Vector3(posX, scoreText.transform.position.y, scoreText.transform.position.z);
        bestText.transform.position = new Vector3(posX, bestText.transform.position.y, bestText.transform.position.z);
    }
}
