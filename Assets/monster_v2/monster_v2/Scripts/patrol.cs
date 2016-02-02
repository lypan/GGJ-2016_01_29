using UnityEngine;
using System.Collections;

public class patrol : MonoBehaviour {
	public Sprite idle;
	public int maxCount;
	public float speed;

	public enum DIRECTION
	{
		LEFT = -1,
		RIGHT = 1
	};
	public DIRECTION Dir;
	public int dir;

	private int _count = 0;
	private int _layerMask;

	private Animator _animator;

	// Use this for initialization
	void Start () {
		dir = (int)Dir;
		_layerMask = 1 << LayerMask.NameToLayer("Platforms");
		_animator = GetComponent<Animator>();

	}
	// Update is called once per frame
	void Update () {
	}
	void FixedUpdate() {
		if (_count >= 100 && _count <= maxCount) {
			_animator.SetBool("idle", true);
			_animator.SetBool("walk", false);
			_count++;
			if (_count == maxCount) {
				_count = 0;
			}
		} else {
			_animator.SetBool("idle", false);
			_animator.SetBool("walk", true);
			_count++;
			CheckForHoles ();
			transform.Translate (dir * Vector3.right * Time.deltaTime * speed);
		}
	}
	void ChangeDirection () {
		if (dir == (int)DIRECTION.RIGHT)
			dir = (int)DIRECTION.LEFT;
		else
			dir = (int)DIRECTION.RIGHT;
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		Debug.Log(col.gameObject.tag);
		if (col.gameObject.tag == "Wall")
			ChangeDirection ();
	}
	void CheckForHoles()
	{
		Vector2 raycastOrigin = new Vector2 (transform.position.x + dir, transform.position.y);
		RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, -Vector2.up, 2, _layerMask);
		Debug.DrawRay(raycastOrigin, -Vector2.up * 10, Color.green);
		if (!hit)
        {
			ChangeDirection ();
		}
	}
}
