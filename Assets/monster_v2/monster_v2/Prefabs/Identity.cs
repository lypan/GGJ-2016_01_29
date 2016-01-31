using UnityEngine;
using System.Collections;

public class Identity : MonoBehaviour {

	// Use this for initialization
    [SerializeField] bool isBlack;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool IsBlack()
    {
        return isBlack;
    }
}
