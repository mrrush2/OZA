using UnityEngine;
using System.Collections;

public class MusicNoteSmAttack : GenericNoteAttack {

	public override void Customize() {
		this.setKnockback (250f);
		this.setKnockbackVertical (20f);
		//this.setGravity (-1f); // For funsies
	}

}
