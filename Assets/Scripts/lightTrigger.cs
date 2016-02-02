using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class lightTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			Debug.Log ("light:: light hit player");
			other.gameObject.GetComponent<PlatformerCharacter2D> ().SetInLight (true);
		} else if (other.tag == "Monster")
			Debug.Log ("light:: light hit monster");
		else if (other.tag == "Ritual") {
			other.gameObject.GetComponent<FinalRitual> ().inLight = true;
		}
		else
			Debug.Log("light:: light hit");
	}


	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Debug.Log("light:: light leave player");
			other.gameObject.GetComponent<PlatformerCharacter2D>().SetInLight(false);
		}
		else if (other.tag == "Monster")
			Debug.Log("light:: light leave monster");
		else if (other.tag == "Ritual") {
			other.gameObject.GetComponent<FinalRitual> ().inLight = false;
		}
		else
			Debug.Log("light:: light leave");
	}
}
