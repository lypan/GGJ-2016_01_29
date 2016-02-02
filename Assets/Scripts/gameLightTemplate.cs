using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets._2D;

public class gameLightTemplate : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("light:: light hit player");
            other.gameObject.GetComponent<PlatformerCharacter2D>().SetInLight(true);
        }
        else if (other.tag == "Monster")
            Debug.Log("light:: light hit monster");
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
    }
} 