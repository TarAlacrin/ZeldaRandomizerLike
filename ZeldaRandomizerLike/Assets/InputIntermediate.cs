using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputIntermediate : MonoBehaviour {

	public CharacterController charController;

	// Use this for initialization
	void Start () {
		
	}
	

	// Update is called once per frame
	void Update () {


		Vector2 movementAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		float axisLengt = movementAxis.magnitude;
		Vector2 axisNormal = movementAxis.normalized;
		axisNormal.x = Mathf.Round(axisNormal.x);
		axisNormal.y = Mathf.Round(axisNormal.y);
		movementAxis = axisNormal.normalized*axisLengt;


		charController.MoveInputPressed(movementAxis);
		//charController.FaceDirection( Vector2.ClampM//Vector2.ClampMagnitude(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"))*0.5f, 1f));
		//Vector2 lookAxis = new Vector2(Input.GetAxisRaw("Look X"), Input.GetAxisRaw("Look Y"));

		//axisLengt = lookAxis.magnitude;
		//axisNormal = lookAxis.normalized;
		////axisNormal.x = Mathf.Round(axisNormal.x);
		//axisNormal.y = Mathf.Round(axisNormal.y);
		//lookAxis = axisNormal.normalized * axisLengt;

		//charController.LookInputPressed(lookAxis);


		if (Input.GetButtonDown("Attack"))
			charController.TryAttack();
		if (Input.GetButtonDown("Item1"))
			charController.TryUseItem();
	}

}
