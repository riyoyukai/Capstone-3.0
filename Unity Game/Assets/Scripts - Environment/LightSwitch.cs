using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour {

	public MeshRenderer meshRenderer;
	public Material dayMat;
	public Material nightMat;
	public Light envLight;

	public GameObject switchOn;
	public GameObject switchOff;

	private bool on = true;

	void OnMouseUp(){
		if(on){ // turn light off
			meshRenderer.material = nightMat;
			envLight.intensity = .3f;
		}else{
			meshRenderer.material = dayMat;
			envLight.intensity = 1;
		} // turn light on

		on = !on;
		switchOn.SetActive(!switchOn.activeSelf);
		switchOff.SetActive(!switchOff.activeSelf);
	}
}
