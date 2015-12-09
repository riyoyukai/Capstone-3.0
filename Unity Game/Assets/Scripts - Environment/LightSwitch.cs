using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour {
	
	public SkinnedMeshRenderer monsterModel;
	public MeshRenderer eggModel;
	public Material dayMat;
	public Material nightMat;
	public Material eggDayMat;
	public Material eggNightMat;
	public Light envLight;

	public GameObject switchOn;
	public GameObject switchOff;

	private bool on = true;

	void OnMouseUp(){
		if(on){ // turn light off
			monsterModel.material = nightMat;
			eggModel.material = eggNightMat;
			envLight.intensity = .3f;
		}else{
			monsterModel.material = dayMat;
			eggModel.material = eggDayMat;
			envLight.intensity = 1;
		} // turn light on

		on = !on;
		switchOn.SetActive(!switchOn.activeSelf);
		switchOff.SetActive(!switchOff.activeSelf);
	}
}
