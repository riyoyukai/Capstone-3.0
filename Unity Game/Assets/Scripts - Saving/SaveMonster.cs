using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class SaveMonster {

	public string name;
	public bool hatched;
	public int hatchTimeLeft;
	public DateTime birthday;
	public float hunger;
	public int care;
	
	public float x;
	public float y;
	public float z;

	public SaveMonster(){}

}
