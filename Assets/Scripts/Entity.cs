using UnityEngine;
using System.Collections.Generic;

public enum Teams
{
	Player,
	Enemy
}

public enum EntityState
{
	None,
	Dodging,
	Attacking,
	Staggering,
	Plunging,
	KnockedDown
}

[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterMotor))]
public abstract class Entity : TransformObject
{
	protected Animation Anim = null;

	protected CharacterController Controller = null;

	protected CharacterMotor Motor = null;

	public int MaxHealth = 100;

	private int _Health = 100;

	public int Health
	{
		get { return _Health; }
		set
		{
			if (value <= 0)
			{
				_Health = 0;

				if (OnDeath())
					DropLoot();
			}
			else
				_Health = value;
		}
	}

	public int Defense = 10;

	public int Stability = 10;

	public int Faith = 0;

	public Teams Team = Teams.Player;

	public Teams CanHurt = Teams.Enemy;

	public EntityState State = EntityState.None;

	public Weapon EquippedWeapon = null;

	public Transform WeaponBind = null;

	public ItemCollection Inventory = null;

	public Vector3 LastGroundPos { get; set; }

	public bool HasControl { get { return Controller.isGrounded && State == EntityState.None; } }

	protected override void Awake()
	{
		base.Awake();
		
		Anim = animation;

		Controller = GetComponent<CharacterController>();

		Motor = GetComponent<CharacterMotor>();
	}

	protected void Start()
	{
		AddEvent("Attack", 0.0f, (O) => State = EntityState.Attacking);
		AddEvent("Attack", 1.0f, (O) => State = EntityState.None);
		
		AddEvent("Dodge", 0.0f, (O) => State = EntityState.Dodging);
		AddEvent("Dodge", 1.0f, (O) => State = EntityState.None);
		
		AddEvent("Stagger", 0.0f, (O) => State = EntityState.Staggering);
		AddEvent("Stagger", 1.0f, (O) => State = EntityState.None);

		// plunge animation only ends when the player is hit or lands
		AddEvent("Plunge", 0.0f, (O) => State = EntityState.Plunging);

		// knockdown animation only ends when the player lands
		AddEvent("KnockDown", 0.0f, (O) => State = EntityState.KnockedDown);
		
		EquippedWeapon = (Weapon)Instantiate(EquippedWeapon);
		EquippedWeapon.parent = WeaponBind;
		EquippedWeapon.localPosition = Vector3.zero;
		EquippedWeapon.Owner = this;
	}
	
	protected virtual void FixedUpdate()
	{
		AI();

		if (Controller.isGrounded)
			LastGroundPos = Tr.position;
	}

	protected virtual void AI()
	{

	}

	public bool PlayAnim(string AnimName)
	{
		if (Anim[AnimName] != null)
		{
			Anim.Play(AnimName);
			return true;
		}

		return false;
	}

	public void AddEvent(string AnimName, float AnimTime, System.Action<object> Function = null)
	{
		AnimationState AS = Anim[AnimName];

		if (AS != null)
		{
			if (AnimTime < 0.0f)
				AnimTime = AS.length;

			AS.clip.AddEvent(new AnimationEvent() { functionName = "Utility.RunEvent", time = AnimTime * AS.length, objectReferenceParameter = new AnimEvent(Function) });
		}
	}

	public virtual int CalcDamage(int Damage, int ArPen = 0)
	{
		return (int)(Damage / (1.0f + (Defense - ArPen) * 0.01f));
	}

	public virtual void OnHit(Projectile P)
	{
		Health -= CalcDamage(P.Damage, P.ArPen);

		if (P.Force > Stability)
			PlayAnim("Stagger");
	}

	public virtual bool OnDeath()
	{
		return true;
	}

	public virtual void DropLoot()
	{

	}
}