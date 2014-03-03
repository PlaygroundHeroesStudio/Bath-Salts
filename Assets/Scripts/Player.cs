using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class Player : Entity
{
	public Transform CamTr = null;

	public KeyCode KeyInteract = KeyCode.E;
	public KeyCode KeyDodge = KeyCode.Space;

	public int MouseAttack = 0;

	protected bool Interacting = false;

	protected List<GameObject> Interactables = new List<GameObject>();

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void AI()
	{
		if (!HasControl || Interacting)
			return;

		Vector3 Vel = Vector3.zero;
		
		Vector3 Forward = CamTr.forward;
		Forward.y = 0.0f;
		Forward.Normalize();
		
		Vector3 Right = Vector3.Cross(Forward, Vector3.up);

		Vel -= Right * Motor.movement.maxSidewaysSpeed * Input.GetAxis("Horizontal");
		Vel += Forward * Motor.movement.maxForwardSpeed * Input.GetAxis("Vertical");

		if (Interactables.Count > 0 && Input.GetKey(KeyInteract))
		{
			Interacting = true;

			foreach (GameObject GI in Interactables)
				foreach (Interactable I in GI.GetComponents<Interactable>())
					I.OnInteract(this);
		}

		if (Input.GetKey(KeyDodge) && PlayAnim("Dodge"))
		{
			float Length = Vel.magnitude;
			
			if (Length <= 0.0f)
				Vel = Forward * Motor.movement.maxForwardSpeed;
			else
				Vel = Vel / Length * Motor.movement.maxForwardSpeed;
		}

		if (Input.GetMouseButton(MouseAttack) && PlayAnim("Attack"))
			EquippedWeapon.ResetHits();

		Motor.movement.velocity = Vel;

		Interactables.Clear();
	}

	protected void OnTriggerEnter(Collider C)
	{
		if (C.GetComponent<Interactable>() != null)
			Interactables.Add(C.gameObject);
	}

	public void Save()
	{
		MemoryStream MS = new MemoryStream();

		BinaryWriter BW = new BinaryWriter(MS);

		BW.Write(Health);
		BW.Write(MaxHealth);
		BW.Write(Defense);
		BW.Write(Stability);
		BW.Write(Faith);
		BW.Write(EquippedWeapon.Value);
		BW.Write(EquippedWeapon.Durability);
		BW.Write(EquippedWeapon.MaxDurability);
		BW.Write(LastGroundPos.x);
		BW.Write(LastGroundPos.y);
		BW.Write(LastGroundPos.z);

		BW.Write(Inventory.Items.Count);

		foreach (Item I in Inventory.Items)
			I.Save(BW);

		BW.Close();

		PlayerPrefs.SetString("Save", System.Text.Encoding.Default.GetString(MS.GetBuffer()));
	}
}