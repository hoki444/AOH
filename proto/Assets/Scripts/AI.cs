//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.18063
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class AI
{
	public AI ()
	{
	}
	public void doAI(card[] pdeck,card[] edeck,int deckcnumber,int edeckcnumber){
		for(int i=0;i<3;i++){
			if(summontest(i,edeck,pdeck,edeckcnumber,deckcnumber)){
				for (int ind = 0; ind < edeckcnumber; ind++) {
					card c = edeck [ind];
					if(c.onfield==false&&c.islive){
						c.isfrontcard=true;
						c.onfield=true;
						c.position1=i;
						c.state="field";
						break;
					}
				}
			}
		}
	}
	public void Align(card[] pdeck,int deckcnumber)
	{
		string mode = "dead";
		int deadnumber = 0;
		int decknumber = 0;
		for(int i=0;i<deckcnumber;i++){
			if(mode=="dead"){
				if(pdeck[i].islive){
					mode="deck";
					deadnumber--;
					decknumber--;
				}
				deadnumber++;
				decknumber++;
			}
			if(mode=="deck"){
				if(!pdeck[i].islive){
					swapcard(pdeck[i].position0,deadnumber,pdeck);
					deadnumber++;
				}
				if(pdeck[i].onfield){
					mode="field";
					decknumber--;
				}
				decknumber++;
			}
			if(mode=="field"){
				if(!pdeck[i].islive){
					swapcard(pdeck[i].position0,deadnumber,pdeck);
					if(decknumber==deadnumber){
						deadnumber++;
						decknumber++;
					}
					else{
						swapcard(pdeck[i].position0,decknumber,pdeck);
						deadnumber++;
						decknumber++;
					}
				}
				else if(!pdeck[i].onfield){
					swapcard(pdeck[i].position0,decknumber,pdeck);
					decknumber++;
				}
			}
		}
		for (int i=deckcnumber-1; i>=decknumber; i--) {
			for(int j=i;j<deckcnumber-1;j++)
			{
				if(pdeck[j].position1>pdeck[j+1].position1){
					swapcard(j,j+1,pdeck);
				}
			}
		}
	}
	public void swapcard(int p1,int p2,card[] pdeck){
		card t1=new card();
		card t2=new card();
		foreach(card c in pdeck){
			if(c.position0==p1){
				t1=c;
				c.position0=p2;
			}
			else if(c.position0==p2){
				t2=c;
				c.position0=p1;
			}
		}
		pdeck [p1] = t2;
		pdeck [p2] = t1;
	}
	public bool summontest(int index,card[] pdeck,card[] edeck,int deckcnumber,int edeckcnumber){
		for (int ind = 0; ind < deckcnumber; ind++) {
			card c = pdeck [ind];
			if(c.onfield&&c.position1==index){
				return false;
			}
		}
		for (int ind = 0; ind < edeckcnumber; ind++) {
			card c = edeck [ind];
			if(c.onfield&&c.position1==11-index){
				return false;
			}
		}
		return true;
	}
}