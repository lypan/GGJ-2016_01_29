using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;


public class FinalRitual : MonoBehaviour {

	public bool isShadowRitual = true;
	public bool inLight = false;
	public bool activated
	{
		get
		{ 
			return (isShadowRitual && !inLight) || (isShadowRitual && !inLight);
		}
	}

    public SpriteRenderer magicSprite;

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Debug.Log("ritual:: ritual hit player");
			if (activated) {
				//gameover
				GameOverManager.GameOver();
			}
				
		}
		else
			Debug.Log("ritual:: ritual hit");
	}

	// Use this for initialization
	void Awake () {
        magicSprite = gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (activated) magicSprite.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        else magicSprite.color = new Color(0.2f, 0.2f, 0.2f, 1.0f);
    }
}
