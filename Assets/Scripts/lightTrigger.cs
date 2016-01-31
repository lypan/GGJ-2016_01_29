using UnityEngine;
using System.Collections;

public class lightTrigger : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
		{
			Debug.Log("Light hit player!");
		}
		else if(other.tag == "Monster")
		{
			Debug.Log("Light hit player!");
		}
		else Debug.Log("Light hit obstacle");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
		{
			Debug.Log("Light hit player!");
		}
		else if(other.tag == "Monster")
		{
			Debug.Log("Light hit player!");
		}
		else Debug.Log("Light hit obstacle");
    }
}
