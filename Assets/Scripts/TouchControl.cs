using UnityEngine;
using System.Collections;

public class TouchControl : MonoBehaviour {

	public GUITexture moveLeft;
	public GUITexture moveRight;
	public GUITexture fire;
	public GUITexture doge;
	public GameObject player;
	private PlayerMovement playerMove;
	private Weapon[] weapons;
	public Score scoreDebug;

	void Start()
	{
		playerMove = player.GetComponent<PlayerMovement> ();
	}

	void CallFire()
	{
		weapons = player.GetComponentsInChildren<Weapon> ();
		foreach (Weapon weapon in weapons) {
			if(weapon.enabled == true)
			weapon.Fire();		
		}
	}

	void CallDoge()
	{
		weapons = player.GetComponentsInChildren<Weapon> ();
		foreach (Weapon weapon in weapons) {
			if(weapon.enabled == true)
				weapon.Doge();		
		}
	}

	void Update()
	{
		for(int i =0; i < Input.touches.Length; i++)
		{
			//scoreDebug.AddPoint (1);
			Touch t = Input.touches [i];
			Input.multiTouchEnabled = true;
			if(moveLeft.HitTest(t.position, Camera.main))
			{
				playerMove.MoveLeft();
			}
			if(moveRight.HitTest(t.position, Camera.main))
			{
				playerMove.MoveRight();
			}
			if(t.phase == TouchPhase.Began)
			{
				if(fire.HitTest(t.position, Camera.main))
				{
					CallFire();
				}

				else if(doge.HitTest(t.position, Camera.main))
				{
					CallDoge();
				}
			}
		}
	}

}
