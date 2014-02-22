using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class makedeck : MonoBehaviour {
	public Texture basedeck;
	public Texture rectt;
	private Camera _camera;
	float screenWidth;
	float screenHeight;
	int ind=0;
	int page=0;
	int money=0;
	int nowdeck=0;
	int cardnumber=0;
	int changecard=0;
	string mode="see";
	int[] deckcnumber=new int[6];
	card[] cardbox=new card[100];
	card[,] deck = new card[6, 6];
	string tstr;
	Object tobj;
	card tcard;
	public GameObject mcard;
	Texturemanager tmanager;
	// Use this for initialization
	void Start () {
		tmanager=GameObject.Find("Tmanager").GetComponent<Texturemanager>();
		_camera = Camera.main;
		screenWidth = _camera.pixelWidth;
		screenHeight = _camera.pixelHeight;
		StreamReader sw= new StreamReader("battleinfo\\playerinfo.txt");
		money = int.Parse(sw.ReadLine ());
		nowdeck = int.Parse(sw.ReadLine ());
		nowdeck--;
		sw.Close ();
		sw= new StreamReader("battleinfo\\box.txt");
		cardnumber=int.Parse(sw.ReadLine());
		while (ind<cardnumber) {
			try{tstr=sw.ReadLine();
				tobj=Instantiate (mcard,
				                  mcard.transform.position,
				                  this.gameObject.transform.localRotation);
				tobj.name="tcard"+ind.ToString();
				tcard = GameObject.Find("tcard"+ind.ToString()).GetComponent<card>();
				tcard.assigncard(tstr);
				tcard.updateTexture(tmanager);
				cardbox[ind]=tcard;
				ind++;
			}
			catch{break;}
		}
		sw.Close ();
		for (int i=1; i<7; i++) {
			StreamReader sw2 = new StreamReader ("battleinfo\\deck" + i.ToString () + ".txt");
			ind=0;
			deckcnumber[i-1]=int.Parse(sw2.ReadLine());
			while (ind<deckcnumber[i-1]) {
				try {
					tstr = sw2.ReadLine ();
					tobj = Instantiate (mcard,
					                    mcard.transform.position,
					                    this.gameObject.transform.localRotation);
					tobj.name = "tcard" + (ind + 100 * i).ToString ();
					tcard = GameObject.Find ("tcard" + (ind + 100 * i).ToString ()).GetComponent<card> ();
					tcard.assigncard (tstr);
					tcard.updateTexture (tmanager);
					deck [i - 1, ind] = tcard;
					ind++;
				} catch {
					break;
				}
			}
			sw2.Close ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI()
	{
		
		GUIStyle mgui = new GUIStyle ();
		mgui.fontSize = 50;
		switch (nowdeck) {
		case 0 :{
			GUI.color = Color.Lerp(Color.blue,Color.white,(float)0.5);
			break;
		}
		case 1 :{
			GUI.color = Color.Lerp(Color.red,Color.white,(float)0.5);
			break;
		}
		case 2 :{
			GUI.color = Color.Lerp(Color.green,Color.white,(float)0.5);
			break;
		}
		case 3 :{
			GUI.color = Color.Lerp(Color.magenta,Color.white,(float)0.5);
			break;
		}
		case 4 :{
			GUI.color = Color.Lerp(Color.cyan,Color.white,(float)0.5);
			break;
		}
		case 5 :{
			GUI.color = Color.Lerp(Color.yellow,Color.white,(float)0.5);
			break;
		}
				}
		for (int i=0; i<6; i++) {
			if (GUI.Button (getRect (i/6.0, 0, 0.165, 0.078), "")) {
				nowdeck = i;
				StreamWriter sw3= new StreamWriter("battleinfo\\playerinfo.txt");
				sw3.WriteLine (money.ToString());
				sw3.WriteLine ((nowdeck+1).ToString ());
				sw3.Close ();
			}
		}
		GUI.DrawTexture (getRect(0,0,1,1),basedeck);
		GUI.color=Color.Lerp(Color.black,GUI.color,(float)0.5);
		GUI.DrawTexture (getRect (0.029+0.056*page, 0.885, 0.05, 0.028),rectt);
		GUI.color = Color.Lerp(Color.blue,Color.white,(float)0.5);
		GUI.DrawTexture (getRect(0,0,0.165,0.078),rectt);
		GUI.color = Color.Lerp(Color.red,Color.white,(float)0.5);
		GUI.DrawTexture (getRect(0.166,0,0.165,0.078),rectt);
		GUI.color = Color.Lerp(Color.green,Color.white,(float)0.5);
		GUI.DrawTexture (getRect(0.333,0,0.165,0.078),rectt);
		GUI.color = Color.Lerp(Color.magenta,Color.white,(float)0.5);
		GUI.DrawTexture (getRect(0.5,0,0.165,0.078),rectt);
		GUI.color = Color.Lerp(Color.cyan,Color.white,(float)0.5);
		GUI.DrawTexture (getRect(0.666,0,0.165,0.078),rectt);
		GUI.color = Color.Lerp(Color.yellow,Color.white,(float)0.5);
		GUI.DrawTexture (getRect(0.833,0,0.165,0.078),rectt);
		GUI.color = Color.white;
		for (int i=0; i<6; i++) {
			GUI.Label (getRect(0.065+i/6.0,0.005,0.165,0.078),(i+1).ToString(),mgui);
		}
		for (int i=0; i<6; i++) {
			if(page*6+i<cardnumber){
				if(GUI.Button (getRect (0.003+i/6.0,0.637,0.16,0.25), "")){
					if(mode=="see")
						mode="change";
					else
						mode="see";
					changecard=i;
				}
				if(mode=="change"&&i!=changecard)
					GUI.color=Color.gray;
				cardbox[page*6+i].drawCard(0.003+i/6.0,0.637,0.16,0.25,"front");
				GUI.color=Color.white;
			}
				}
		for (int i=0; i<6; i++) {
			if(GUI.Button (getRect (0.003+i/6.0,0.08,0.16,0.27), "")){
				if(mode=="change"&&summontest(i,cardbox[page*6+changecard])){
					deck[nowdeck,i]=cardbox[page*6+changecard];
					if (i>=deckcnumber[nowdeck]){
						deck[nowdeck,deckcnumber[nowdeck]]=cardbox[page*6+changecard];
						deckcnumber[nowdeck]++;
					}
					mode="see";
					continue;
				}
				if(mode=="see")
					deletecard(i);
			}
			if(i<deckcnumber[nowdeck]){
				deck[nowdeck,i].drawCard(0.003+i/6.0,0.08,0.16,0.27,"front");
			}
		}
		if (GUI.Button (getRect (0, 0.882, 0.029, 0.031), "")) {
			page--;
			mode="see";
			if(page<0){
				page=16;
			}
				}
		if (GUI.Button (getRect (0.972, 0.882, 0.029, 0.031), "")) {
			page++;
			mode="see";
			if(page>16){
				page=0;
			}
		}
		if (GUI.Button (getRect(0.257,0.935,0.125,0.048), "")) {
		}
		if (GUI.Button (getRect(0.443,0.935,0.125,0.048), "")) {
			deckcnumber [nowdeck]=0;
		}
		if (GUI.Button (getRect(0.628,0.935,0.125,0.048), "")) {
			savedata();
		}
		if (GUI.Button (getRect(0.812,0.935,0.125,0.048), "")) {
			Application.LoadLevel("mainscreen");
		}
	}
	void deletecard(int position){
		if (position >= deckcnumber [nowdeck])
			return;
		else {
			for(int i=position;i<deckcnumber [nowdeck]-1;i++)
				deck[nowdeck,i]=deck[nowdeck,i+1];
			deckcnumber [nowdeck]--;
		}
		}
	bool summontest(int position,card scard){
		for (int i=0; i<6; i++) {
			if(i!=position&&i<deckcnumber [nowdeck]&&scard.cardno==deck[nowdeck,i].cardno&&scard.q==deck[nowdeck,i].q)
				return false;
				}
		return true;
		}
	void savedata(){
		StreamWriter sw= new StreamWriter("battleinfo\\box.txt");
		sw.WriteLine (cardnumber.ToString());
		for (int i=0; i<cardnumber; i++) {
			sw.WriteLine(cardbox[i].q+cardbox[i].cardno.ToString());
				}
		sw.Close ();
		for (int i=1; i<7; i++) {
			StreamWriter sw2 = new StreamWriter ("battleinfo\\deck" + i.ToString () + ".txt");
			sw2.WriteLine (deckcnumber[i-1].ToString());
			for (int j=0; j<deckcnumber[i-1]; j++) {
				sw2.WriteLine(deck[i-1,j].q+deck[i-1,j].cardno.ToString());
			}
			sw2.Close ();
		}
	}
	Rect getRect(double x, double y, double w,double h){
		return new Rect ((float)x * screenWidth, (float)y * screenHeight, (float)w * screenWidth, (float)h * screenHeight);
	}
}
