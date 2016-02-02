using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;
public class bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D(Collision2D col)
	{
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlatformerCharacter2D>().OnDamage(gameObject.GetComponent<Rigidbody2D>().velocity);
            Destroy(gameObject);
        }
	}
}
