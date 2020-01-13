
using System;
using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
	private const float timeBeforeDelete = 10;
	public bool isLeft;
	private bool isAttached = false;
	private bool isFired = false;
	public float startDisapearTime = 0;
	void OnTriggerEnter(Collider other) 
	{
		if(other.transform.CompareTag("Bow"))
		{
			AttachArrow(other.GetComponent<BowManager>());
		}
	}
	private void OnTriggerExit(Collider other)
	{
		
		if (other.transform.CompareTag("Bow"))
		{
			GetComponent<BoxCollider>().isTrigger = false;
		}
	}
	void Update() 
	{
		if (isFired && transform.GetComponent<Rigidbody> ().velocity.magnitude > 5f) {
			transform.LookAt (transform.position + transform.GetComponent<Rigidbody> ().velocity);
		}

		if (startDisapearTime != 0)
		{
			if (startDisapearTime + timeBeforeDelete <= Time.time)
			{
				Destroy(this.gameObject);
			}
		}
	}
	public void Fired() 
	{
		isFired =  true; 
	}
	private void AttachArrow(BowManager bow) 
	{
		if (!isAttached) 
		{
			bow.AttachBowToArrow (this);
			isAttached = true;
		}
	}
}
