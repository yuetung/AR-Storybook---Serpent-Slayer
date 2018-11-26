using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour {

	private bool enemySpawn;
	private Weapon[] addWeapons;
	public float speed;
	private float posMin = -1.719578f;
	private float posMax = 1.653888f;
	public bool moveRight;
	public GameObject mainCam;
	private float coolDown;
	public float attackTime;
	private Weapon weapon;
//	private Score scoreGui;
	private Health health;


	
	void Start()
	{

		health = GetComponent<Health> ();
//		scoreGui = GameObject.Find ("GUI").GetComponentInChildren<Score> ();
		coolDown = attackTime;

		addWeapons = GetComponentsInChildren<Weapon> ();
		weapon = GetComponent<Weapon> ();
		
		float initialMove = Random.Range(1, 10);
		if (initialMove <= 5) {
			moveRight = true;		
		} 
		else if (initialMove > 5) {
			moveRight = false;	
		}

		
		
		enemySpawn = true;
		foreach (Weapon addWeapon in addWeapons) {
			addWeapon.enabled = false;
		}
		Vector3 pos = transform.localPosition;
		pos.x = Random.Range (posMin, posMax);
		transform.localPosition = pos;
		
	}
	
	
	void Update()
	{
		if (coolDown > 0) {
			coolDown -= Time.deltaTime;
		}
		if (coolDown <= 0) {
			coolDown = attackTime;	
		}
		if (enemySpawn == true && BossNotAttack == false) {	
			foreach (Weapon addWeapon in addWeapons){
				addWeapon.enabled = true;	
				}
			weapon.enabled = false;
			}
		else if( enemySpawn&& BossNotAttack)
		{
			weapon.enabled = true;
		}


		if (health.hp <= 0) {
			ActionHelper.instanceAction.FinishLevel();
			FxHelper.instanceFX.CrashFX(transform.localPosition);
		}
	
	}
	
	
	void FixedUpdate()
	{ 
		if(enemySpawn) {
			Spawn ();	
		}
	}
	
	private void Spawn()
	{

		if (moveRight == true) {
			transform.Translate (transform.right * speed * Time.deltaTime);
		} 
		if(moveRight == false)
		{
			transform.Translate (transform.right * -speed * Time.deltaTime);;
		}
		
	}


	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "BorderLeft") {
			moveRight = true;
			print ("collded with Borderleft, moving right");
		}
		
		if (coll.gameObject.tag == "BorderRight") {
			moveRight = false;
		}
		if (coll.gameObject.tag == "Player Laser") {
			Score scoreScript = GameObject.Find("GUI").GetComponent<Score>();
			scoreScript.AddPoint();		
		}
	}

	public bool BossNotAttack
	{
		get
		{
			return coolDown >= 0f && coolDown <= 2f;
		}
	}
}
