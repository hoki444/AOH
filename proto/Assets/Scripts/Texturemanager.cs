using UnityEngine;
using System.Collections;
public class Texturemanager : MonoBehaviour {
	public Texture[] skillt;
	public string[] skillname;
	// Use this for initialization
	void Start () {
		Debug.Log (this.transform);
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
}
