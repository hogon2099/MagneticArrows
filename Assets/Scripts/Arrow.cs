using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Arrow : MonoBehaviour
{
	private float FieldRadius;
	[HideInInspector] public float affectingPower = 2f;
	private Rigidbody2D rgbody2D;
	//private Animator animator;


	void Start()
	{
		rgbody2D = GetComponent<Rigidbody2D>();
		//animator = GetComponent<Animator>();
		FieldRadius = GetComponent<CircleCollider2D>().radius;
		//GameObject.Destroy(gameObject, 7f);
	}

	// Update is called once per frame
	void Update()
	{
		if (rgbody2D.bodyType == RigidbodyType2D.Static) return;

		float oldAngle = transform.eulerAngles.z;
		Vector2 direction = rgbody2D.velocity;
		float newAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		//float resultANgle = Mathf.LerpAngle(oldAngle, newAngle, 0.05f);
		transform.rotation = Quaternion.Euler(0, 0, newAngle);
	//	animator.SetFloat("Angle", newAngle);

	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.GetComponent<Arrow>()) return;

		rgbody2D.gravityScale = 0;
		rgbody2D.velocity = Vector2.zero;
		rgbody2D.bodyType = RigidbodyType2D.Static;
		//this.enabled = false;
	}
	private void OnTriggerExit2D(Collider2D other)
	{
		Debug.Log("Entered the trigger collider of " + other.transform);
		if (!other.transform.GetComponent<Arrow>() || rgbody2D.bodyType == RigidbodyType2D.Static)
		{
			Debug.Log("Quit");
			return;
		}

		rgbody2D.AddForce(((Vector2)other.transform.position - (Vector2)transform.position)* 
			(1/((Vector2)other.transform.position - (Vector2)transform.position).magnitude)  * affectingPower, ForceMode2D.Impulse);
		Debug.Log("Proceed");

		//this.enabled = false;
	}
	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, FieldRadius);
	}
}
