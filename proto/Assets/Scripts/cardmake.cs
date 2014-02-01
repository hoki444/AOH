using UnityEngine;
using System.Collections;
using System.IO;

public class cardmake : MonoBehaviour {
	public Texture mtexture;
	public Texture mtexture2;
	public Texture skill;
	public GameObject makerange;
	int state=0;
	string cardno="1";
	string cardname="이름";
	string cost="0";
	string level="1";
	string q="F";
	string life="1";
	string hp="5";
	string atk="15";
	skill[] mskill = new skill[4];
	attack[] mattack= new attack[4];
	bool save = false;
	FileStream fs;
	private Camera _camera;
	float screenWidth;
	float screenHeight;
	// Use this for initialization
	void Start () {
		_camera = Camera.main;
		screenWidth = _camera.pixelWidth;
		screenHeight = _camera.pixelHeight;
		for (int i=0; i<4; i++) {
			mattack[i]=new attack();
			mskill[i]=new skill("0",1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (save) {
			try{
				fs=File.Create("cardinfo\\"+q+"\\"+cardname+level+cardno+".txt");
			}
			catch{
				DirectoryInfo md= new DirectoryInfo("cardinfo\\"+q+"\\");
				md.Create();
				fs=File.Create("cardinfo\\"+q+"\\"+cardname+level+cardno+".txt");
			}

			fs.Close();
			StreamWriter sw= new StreamWriter("cardinfo\\"+q+"\\"+cardname+level+cardno+".txt");
			sw.WriteLine(cardname);
			sw.WriteLine(q);
			sw.WriteLine(level);
			sw.WriteLine(cardno);
			sw.WriteLine(cost);
			sw.WriteLine(life);
			sw.WriteLine(hp);
			sw.WriteLine(atk);
			sw.Close();
			save=false;
		}
	}
	void OnGUI()
	{
		if (state==0) {
			GUI.DrawTexture(getRect(0.2,0.1,0.6,0.8),mtexture);
			cardno = GUI.TextField (getRect(0.3,0.03,0.2,0.04), cardno);
			q = GUI.TextField (getRect(0.05,0.3,0.1,0.04), q);
			cost = GUI.TextField (getRect(0.68,0.16,0.06,0.04), cost);
			level = GUI.TextField (getRect(0.25,0.16,0.06,0.04), level);
			cardname = GUI.TextField (getRect(0.4,0.16,0.2,0.04), cardname);
			life = GUI.TextField (getRect(0.33,0.65,0.08,0.04), life);
			hp = GUI.TextField (getRect(0.48,0.65,0.08,0.04), hp);
			atk = GUI.TextField (getRect(0.63,0.65,0.08,0.04), atk);
			if (GUI.Button (getRect(0.7,0.92,0.2,0.04), "save!!")) {
				save=true;
			}
			if (GUI.Button (getRect(0.82,0.3,0.16,0.2), "make attack")) {
				state=2;
			}
			if (GUI.Button (getRect(0.82,0.6,0.16,0.2), "make skill")) {
				state=1;
			}
			if (GUI.Button (getRect(0.02,0.6,0.16,0.2), "return")) {
				Application.LoadLevel("mainscreen");
			}
		}
		if (state == 1) {
			if (GUI.Button (getRect(0.455,0.20,0.06,0.12), "")) {
				state=10;
			}
			if (GUI.Button (getRect(0.455,0.35,0.06,0.12), "")) {
				state=11;
			}
			if (GUI.Button (getRect(0.455,0.5,0.06,0.12), "")) {
				state=12;
			}
			if (GUI.Button (getRect(0.455,0.65,0.06,0.12), "")) {
				state=13;
			}
			GUI.DrawTexture(getRect(0.2,0.1,0.6,0.8),skill);
			for (int i=0; i<4; i++) {
				mskill[i].setLevel(int.Parse(GUI.TextField (getRect(0.6,0.24+(double)i*0.15,0.15,0.04), mskill[i].getLevel().ToString())));
			}

		}
		if (state == 2) {
			GUI.DrawTexture(getRect(0.2,0.1,0.6,0.8),mtexture2);

				}
	}
	Rect getRect(double x, double y, double w,double h){
		return new Rect ((float)x * screenWidth, (float)y * screenHeight, (float)w * screenWidth, (float)h * screenHeight);
	}
}
