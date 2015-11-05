using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour {
	
	public SkinnedMeshRenderer monsterModel;
	public MeshRenderer eggModel;
	public Material dayMat;
	public Material nightMat;
	public Light envLight;

	public GameObject switchOn;
	public GameObject switchOff;

	private bool on = true;

	void OnMouseUp(){
		if(on){ // turn light off
			monsterModel.material = nightMat;
			eggModel.material = nightMat;
			envLight.intensity = .3f;
		}else{
			monsterModel.material = dayMat;
			eggModel.material = dayMat;
			envLight.intensity = 1;
		} // turn light on

		on = !on;
		switchOn.SetActive(!switchOn.activeSelf);
		switchOff.SetActive(!switchOff.activeSelf);
	}
}
