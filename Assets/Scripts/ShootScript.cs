using UnityEngine;
using System.Collections;

public class ShootScript : MonoBehaviour {

	public int dammage = 1;
	public bool isEnemyShot = false;
	public Sprite lightDestroy;
	public float laserTime;
	private float increament;
	public bool isDoggo = false;
	

	void Update()
	{
		laserTime -= Time.deltaTime;
		if (laserTime <= 0) {
			Destroy(gameObject);
		}
	}


	void FixedUpdate()
	{
		if (!isEnemyShot) {
			this.transform.Translate (Vector3.up * 6 * Time.deltaTime);
		} else {
			this.transform.Translate (Vector3.up * -6 * Time.deltaTime);
		}
	}

	void OnCollisionEnter2D( Collision2D coll)
	{
		if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Enemy") 
		{
			if (isDoggo) {
				FxHelper.instanceFX.CrashFX (transform.position);
				SoundHelper.instanceSound.DogeExplodeSound ();
			} else {
				FxHelper.instanceFX.SwordFX (transform.position);
				SoundHelper.instanceSound.SwordSound ();
			}
			Destroy(gameObject);
		} else {
			Destroy(gameObject);
		}
	}
}
