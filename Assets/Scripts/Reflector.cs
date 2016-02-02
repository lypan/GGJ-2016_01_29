using UnityEngine;
using System.Collections;

[RequireComponent(typeof(gameLight))]
[RequireComponent(typeof(Collider2D))]
public class Reflector : MonoBehaviour {

    public gameLight outcomeLight;
    public bool enableReflect = true;

	// Use this for initialization
	void Start () {
        outcomeLight = GetComponent<gameLight>();
        outcomeLight.SetEnable(false);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}


    public void TriggerReflect(gameLight input, Vector3 originA, Vector3 originB, Vector3 inNomral)
    {
        Debug.Log("Trigger Mirror!");
        outcomeLight.lightOriginRight = originA - outcomeLight.lightProbe.transform.position;
        outcomeLight.lightOriginLeft = originB - outcomeLight.lightProbe.transform.position;
        Vector3 newDir = Vector3.Reflect(input.lightDirection, inNomral);
        outcomeLight.ChangeLightDirection(newDir);
        outcomeLight.SetEnable(true);
        outcomeLight.useWidth = false;
    }

    public void LeaveReflect()
    {
        outcomeLight.SetEnable(false);
    }
}
