
using System;
using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
	public bool isLeft;
	private bool isAttached = false;
	private bool isFired = false;
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
