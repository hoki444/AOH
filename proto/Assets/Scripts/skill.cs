//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.18052
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
		public class skill
		{
	string name;
	string skilltiming="null";
	skillai sai=new skillai();
	int level;
	public skill(){
		name = "null";
		level = 1;
		}
	public skill (string x,int y)
	{
		name = x;
		level = y;
	}
	public skill (skill otherskill)
	{
		name = otherskill.getName();
		level = otherskill.getLevel();
	}
	public void setName (string x)
	{
		name = x;
	}
	public void setLevel (int y)
	{
		level = y;
	}
	public string getName()
	{
		return name;
	}
	public int getLevel()
	{
		return level;
	}
	public string[] tostring(){
		string[] result = new string[2];
		result [0] = name;
		result [1] = level.ToString ();
		return result;
	}
	public void assignstring(string[] s){
		name = s [0];
		level = int.Parse (s [1]);
		}
		}

