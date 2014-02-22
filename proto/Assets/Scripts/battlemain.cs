using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class battlemain : MonoBehaviour {
	AI ai;
	int turn=1;
	public GameObject mcard;
	int whoiswin=0;
	int tick;
	Object tobj;
	int money;
	int nowdeck;
	int summonposition;
	card[] pdeck=new card[6];
	card[] edeck=new card[6];
	card tcard;
	bool automode=false;
	int preturnnum=5;
	int ereturnnum=5;
	int pdeckcnumber=0;
	int edeckcnumber=0;
	bool ecard=false;
	bool updated=false;
	string phase="start";
	string tstr;
	string auto="자동으로";
	public Texture bfield;
	public Texture arrow;
	public Texture returnt;
	public Texture[] summont=new Texture[7];
	int ind=1;
	int gtime=0;
	private Camera _camera;
	float screenWidth;
	float screenHeight;
	Texturemanager tmanager;
	// Use this for initialization
	void Start () {
		ai = new AI ();
		for (int i=0; i<6; i++) {
			pdeck[i]=new card();
				}
		tmanager=GameObject.Find("Tmanager").GetComponent<Texturemanager>();
		_camera = Camera.main;
		screenWidth = _camera.pixelWidth;
		screenHeight = _camera.pixelHeight;
		StreamReader sw= new StreamReader("battleinfo\\playerinfo.txt");
		money = int.Parse(sw.ReadLine ());
		nowdeck = int.Parse(sw.ReadLine ());
		sw.Close ();
		sw= new StreamReader("battleinfo\\battle.txt");
		while (ind<14) {
			try{tstr=sw.ReadLine();
				if (tstr=="e"){
					ecard=true;
					ind++;
					tstr=sw.ReadLine();
				}
				if(ecard){
					tobj=Instantiate (mcard,
					                  mcard.transform.position,
					                  this.gameObject.transform.localRotation);
					tobj.name="tcard"+ind.ToString();
					tcard = GameObject.Find("tcard"+ind.ToString()).GetComponent<card>();
					tcard.assigncard(tstr);
					tcard.isfrontcard=false;
					tcard.ismycard=false;
					tcard.position0=13-ind;
					tcard.updateTexture(tmanager);
					edeck[13-ind]=tcard;
				}
				ind++;
			}
			catch{break;}
			}
		edeckcnumber = 6;//TODO : 적의 덱 수를 알 수 있으면 지울것
		sw.Close ();
		StreamReader sw2 = new StreamReader ("battleinfo\\deck" + nowdeck.ToString () + ".txt");
		ind=0;
		pdeckcnumber=int.Parse(sw2.ReadLine());
		while (ind<pdeckcnumber) {
			try {
				tstr = sw2.ReadLine ();
				tobj = Instantiate (mcard,
				                    mcard.transform.position,
				                    this.gameObject.transform.localRotation);
				tobj.name = "tcard" + (ind).ToString ();
				tcard = GameObject.Find ("tcard" + (ind).ToString ()).GetComponent<card> ();
				tcard.assigncard (tstr);
				tcard.position0=ind;
				tcard.updateTexture (tmanager);
				pdeck [ind] = tcard;
				ind++;
			} catch {
				break;
			}
		}
		sw2.Close ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!updated) {
						tmanager = GameObject.Find ("Tmanager").GetComponent<Texturemanager> ();
						tcard.updateTexture (tmanager);
			updated=true;
				}
		if (!automode) {
			switch (phase) {
			case("start"):
			{
				bool scheck = false;
				for (int index = 0; index < pdeckcnumber; index++) {
					card c = pdeck [index];
					if (c.state == "selected" || c.state == "sfield")
						scheck = true;
					else
						if (c.state == "deck") {
							c.state = "nselected";
						}
						else
							if (c.state == "field") {
								c.state = "nfield";
							}
				}
				if (!scheck) {
					for (int index = 0; index < pdeckcnumber; index++) {
						card c = pdeck [index];
						if (c.state == "nselected") {
							c.state = "deck";
						} else if (c.state == "nfield") {
							c.state = "field";
						}
					}
				} else {
					phase = "card";
				}
				break;
			}
			case("card"):
			{
				int scheck = 0;
				for (int index = 0; index < pdeckcnumber; index++) {
					card c = pdeck [index];
					if (c.state == "selected" || c.state == "sfield")
						scheck++;
					else if (c.state == "deck") {
						c.state = "nselected";
					} else if (c.state == "field") {
						c.state = "nfield";
					}
				}
				if (scheck != 1) {
					for (int index = 0; index < pdeckcnumber; index++) {
						card c = pdeck [index];
						if (c.state == "nselected" || c.state == "selected") {
							c.state = "deck";
						} else if (c.state == "nfield" || c.state == "sfield") {
							c.state = "field";
						}
					}
					phase = "start";
				}
				break;
			}
			}
		} else {
			tick++;
		}
	}
	void summonEvent(int position){
		for (int index = 0; index < pdeckcnumber; index++) {
			card c = pdeck [index];
			if(c.state=="selected"){
				c.onfield=true;
				c.position1=position;
				c.state="field";
			}
			else if(c.state=="sfield"&&preturnnum>0){
				c.onfield=true;
				c.position1=position;
				c.state="field";
				preturnnum--;
			}
		}
		ai.Align (pdeck,pdeckcnumber);
		}
	bool movetest(int position){
		for (int index = 0; index < pdeckcnumber; index++) {
			card c = pdeck [index];
			if(c.onfield&&c.position1==position){
				return false;
			}
		}
		for (int index = 0; index < edeckcnumber; index++) {
			card c = edeck [index];
			if(c.onfield&&c.position1==11-position){
				return false;
			}
		}
		return true;
	}
	void turnend(){
		for(int i=pdeckcnumber-1;i>=0;i--){
			if(pdeck[i].onfield&&movetest(pdeck[i].position1+1)){
				pdeck[i].position1++;
				if(pdeck[i].position1>=12){
					whoiswin=1;
					for (int index = 0; index < edeckcnumber; index++) {
						card c = edeck [index];
						money+=c.money;
					}
					StreamWriter sw3= new StreamWriter("battleinfo\\playerinfo.txt");
					sw3.WriteLine (money.ToString());
					sw3.WriteLine ((nowdeck).ToString ());
					sw3.Close ();
				}
			}
		}
		for (int index = 0; index < pdeckcnumber; index++) {
			card c = pdeck [index];
			if(c.onfield){
				c.attack(pdeck,edeck);
			}
		}
		ai.doAI (pdeck,edeck,pdeckcnumber,edeckcnumber);
		for(int i=edeckcnumber-1;i>=0;i--){
			if(edeck[i].onfield&&movetest(11-edeck[i].position1-1)){
				edeck[i].position1++;
				if(edeck[i].position1>=12)
					whoiswin=2;
			}
		}
		for (int index = 0; index < edeckcnumber; index++) {
			card c = edeck [index];
			if(c.onfield){
				c.attack(edeck,pdeck);
			}
		}
		ai.Align (pdeck,pdeckcnumber);
		ai.Align (edeck,edeckcnumber);
		wintest ();
		turn++;
		}
	void wintest(){
		whoiswin = 2;
		for (int index = 0; index < pdeckcnumber; index++) {
			card c = pdeck [index];
			if(c.islive)
				whoiswin=0;
		}
		if (whoiswin == 2) {
			return;
				}
		whoiswin = 1;
		for (int index = 0; index < edeckcnumber; index++) {
			card c = edeck [index];
			if(c.islive)
				whoiswin=0;
		}
		if (whoiswin == 1) {
			return;
		}
		}
	public void summontest(int p){
		if(phase=="card"&&ai.summontest(p,pdeck,edeck,pdeckcnumber,edeckcnumber)){
			summonposition=p;
			phase="summon";
			gtime=0;
		}
	}
	void OnGUI()
	{
		GUIStyle mgui = new GUIStyle ();
		mgui.fontSize = 30;
		mgui.normal.textColor = Color.white;
		GUI.DrawTexture (getRect (0.005, 0.255, 0.05, 0.05), returnt);
		GUI.Label (getRect (0.057, 0.257, 0.1, 0.1), "X "+ereturnnum.ToString(), mgui);
		GUI.DrawTexture (getRect (0.862, 0.685, 0.05, 0.05), returnt);
		GUI.Label (getRect (0.914, 0.687, 0.1, 0.1), "X "+preturnnum.ToString(), mgui);
		mgui.normal.textColor = Color.black;
		for (int index = 0; index < pdeckcnumber; index++) {
			card c = pdeck [index];
			c.drawCard();
		}
		for (int index = 0; index < edeckcnumber; index++) {
			card c = edeck [index];
			c.drawCard();
		}
		mgui.fontSize = 100;
		if (whoiswin == 1) {
			GUI.Label (getRect (0.4, 0.4, 0.2, 0.2), "승리", mgui);
		}
		if (whoiswin == 2) {
			GUI.Label (getRect (0.4, 0.4, 0.2, 0.2), "패배", mgui);
		}
		mgui.fontSize = 40;
		GUI.Label (getRect (0.04, 0.13, 0.1, 0.1), turn.ToString(), mgui);
		if (phase == "card") {
			for(int i=0;i<3;i++){
				if(ai.summontest(i,pdeck,edeck,pdeckcnumber,edeckcnumber))
					GUI.DrawTexture (getRect (0.018+0.08*i, 0.3, 0.1, 0.2), arrow);
			}
				}
		if (GUI.Button (getRect(0.87,0.813,0.115,0.048), auto)) {
			if (auto=="자동으로"){
				auto="자동해제";
				automode=true;
			}
			else{
				auto="자동으로";
				automode=false;
			}
		}
		if (GUI.Button (getRect (0.87, 0.875, 0.115, 0.048), "")&&phase=="start"&&whoiswin==0||(automode&&tick==60)) {
			turnend();
			tick=0;
		}
		if (GUI.Button (getRect (0.87, 0.935, 0.115, 0.048), "")) {
			Application.LoadLevel("mainscreen");
		}
		if (phase == "summon") {
			gtime++;
			if(gtime==30){
				summonEvent(summonposition);
				for (int index = 0; index < pdeckcnumber; index++) {
					card c = pdeck [index];
					if (c.state == "nselected" || c.state == "selected") {
						c.state = "deck";
					} else if (c.state == "nfield" || c.state == "sfield") {
						c.state = "field";
					}
				}
			}
			if(gtime>60)
				phase="start";
			GUI.DrawTexture (getRect (0.018+0.08*summonposition, 0.4, 0.1, 0.1), summont[(gtime/3)%7]);
		}
	}
	Rect getRect(double x, double y, double w,double h){
		return new Rect ((float)x * screenWidth, (float)y * screenHeight, (float)w * screenWidth, (float)h * screenHeight);
	}
}
