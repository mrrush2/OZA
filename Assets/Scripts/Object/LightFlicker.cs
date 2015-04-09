using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

	GameObject thisLight;			// Reference to this light so we can edit its properties on the fly
	float baseMultiplier;			// Holds the intensity of the light so we have a reference to its original value
	
	public float flickerMinStr;		// As a percentage, the lower bound of the flicker
	public float flickerMaxStr;		// As a percentage the upper bound of the flicker
	
	float timer;					// Used in setting the flicker rate
	public float flickerRate;		// A quantifiable value for epilepsy.
	


	// Use this for initialization
	void Start () {
		thisLight = this.gameObject;
		baseMultiplier = thisLight.light.intensity;
		
		if (flickerMinStr > flickerMaxStr)			// Stops min > max, which would cause weird errors
			flickerMaxStr = flickerMinStr + .25f;
			
		timer = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Time.time - timer > flickerRate)
		{
			thisLight.light.intensity = Random.Range (baseMultiplier * flickerMinStr, baseMultiplier * flickerMaxStr);
			timer = Time.time;
		}
	}
}
