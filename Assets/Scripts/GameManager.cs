using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.R))
			Restart ();
	
		if (Input.GetKey (KeyCode.Escape))
			Quit ();
	}

	void Restart() 
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	void Quit() 
	{
		Application.Quit ();
	}
}
