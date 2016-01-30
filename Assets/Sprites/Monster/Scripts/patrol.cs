using UnityEngine;
using System.Collections;

public class patrol : MonoBehaviour {
	public int maxCount;
	public float speed;
	public enum DIRECTION
	{
		LEFT = -1,
		RIGHT = 1
	};
	public DIRECTION Dir;
	private int _dir;
	private int count = 0;
	private int layerMask = 1 << 8;
	// Use this for initialization
	void Start () {
		_dir = (int)Dir;
	}
	// Update is called once per frame
	void Update () {
	}
	void FixedUpdate() {
		if (count >= 100 && count <= maxCount) {
			Debug.Log ("Sleeping");
			GetComponent<Animator> ().enabled = false;
			count++;
			if (count == maxCount) {
				count = 0;
			}
		} else {
			GetComponent<Animator> ().enabled = true;
			count++;
			CheckForHoles ();
			transform.Translate (_dir * Vector3.right * Time.deltaTime * speed);
		}
	}
	void ChangeDirection () {
		if (_dir == (int)DIRECTION.RIGHT)
			_dir = (int)DIRECTION.LEFT;
		else
			_dir = (int)DIRECTION.RIGHT;
		Debug.Log ("Direction:" + _dir);
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		Debug.Log(col.gameObject.tag);
		if (col.gameObject.tag == "Wall")
			ChangeDirection ();
	}
	void CheckForHoles()
	{
		Vector2 raycastOrigin = new Vector2 (transform.position.x + _dir, transform.position.y);
		RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, -Vector2.up, Mathf.Infinity, layerMask);
		Debug.DrawRay(raycastOrigin, -Vector2.up, Color.red);
		if (!hit)
		{
			ChangeDirection ();
		}
	}
}
