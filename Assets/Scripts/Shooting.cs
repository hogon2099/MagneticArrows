using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
	public Transform Gunpoint;
	public Rigidbody2D ArrowPrefab;
	public float ShotStrength = 10f;
	public float PowerOfMagnet = 5;
	private Stack<GameObject> arrows = new Stack<GameObject>();
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y).normalized;
		direction *= ShotStrength;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		this.transform.rotation = Quaternion.Euler(0, 0, angle);

		if(Input.GetMouseButtonDown(1))
		{
			if (arrows.Count > 0)
			{
				GameObject.Destroy(arrows.Peek());
				arrows.Pop();
			}
		}

		if (Input.GetMouseButtonDown(0))
		{
			Rigidbody2D clone = Instantiate(ArrowPrefab.GetComponent<Rigidbody2D>(), Gunpoint.position, Quaternion.Euler(0, 0, this.transform.eulerAngles.z)) as Rigidbody2D;
			clone.AddForce(direction * ShotStrength, ForceMode2D.Impulse);
			clone.GetComponent<Arrow>().affectingPower = PowerOfMagnet;
			arrows.Push(clone.gameObject);
		}
	}
}
