using UnityEngine;

using System.Collections;

public class GameOverManager : MonoBehaviour {
	public static GameOverManager gom;
	public static GameOverManager instance
	{
		get
		{
			if (!gom)
			{
				gom = FindObjectOfType(typeof(GameOverManager)) as GameOverManager;

				if (!gom)
				{
					Debug.LogError("userInputManager           ::There needs to be one active GameManager script on a GameObject in your scene.");
				}
			}
			return gom;
		}
	}

	public GameObject loadingImage;

	public static void GameOver()
	{
		instance.loadingImage.SetActive (true);
		Application.LoadLevel (0);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
