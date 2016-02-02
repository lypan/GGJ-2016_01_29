using UnityEngine;
using System.Collections;

public class attack : MonoBehaviour {
	public GameObject target;
	public GameObject bullet;
	public float speed;
	public int maxCount;
	public float attackDistance;
	private int _dir;
	private bool _face;
	private int _layerMask;
	private bool _hasFired;
	private bool _canFire;
	private int _count;

	// Use this for initialization
	void Start () {
		_hasFired = false;
		_canFire = true;
		_count = 0;
	}

	// Update is called once per frame
	void Update () {
		if (_hasFired)
			_count++;
		if (_count == maxCount) {
			Debug.Log (_count);
			_count = 0;
			_canFire = true;
			_hasFired = false;
		}

		_dir = GetComponent<patrol> ().dir;
		_layerMask = 1 << LayerMask.NameToLayer("Player");
		CheckFace ();
	}
	void CheckFace () {
		_face = (_dir * (target.transform.position.x - transform.position.x)) > 0 ? true : false;
		if (_face && (Mathf.Abs(target.transform.position.x - transform.position.x) < attackDistance)) {
			Vector2 raycastOrigin = new Vector2 (transform.position.x, transform.position.y + Mathf.Abs(_dir));
			RaycastHit2D hit = Physics2D.Raycast (raycastOrigin, _dir * Vector2.right, Mathf.Infinity, _layerMask);
		    Debug.DrawRay (raycastOrigin, _dir * Vector2.right * 10, Color.red);
			if (hit&& _canFire) {
				Debug.Log ("canFire:" + _canFire);
				GameObject ibullet = Instantiate (bullet, new Vector3(transform.position.x + _dir, transform.position.y + Mathf.Abs(_dir), 0), transform.rotation) as GameObject;
				ibullet.GetComponent<Rigidbody2D> ().velocity = _dir * Vector2.right * 10;
				_hasFired = true;
				_canFire = false;
			}
		}
	}

}
