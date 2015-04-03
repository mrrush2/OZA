using UnityEngine;
using System.Collections;

public class MousePointer : MonoBehaviour {

	public Texture2D cursor;

	int cursorWidth = 32;
	int cursorHeight = 32;

	void Start () 
	{
		Screen.showCursor = false;
	}

	void OnGUI () 
	{
		GUI.DrawTexture (new Rect (Input.mousePosition.x, Screen.height - Input.mousePosition.y, cursorWidth, cursorHeight), cursor);
	}

	void Update ()
	{
		if (Screen.showCursor)
			Screen.showCursor = false;
	}
}
