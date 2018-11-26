using UnityEngine;
using System.Collections;

public class BGLooper : MonoBehaviour {

	public float posMin = -2.359058f;		// value for min random position of x
	public float posMax = 2.245874f;		// ... and it's for max.
	private SpriteRenderer sRender;			// reference for initial SpriteRendere component.

	void Start () {

	}

	// do looper thing in this void
	void OnTriggerEnter2D(Collider2D coll)
	{
		// First, we need the backgrounds look like looper (parallax).
		//So, if trgigered with tag Background, move that to right position
		if (coll.tag == "Background") {
			//... just find the best number to calculate with vector2-up.
			coll.transform.Translate (Vector2.up * 7.66f);
		}
		// Nah, if we meet the cunts, kill her.
		if (coll.tag == "Enemy") {
			Destroy(coll.gameObject);		
		}
	}
}
