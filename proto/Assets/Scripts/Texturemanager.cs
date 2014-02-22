using UnityEngine;
using System.Collections;
public class Texturemanager : MonoBehaviour {
	public Texture[] skillt;
	public string[] skillname;
	public Texture[] interfacet;
	public Texture[] illustt;
	public string[] illustname;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public Texture getskilltexture(string s){
		return skillt [findindex (skillname, s)];
	}
	int findindex(string[] array, string key){
		for (int i=0; i<array.Length; i++) {
			if (array[i]==key)
				return i;
				}
		return -1;
	}
	public string getSkillname(int i){
		return skillname [i];
	}
	public Texture getattacktexture(string s){
		if (s == "attackvalue") {
						return interfacet [0];
				} else if (s == "power") {
						return interfacet [1];
				} else
						return getskilltexture (s);
	}
	public Texture getillusttexture(string s){
		return illustt [findindex (illustname, s)];
	}
}
