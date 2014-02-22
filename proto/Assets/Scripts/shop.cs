using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public class shop : MonoBehaviour {
	public Texture basedeck;
	public Texture basebuy1;
	public Texture basebuy2;
	public Texture moneyt;
	public Texture rectt;
	private Camera _camera;
	public Texture aoh;
	float screenWidth;
	float screenHeight;
	int ind=0;
	int money=0;
	int buyq=0;
	int page=0;
	int nowdeck=0;
	int cardnumber=0;
	int changecard=0;
	string mode="menu";
	string state="see";
	card[] cardbox=new card[100];
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
		StreamReader sw= new StreamReader("battleinfo\\box.txt");
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
		sw= new StreamReader("battleinfo\\playerinfo.txt");
		money = int.Parse(sw.ReadLine ());
		nowdeck = int.Parse(sw.ReadLine ());
		sw.Close ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnGUI()
	{
		
		GUIStyle mgui = new GUIStyle ();
		mgui.fontSize = 50;
		if (mode == "menu") {
			GUI.DrawTexture (getRect (0, 0, 1, 1), rectt);
			GUI.DrawTexture (getRect (0.53, 0.02, 0.1, 0.1), moneyt);
			GUI.Label (getRect (0.1, 0.022, 0.5, 0.1), "SHOP", mgui);
			GUI.Label (getRect (0.65, 0.022, 0.5, 0.1), "X " + money.ToString () + "D", mgui);
			if (GUI.Button (getRect (0.3, 0.3, 0.4, 0.15), "구매")) {
				mode = "buy1";
			}
			if (GUI.Button (getRect (0.3, 0.55, 0.4, 0.15), "판매")) {
				mode = "sale";
			}
			if (GUI.Button (getRect (0.3, 0.8, 0.4, 0.15), "메인으로")) {
				Application.LoadLevel ("mainscreen");
			}
		} else if (mode == "sale") {
			GUI.DrawTexture (getRect (0, 0, 1, 1), basedeck);
			GUI.Label (getRect (0.65, 0.022, 0.5, 0.1), money.ToString () + "D", mgui);
			GUI.color = Color.gray;
			GUI.DrawTexture (getRect (0, 0.092 + 0.0456 * page, 0.085, 0.092), rectt);
			GUI.color = Color.white;
			for (int i=0; i<6; i++) {
				if (page * 6 + i < cardnumber) {
					if (GUI.Button (getpostition (i), "")) {
						if (state == "see")
							state = "change";
						else
							state = "see";
						changecard = i;
					}
					if (state == "change" && i != changecard)
						GUI.color = Color.gray;
					drawcard (i, cardbox [page * 6 + i], "front");
					if (state == "change" && i == changecard) {
						cardbox [page * 6 + i].drawCard (0.62, 0.12, 0.22, 0.35, "front");
						mgui.fontSize = 50;
						GUI.Label (getRect (0.67, 0.565, 0.3, 0.1), cardbox [page * 6 + i].money.ToString () + "D", mgui);
					}
					GUI.color = Color.white;
				}
			}
			if (GUI.Button (getRect (0, 0, 0.085, 0.092), "")) {
				page--;
				state = "see";
				if (page < 0) {
					page = 16;
				}
			}
			if (GUI.Button (getRect (0, 0.908, 0.085, 0.092), "")) {
				page++;
				state = "see";
				if (page > 16) {
					page = 0;
				}
			}
			if (GUI.Button (getRect (0.52, 0.79, 0.164, 0.054), "")) {
				salecard (changecard);
				savedata ();
			}
			if (GUI.Button (getRect (0.766, 0.79, 0.164, 0.054), "")) {
				state = "see";
			}
			if (GUI.Button (getRect (0.52, 0.919, 0.164, 0.054), "")) {
			}
			if (GUI.Button (getRect (0.766, 0.919, 0.164, 0.054), "")) {
				Application.LoadLevel ("mainscreen");
			}
		}
		else if (mode == "buy1") {
			GUI.DrawTexture (getRect (0, 0, 1, 1), basebuy1);
			GUI.Label (getRect (0.65, 0.022, 0.5, 0.1), money.ToString () + "D", mgui);
			if (GUI.Button (getRect (0.8, 0.2, 0.1, 0.1), "구매")) {
				buyq=1;
				mode="buy2";
				state="case";
			}
			if (GUI.Button (getRect (0.8, 0.355, 0.1, 0.1), "구매")) {
				buyq=2;
				mode="buy2";
				state="case";
			}
			if (GUI.Button (getRect (0.8, 0.51, 0.1, 0.1), "구매")) {
				buyq=3;
				mode="buy2";
				state="case";
			}
			if (GUI.Button (getRect (0.8, 0.665, 0.1, 0.1), "구매")) {
				buyq=4;
				mode="buy2";
				state="case";
			}
			if (GUI.Button (getRect (0.41, 0.86, 0.164, 0.054), "")) {
				Application.LoadLevel ("mainscreen");
			}
		}
		else if (mode == "buy2") {
			mgui.fontSize=40;
			GUI.DrawTexture (getRect (0, 0, 1, 1), basebuy2);
			GUI.Label (getRect (0.65, 0.022, 0.5, 0.1), money.ToString () + "D", mgui);
			mgui.fontSize=35;
			if(state=="case"){
				if (GUI.Button(getRect (0.3, 0.2, 0.32, 0.5), "")&&money>=buycard(buyq)&&cardnumber<101){
					state="card";
					tstr=getcard(buyq);
					tobj=Instantiate (mcard,
					                  mcard.transform.position,
					                  this.gameObject.transform.localRotation);
					tobj.name="tcard";
					tcard = GameObject.Find("tcard").GetComponent<card>();
					tcard.assigncard(tstr);
					tcard.updateTexture(tmanager);
					money-=buycard(buyq);
					savedata();
				}
				GUI.color=Color.yellow;
				GUI.DrawTexture (getRect (0, 0.72, 1, 0.28), rectt);
				GUI.color=Color.white;
				GUI.Label (getRect (0.3, 0.1, 0.5, 0.1), "카드를 클릭하세요", mgui);
				GUI.DrawTexture (getRect (0.3, 0.2, 0.32, 0.5), aoh);
				if (GUI.Button (getRect (0.4, 0.8, 0.2, 0.1), "취소")){
					mode="buy1";
				}
				mgui.fontSize=50;
			}
			else if(state=="card"){
				tcard.drawCard(0.34, 0.2, 0.32, 0.5,"front");
				mgui.fontSize=50;
				GUI.Label (getRect (0.45, 0.785, 0.3, 0.1), tcard.money.ToString () + "D", mgui);
				mgui.fontSize=30;
				GUI.Label (getRect (0.15, 0.86, 0.3, 0.1), buycard(buyq).ToString () + "D", mgui);
				mgui.fontSize=50;
				if (GUI.Button (getRect (0.048, 0.925, 0.145, 0.042), "")) {
					cardbox[cardnumber]=tcard;
					cardnumber++;
					tobj.name="aftercard";
					savedata();
					state="case";
				}
				if (GUI.Button (getRect (0.237, 0.925, 0.145, 0.042), "")) {
					money+=tcard.money;
					GameObject.Destroy(tobj);
					savedata();
					state="case";
				}
				if (GUI.Button (getRect (0.425, 0.925, 0.145, 0.042), "")) {
					cardbox[cardnumber]=tcard;
					cardnumber++;
					tobj.name="aftercard";
					savedata();
					mode="buy1";
				}
				if (GUI.Button (getRect (0.612, 0.925, 0.145, 0.042), "")) {
					money+=tcard.money;
					GameObject.Destroy(tobj);
					savedata();
					mode="buy1";
				}
				if (GUI.Button (getRect (0.800, 0.925, 0.145, 0.042), "")) {
					cardbox[cardnumber]=tcard;
					cardnumber++;
					tobj.name="aftercard";
					savedata();
					Application.LoadLevel ("mainscreen");
				}
			}
		}
	}
	int buycard(int q){
		if (q == 1) {
			return 500;
				}
		else if (q == 2) {
			return 5000;
		}
		else if (q == 3) {	
			return 50000;
		}
		else if (q == 4) {
			return 500000;
		}
		return 0;
	}
	string getcard(int q){
		StreamReader sw= new StreamReader("battleinfo\\buy"+q.ToString()+".txt");
		string keystring;
		int i = Random.Range (0, 1000);
		if (i < int.Parse (sw.ReadLine ())) {
			keystring="HS";
		}
		else if (i < int.Parse (sw.ReadLine ())) {
			keystring="S";
		}
		else if (i < int.Parse (sw.ReadLine ())) {
			keystring="A";
		}
		else if (i < int.Parse (sw.ReadLine ())) {
			keystring="B";
		}
		else if (i < int.Parse (sw.ReadLine ())) {
			keystring="C";
		}
		else if (i < int.Parse (sw.ReadLine ())) {
			keystring="D";
		}
		else if (i < int.Parse (sw.ReadLine ())) {
			keystring="E";
		}
		else{
			keystring="F";
		}
		while(sw.ReadLine ()!=keystring){
		}
		int cnumber = int.Parse(sw.ReadLine ());
		i = Random.Range (0, 1000);
		int k = 1;
		while (i >= k*(1000)/cnumber) {
			sw.ReadLine();
			k++;
		}
		keystring += int.Parse (sw.ReadLine ());
		sw.Close ();
		return keystring;
	}
	Rect getpostition(int i){
		if (i > 2) {return getRect (0.28,0.05+ 0.25*(i-3), 0.16, 0.25);
		} else {return getRect (0.1 , 0.05+0.25*i, 0.16, 0.25);
		}
	}
	void drawcard(int i,card c,string mode){
		if (i > 2) {c.drawCard(0.28,0.05+ 0.25*(i-3), 0.16, 0.25,mode);
		} else {c.drawCard (0.1 , 0.05+0.25*i, 0.16, 0.25,mode);
		}
		return;
	}
	void salecard(int p){
		if (page * 6 + p >= cardnumber)
			return;
		else {
			money+=cardbox[p].money;
			for(int i=page * 6 + p;i<cardnumber;i++)
				cardbox[i]=cardbox[i+1];
			cardnumber--;
		}
	}
	void savedata(){
		StreamWriter sw= new StreamWriter("battleinfo\\box.txt");
		sw.WriteLine (cardnumber.ToString());
		for (int i=0; i<cardnumber; i++) {
			sw.WriteLine(cardbox[i].q+cardbox[i].cardno.ToString());
		}
		sw.Close ();
		sw= new StreamWriter("battleinfo\\playerinfo.txt");
		sw.WriteLine (money.ToString());
		sw.WriteLine (nowdeck.ToString ());
		sw.Close ();
	}
	Rect getRect(double x, double y, double w,double h){
		return new Rect ((float)x * screenWidth, (float)y * screenHeight, (float)w * screenWidth, (float)h * screenHeight);
	}
}
