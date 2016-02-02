using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadCredit : MonoBehaviour {
//	public Image UICreditIm;
	public GameObject creditImage;
	private bool creditEnable = false;
	public GameObject loadingImage;
	// Use this for initialization
	void Start (){
		
	}
	public void LoadCreditImage () {
//		UICreditIm.enabled = false;
		creditEnable ^= true;
		creditImage.SetActive(creditEnable);

	}

	public void ExitGame()
	{
		Debug.Log ("Exit Gm");
		Application.Quit ();
	}

	public void LoadScene(int level){
		creditImage.SetActive(false);
		loadingImage.SetActive(true);
		Application.LoadLevel (level);
	}
		

	
	// Update is called once per frame
	void Update () {
	}
	

//	void LoadCreditImage (){
//		UICreditIm.enabled = true;
//	}
	
}
