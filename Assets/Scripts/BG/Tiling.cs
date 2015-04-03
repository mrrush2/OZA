using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	public int offsetX = 2;			// avoids seams

	// So we don't infinite loop everything, we check to see if we've already tiled in a direction
	public bool tiledRight = false;
	public bool tiledLeft = false;

	public bool reverseScale = false;

	private float spriteWidth = 0f;
	private Camera cam;
	private Transform myTransform;

	void Awake () {
		cam = Camera.main;
		myTransform = transform;
	}

	// Use this for initialization
	void Start () {
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
		spriteWidth = sRenderer.sprite.bounds.size.x;
	}
	
	// Update is called constantly
	void Update () {
		if (tiledLeft == false || tiledRight == false) {
			// Basically what the camera can SEE
			float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;

			// Used in checking if we can see the edge of a sprite
			float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth/2) - camHorizontalExtend;
			float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth/2) + camHorizontalExtend;

			if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && tiledRight == false)
			{
				CreateNewTile (1);
				tiledRight = true;
			}
			else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && tiledLeft == false)
			{
				CreateNewTile (-1);
				tiledLeft = true;
			}
		}
	}
	
	void CreateNewTile (int rightOrLeft) {
		Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
		Transform newTile = Instantiate (myTransform, newPosition, myTransform.rotation) as Transform;

		if (reverseScale == true) {
			newTile.localScale = new Vector3 (newTile.localScale.x*-1, newTile.localScale.y, newTile.localScale.z);
		}

		newTile.parent = myTransform.parent;
		if (rightOrLeft > 0) {
			newTile.GetComponent<Tiling>().tiledLeft = true;
		}
		else {
			newTile.GetComponent<Tiling>().tiledRight = true;
		}
	}
}
