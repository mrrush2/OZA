using UnityEngine;
using System.Collections;

public class CameraLogic : MonoBehaviour 
{
	GameObject mainCamera;
	static float shake = 0f;
	static float shakeMult = 0.7f;
	static float decreaseTime = 1.0f;
	
	void Awake ()
	{
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	void Update ()
	{
		if (shake > 0)
		{
			//mainCamera.transform.localPosition = Random.insideUnitSphere * shakeMult * shake;
			mainCamera.transform.localPosition = new Vector3 ((float)Random.Range (-2,2) * shakeMult * shake, ((float)Random.Range (-2,2) * shakeMult * shake) + 0.1733569f, -10);
			shake -= Time.deltaTime * decreaseTime;
		}
		else
		{
			shake = 0.0f;
		}
	
	// TODO: Get rid of this debug screenshake call!
	if (Input.GetKeyDown (KeyCode.C))
	{
		ShakeItUp (0.25f, 0.2f, 1.0f);
	}
	
	}

	public static void ShakeItUp (float amount, float mult, float falloff)
	{
		shake = amount;
		shakeMult = mult;
		falloff = 1.0f;
	}
}
