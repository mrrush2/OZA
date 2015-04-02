using UnityEngine;
using System.Collections;
using UnityEngine.UI; //Don't forget to do this when working with UI or it will be really silly

public class ScrollbarHelper : MonoBehaviour 
{
	public bool inRect = false;
	Scrollbar bar;							// The scrollbar's Script
//	public RectTransform scrollableBox;		// The object that contains the coordinates of where the mousewheel will work
//	Rect scrollableArea;

	// Awake is called as soon as this object exists, before Start.
	void Awake()
	{
		bar = this.GetComponent<Scrollbar>(); 
//		scrollableArea = new Rect (scrollableBox.rect);
	}

	// These two functions are to be called by the event system for when
	// the player hits the buttons above or below the bar.
	public void ScrollUp()
	{
		bar.value -= .1f;
	}
	public void ScrollDown()
	{
		bar.value += .1f;
	}


	void OnGUI()
	{
		//This function is for mousewheel scrolling. Couldn't get it to only work when hovering over the area, though.

//		Event e = Event.current;
//		if (scrollableArea.Contains (e.mousePosition)) 
//		{
		bar.value -= (Input.GetAxis ("Mouse ScrollWheel") / 10f);
//			inRect = true;
//		}
//		else
//			inRect = false;

	}

}
