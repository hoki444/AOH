using UnityEngine;
using System.Collections;

public class background : MonoBehaviour {

	public Texture bground;
	private Camera _camera;
	float screenWidth;
	float screenHeight;
	// Use this for initialization
	void Start () {
		_camera = Camera.main;
		screenWidth = _camera.pixelWidth;
		screenHeight = _camera.pixelHeight;
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnGUI()
	{

		GUIStyle mgui = new GUIStyle ();
		mgui.fontSize = 50;
		GUI.DrawTexture (getRect(0,0,1,1),bground);
		GUI.Label (getRect(0.25,0.15,0.5,0.04), new GUIContent ("ARENA OF HEROES"), mgui);
		if (GUI.Button (getRect(0.4,0.38,0.2,0.04), "CARD EDITOR")) {
			Application.LoadLevel("cardmake");
		}
		if (GUI.Button (getRect(0.4,0.48,0.2,0.04), "GAME START")) {
			Application.LoadLevel("battle");
		}
		if (GUI.Button (getRect(0.4,0.58,0.2,0.04), "MAKE DECK")) {
			Application.LoadLevel("battle");
		}
		if (GUI.Button (getRect(0.4,0.68,0.2,0.04), "SHOP")) {
			Application.LoadLevel("battle");
		}
		if (GUI.Button (getRect(0.4,0.78,0.2,0.04), "EXIT")) {
			Application.Quit();
		}
	}
	Rect getRect(double x, double y, double w,double h){
		return new Rect ((float)x * screenWidth, (float)y * screenHeight, (float)w * screenWidth, (float)h * screenHeight);
		}
}
