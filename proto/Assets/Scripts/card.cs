using UnityEngine;
using System.Collections;
using System.IO;

public class card : MonoBehaviour {
	public int position0=0;
	public int position1=0;
	public int life=0;
	public int maxhp=0;
	public int hp=0;
	public int atk=0;
	public int money=0;
	public attack[] mattack= new attack[4];
	public skill[] mskill = new skill[4];
	public Texture[] attackt = new Texture[4];
	public Texture[,] ast = new Texture[4,4];
	public Texture[] skillt = new Texture[4];
	public int cardno=0;
	public string cardname="t";
	public int cost=0;
	public string q="F";
	int[,] attackrange = new int[4, 6];
	public bool assigned=false;
	public bool onfield=false;
	public bool islive=true;
	public bool isfrontcard=true;
	public bool ismycard=true;
	public Texture s1;
	public Texture hp1;
	public Texture hp2;
	public Texture heart;
	public Sprite s2;
	public string state;
	public Texture front;
	public Texture back;
	public Texture aoh;
	public Texture rectt;
	public Texture dattackt;
	public bool isshort=true;
	bool up=true;
	battlemain bmain;
	private Camera _camera;
	float screenWidth;
	float screenHeight;
	
	// Use this for initialization
	void Start () {
		_camera = Camera.main;
		screenWidth = _camera.pixelWidth;
		screenHeight = _camera.pixelHeight;
		state = "deck";
	}
	
	// Update is called once per frame
	void Update () {
	}
	public void movetohand(){
		state = "hand";
	}
	public void assigncard(string acard){
		assigned = true;
		for (int i=0; i<4; i++) {
			mattack[i]=new attack();
			mskill[i]=new skill();
			attackt[i]=new Texture();
			skillt[i]=new Texture();
		}
		char[] info = acard.ToCharArray ();
		StreamReader sw= new StreamReader(makepath(info));
		cardname=sw.ReadLine();
		q=sw.ReadLine(); 
		cardno=int.Parse(sw.ReadLine());
		cost=int.Parse(sw.ReadLine());
		life=int.Parse(sw.ReadLine());
		hp=int.Parse(sw.ReadLine());
		maxhp = hp;
		atk=int.Parse(sw.ReadLine());
		for(int i=0;i<4;i++){
			string[] temp=new string[17];
			for(int j=0;j<17;j++){
				temp[j]=sw.ReadLine();
			}
			mattack[i].assignstring(temp);
		}
		for(int i=0;i<4;i++){
			string[] temp=new string[2];
			for(int j=0;j<2;j++){
				temp[j]=sw.ReadLine();
			}
			mskill[i].assignstring(temp);
		}
		sw.Close();
		int attacknumber=0;
		for(int i=0;i<6;i++){
			if(mattack[0].getattackrange()[i]!=0)
				attacknumber++;
		}
		money = atk * hp * (life + 1)*mattack[0].getattackrange()[0]*attacknumber;
		}
	string makepath(char[] info){
		string path="";
		if (info [0] == 'H') {path="cardinfo\\HS\\";
		} else {path="cardinfo\\"+info [0].ToString()+"\\"+info [1].ToString();
				}
		for (int i=2; i<info.Length; i++) {
			path=path+info[i].ToString();
				}
		return (path+".txt");
	}
	public void hit(int atk){
		hp = hp - atk;
		if (hp <= 0) {
			life--;
			hp=maxhp;
			if(life<0){
				islive=false;
			state="dead";
				onfield=false;
			}
				}
	}
	public void attack(card[] pdeck,card[] edeck){
		for (int i=1; i<7; i++) {
			if(mattack[0].getattackrange()[i-1]!=0){
				card ocard = findcard (11 - (position1 + i), edeck);
				if (ocard != null) {
					for(int j=0;j<mattack[0].getattackrange()[0];j++)
						ocard.hit (atk);
				}
			}
		}
	}
	card findcard (int position,card[] pdeck){
		foreach(card c in pdeck){
			if(c.onfield&&c.position1==position){
				return c;
			}
		}
		return null;
	}
	public void drawCard()
	{
		GUIStyle mgui = new GUIStyle ();
		mgui.fontSize = 10;
		if (ismycard) {
			if (onfield) {
				GUI.DrawTexture (getRect (0.02 + 0.08 * position1, 0.38, 0.1, 0.2), s1);
				GUI.DrawTexture (getRect (0.028 + 0.08 * position1, 0.35, 0.07, 0.02), hp1);
				GUI.DrawTexture (getRect (0.028 + 0.08 * position1, 0.35, 0.07 * hp / maxhp, 0.02), hp2);
				mgui.fontSize = 20;
				mgui.normal.textColor = Color.white;
				if (life > 3) {
					GUI.DrawTexture (getRect (0.0265 + 0.08 * position1, 0.323, 0.023, 0.028), heart);
					GUI.Label (getRect (0.045 + 0.08 * position1, 0.323, 0.023, 0.028), " X " + life.ToString (), mgui);
					
				} else {
					for (int i=0; i<life; i++) {
						GUI.DrawTexture (getRect (0.0265 + 0.08 * position1 + 0.02 * i, 0.323, 0.023, 0.028), heart);
					}
				}
				GUI.Label (getRect2 (0.065, 0.65, 0.02, 0.02), (6 - position0).ToString (), mgui);
				GUI.Label (getRect (0.05 + 0.08 * position1, 0.6, 0.02, 0.02), (6 - position0).ToString (), mgui);
				mgui.normal.textColor = Color.black;
				mgui.fontSize = 10;
			}
			if (state == "nselected" || state == "dead" || state == "nfield") {
				GUI.color = Color.gray;
			}
			if (GUI.Button (getRect2 (0, 0.75, 1.0 / 7.0, 0.25), "")) {
				if (state == "deck" || state == "nselected")
					state = "selected";
				else if (state == "field"|| state == "nfield")
					state = "sfield";
				else if (state == "sfield")
					state = "field";
				else if (state == "selected")
					state = "deck";
			}
			if (isshort) {
				drawCard2(0, 0.75, 1.0 / 7.0, 0.25,"front");
			} else {
				drawCard2(0, 0.75, 1.0 / 7.0, 0.25,"back");
			}
		} else {
			if(!isfrontcard)
				GUI.DrawTexture (getRect3 (0, 0.75, 1.0 / 7.0, 0.25), aoh);
			else {
				if (onfield) {
				GUI.DrawTexture (getRect (1 - 0.08 * position1, 0.38, -0.1, 0.2), s1);
				GUI.DrawTexture (getRect (0.908 - 0.08 * position1, 0.35, 0.07, 0.02), hp1);
				GUI.DrawTexture (getRect (0.908 - 0.08 * position1, 0.35, 0.07 * hp / maxhp, 0.02), hp2);
				mgui.fontSize = 20;
				mgui.normal.textColor = Color.white;
				if (life > 3) {
					GUI.DrawTexture (getRect (0.9065 - 0.08 * position1, 0.323, 0.023, 0.028), heart);
					GUI.Label (getRect (0.925 - 0.08 * position1, 0.323, 0.023, 0.028), " X " + life.ToString (), mgui);
					
				} else {
					for (int i=0; i<life; i++) {
						GUI.DrawTexture (getRect (0.9065 - 0.08 * position1 + 0.02 * i, 0.323, 0.023, 0.028), heart);
					}
				}
				mgui.normal.textColor = Color.blue;
				GUI.Label (getRect3 (0.065, 1.05, 0.02, 0.02), (6 - position0).ToString (), mgui);
				GUI.Label (getRect (0.93 - 0.08 * position1, 0.6, 0.02, 0.02), (6 - position0).ToString (), mgui);
				mgui.normal.textColor = Color.black;
				mgui.fontSize = 10;
			}
			if (state == "nselected" || state == "dead" || state == "nfield") {
				GUI.color = Color.gray;
			}
				if (isshort) {drawCard3(0, 0.75, 1.0 / 7.0, 0.25,"front");
				} else {
					drawCard3(0, 0.75, 1.0 / 7.0, 0.25,"back");
				}
			}
		}
		GUI.color = Color.white;
	}
	public void drawCard(double x,double y, double w, double h,string mode){
		GUIStyle mgui = new GUIStyle ();
		mgui.fontSize = (int)(70*w);
		switch (mode) {
		case("case") : {
			GUI.DrawTexture (getRect (x, y, w, h), aoh);
			break;
		}
		case("front") : {
			GUI.DrawTexture (getRect (x, y, w, h), front);
			GUI.Label (getRect (x+0.1225*w, y+0.07*h, 0.14*w, 0.08*h), q, mgui);
			GUI.Label (getRect (x+0.28*w, y+0.07*h, 0.14*w, 0.08*h), cardname, mgui);
			GUI.Label (getRect (x+0.819*w, y+0.07*h, 0.14*w, 0.08*h), cost.ToString (), mgui);
			GUI.DrawTexture (getRect (x+0.07*w, y+0.2*h, 0.875*w, 0.48*h), s1);
			GUI.Label (getRect (x+0.231*w, y+0.684*h, 0.14*w, 0.08*h), life.ToString (), mgui);
			GUI.Label (getRect (x+0.49*w, y+0.684*h, 0.14*w, 0.08*h), hp.ToString (), mgui);
			GUI.Label (getRect (x+0.749*w, y+0.684*h, 0.14*w, 0.08*h), atk.ToString (), mgui);
			GUI.color=Color.red;
			for(int i=0;i<6;i++){
				if(mattack[0].getattackrange()[i]!=0)
					GUI.DrawTexture(getRect(x+0.11*w+0.129*w*i, y+0.765*h, 0.125*w, 0.09*h),rectt);
			}
			
			GUI.color=Color.white;
			if(mattack[0].getattackrange()[0]>1){
				GUI.DrawTexture(getRect(x+0.11*w, y+0.876*h, 0.45*w, 0.09*h),dattackt);
				GUI.Label(getRect(x+0.56*w, y+0.884*h, 0.6*w, 0.09*h),mattack[0].getattackrange()[0].ToString(),mgui);
			}
			break;
		}
		case("back") : {
			GUI.DrawTexture (getRect (x, y, w, h), back);
			GUI.Label (getRect (x+0.231*w, y+0.032*h, 0.14*w, 0.08*h), life.ToString (), mgui);
			GUI.Label (getRect (x+0.49*w, y+0.032*h, 0.14*w, 0.08*h), hp.ToString (), mgui);
			GUI.Label (getRect (x+0.749*w, y+0.032*h, 0.14*w, 0.08*h), atk.ToString (), mgui);
			try {
				for (int i=0; i<4; i++) {
					GUI.DrawTexture (getRect (x+0.103*w + 0.196*w * i, y+0.114*h, 0.08*w, 0.06*h), skillt [i]);
					GUI.Label (getRect (x+0.196*w + 0.196*w * i,y+ 0.116*h, 0.091*w, 0.072*h), mskill [i].getLevel ().ToString (), mgui);
					GUI.Label (getRect (x+0.196*w, y+0.228*h + 0.2*h * i, 0.091*w, 0.072*h), mattack [i].getPower ().ToString (), mgui);
					GUI.Label (getRect (x+0.336*w, y+0.168*h + 0.2*h * i, 0.091*w, 0.072*h), mattack [i].getAttackvalue ().ToString () + "%", mgui);
					for (int j=0; j<4; j++) {
						GUI.DrawTexture (getRect (x+0.103*w + 0.196*w * j, y+0.3*h + 0.2*h * i, 0.08*w, 0.06*h), ast [i, j]);
						GUI.Label (getRect (x+0.196*w + 0.196*w * j, y+0.3*h + 0.2*h * i, 0.091*w, 0.072*h), mattack [i].getSkill () [j].getLevel ().ToString (), mgui);
					}
					for (int j=0; j<6; j++) {
						GUI.Label (getRect (x+0.399*w + 0.0749*w * j, y+0.228*h + 0.2*h * i, 0.091*w, 0.072*h), mattack [i].getattackrange () [j].ToString (), mgui);
					}
				}
			} catch {
			}
			break;
		}
				}
		}
	public void updateTexture(Texturemanager tm){
		for (int i=0; i<4; i++) {
			attackt[i]=tm.getattacktexture(mattack[i].getType());
			skillt[i]=tm.getskilltexture(mskill[i].getName());
			for(int j=0;j<4;j++){
				ast[i,j]=tm.getskilltexture(mattack[i].getSkill()[j].getName());
			}
			s1=tm.getillusttexture(cardno.ToString());
		}
	}
	Rect getRect(double x, double y, double w,double h){
		return new Rect ((float)x * screenWidth, (float)y * screenHeight, (float)w * screenWidth, (float)h * screenHeight);
	}
	Rect getRect2(double x, double y, double w,double h){
		return new Rect ((float)(x+position0/7.0) * screenWidth, (float)y * screenHeight, (float)w * screenWidth, (float)h * screenHeight);
	}
	Rect getRect3(double x, double y, double w,double h){
		return new Rect ((float)(x+(6-position0)/7.0+0.005) * screenWidth, (float)(y-0.75) * screenHeight, (float)w * screenWidth, (float)h * screenHeight);
	}
	void drawCard2(double x, double y, double w,double h,string mode){
		drawCard ((x+position0/7.0), y, w, h,mode);
	}
	void drawCard3(double x, double y, double w,double h,string mode){
		drawCard ((x+(6-position0)/7.0+0.005),(y-0.75), w, h,mode);
	}
}
