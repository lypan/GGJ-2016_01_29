using UnityEngine;
using System.Collections;

public class health : MonoBehaviour {
	public int hp;
	private Animator _animator;
	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (hp == 0) {
			GetComponent<Animator> ().enabled = true;
			_animator.SetTrigger ("die");
			StartCoroutine (KillOnAnimationEnd ());
		}
	}
	private IEnumerator KillOnAnimationEnd() {
		yield return new WaitForSeconds (0.3f);
		Destroy (gameObject);
	}
	public void Damage(){
		hp--;
	}
}
