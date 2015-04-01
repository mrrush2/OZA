using UnityEngine;
using System.Collections;

public class Parallaxxer : MonoBehaviour {

	public Transform[] backgrounds;		// Array of elements to parallax
	private float[] parallaxScales;		// Speeds
	public float smoothing = 1f;

	private Transform cam;				// Reference to camera's pos
	private Vector3 previousCamPos;		// Position of camera a frame ago

	// Called before Start
	void Awake () {
		cam = Camera.main.transform;
	}
	
	// Use this for initialization
	void Start () {
		previousCamPos = cam.position;

		// Set each parallax speed
		parallaxScales = new float[backgrounds.Length];
		for (int i = 0; i < backgrounds.Length; i++) 
		{
			parallaxScales[i] = backgrounds[i].position.z*-1;
		}
	}
	
	// Update is called constantly
	void Update () 
	{
		// Go through each background and apply a parallax effect.
		for (int i = 0; i < backgrounds.Length; i++) 
		{
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;
			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
			// Fade the difference using god damn magic (Lerp)
			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}
		previousCamPos = cam.position;
	}
}
