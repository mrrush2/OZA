using UnityEngine;
using System.Collections;
using System;

public class ReSkinPlayer : MonoBehaviour {

	public string instrument;



	void LateUpdate() // Must be done in LateUpdate
	{
		var subSprites = Resources.LoadAll<Sprite> ("PlayerSpritesheets/" + instrument); // Load all sprites from an instrument.

		foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
		{
			string spriteName = renderer.sprite.name;
			var newSprite = Array.Find(subSprites, item => item.name == spriteName);

			if (newSprite)
				renderer.sprite = newSprite; //Set new sprites for aniation
		}
	}


}