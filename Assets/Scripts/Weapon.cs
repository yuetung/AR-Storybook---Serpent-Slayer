using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public Transform laserPrefab;
	public Transform dogePrefab;
	public bool isEnemyWeapon = false;
	public float shootDelay = 1.0f;
	private float waitToShoot;

	void Start () {
		waitToShoot = 0f;
	}

	void Update () {

		if (waitToShoot > 0) {
			waitToShoot -= Time.deltaTime;		
		}

		if (Input.GetKeyDown (KeyCode.Space) && isEnemyWeapon == false) {
			Fire();
		}

		if (Input.GetKeyDown (KeyCode.B) && isEnemyWeapon == false) {
			Doge();
		}

		if (isEnemyWeapon == true && EnemyAttack == true) {
			waitToShoot = shootDelay;
			var shootLaser = Instantiate (laserPrefab) as Transform;
			Vector3 pos = shootLaser.transform.position;
			pos.x = transform.position.x;
			pos.y = transform.position.y - 0.1f;
			pos.z = transform.position.z;
			shootLaser.transform.position = pos;
			SoundHelper.instanceSound.EnemyLaserSound();
		}
	}


	public void Fire()
	{
		var shootLaser = Instantiate(laserPrefab) as Transform;
		Vector3 pos = shootLaser.transform.position;
		pos.x = transform.position.x;
		pos.y = transform.position.y + 0.5f;
		pos.z = transform.position.z;
		shootLaser.transform.position = pos;
		SoundHelper.instanceSound.PlayerLaserSound();
	}
		
	public void Doge()
	{
		var shootLaser = Instantiate(dogePrefab) as Transform;
		Vector3 pos = shootLaser.transform.position;
		pos.x = transform.position.x;
		pos.y = transform.position.y + 0.5f;
		pos.z = transform.position.z;
		shootLaser.transform.position = pos;
		SoundHelper.instanceSound.DogeSound();
	}

public bool EnemyAttack
	{
		get
		{
			return waitToShoot <= 0f;
		}
	}
}