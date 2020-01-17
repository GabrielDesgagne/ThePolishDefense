
using System;
using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
	public float startToCurve = 5f;
	public float forceToApplyMax = 30f;
	private Rigidbody rb;
	private const float timeBeforeDelete = 10;
	private bool isAttached = false;
	private bool isFired = false;
	public float startDisapearTime = 0;
	private Collider collider;

	private void Start()
	{
		collider = GetComponent<Collider>();
		rb = transform.GetComponent<Rigidbody>();
	}

	void OnTriggerEnter(Collider other) 
	{
		if(other.CompareTag("Bow"))
		{
			AttachArrow(other.GetComponent<BowManager>());
		}

		
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.transform.CompareTag("Enemy"))
		{
			other.transform.GetComponent<Enemy>().TakeDamage(PlayerManager.Instance.player.playerStat.damage);
			Debug.Log("colisionEnemy");
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
		if (isFired && rb.velocity.magnitude > startToCurve) {
			transform.LookAt (transform.position + rb.velocity);
		}

		if ((int)startDisapearTime != 0)
		{
			if (startDisapearTime + timeBeforeDelete <= Time.time)
			{
				Destroy(this.gameObject);
			}
		}
	}
	public void Fired(float veloToApply) 
	{
		isFired =  true;
		transform.parent = null;
		rb.isKinematic = false;
		rb.velocity = transform.forward * forceToApplyMax * veloToApply;
		rb.useGravity = true;
		collider.isTrigger = true;
		startDisapearTime = Time.time;
	}
	private void AttachArrow(BowManager bow) 
	{
		if (!isAttached) 
		{
			bow.AttachBowToArrow (this);
			isAttached = true;
		}
	}

	public void setOffHand()
	{
		transform.parent = null;
		rb.useGravity = true;
		rb.isKinematic = false;
		startDisapearTime = Time.time;
	}
}
