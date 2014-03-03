using UnityEngine;

public class Enemy : Entity
{
	public enum AIStyle
	{
		Hostile,
		Neutral,
		Feared
	}

	protected NavMeshAgent Nav = null;

	public Entity Target = null;

	public AIStyle Style = AIStyle.Hostile;

	protected AttackDict Attacks = new AttackDict();

	protected Attack NextAttack = null;

	protected float MinAttackRange { get { return NextAttack != null ? NextAttack.MinRange : 0.0f; } }
	protected float MaxAttackRange { get { return NextAttack != null ? NextAttack.MaxRange : 0.0f; } }

	protected override void Awake()
	{
		base.Awake();

		Nav = GetComponent<NavMeshAgent>();
		
		Team = Teams.Enemy;

		CanHurt = Teams.Player;
	}

	protected override void AI()
	{
		if (!HasControl)
		{
			Nav.Stop();
			return;
		}

		if (Target != null)
		{
			if (Style == AIStyle.Hostile)
			{
				Nav.SetDestination(Target.position);

				Vector3 Dist = Target.position - position;

				Dist.y = 0.0f;

				float SqrMag = Dist.sqrMagnitude;

				/*if (SqrMag < Mathf.Pow(Nav.radius + MinAttackRange, 2))
				{
					//back up or change attack
				}
				else */if (SqrMag < Mathf.Pow(Nav.radius + MaxAttackRange * 0.8f, 2))
				{
					// multiplied by 0.8 so the player can't immediately run out of range
					// max range should be at least 25% greater than the min range

					Nav.updatePosition = false;

					float AngleDiff = Mathf.Atan2(Tr.forward.z, Tr.forward.x) - Mathf.Atan2(Dist.z, Dist.x);

					if (AngleDiff < -Mathf.PI)
						AngleDiff += Mathf.PI * 2;

					if (Mathf.Abs(AngleDiff) < Mathf.PI * 0.25f)
					{
						PlayAnim("Attack");

						NextAttack = Attacks.GetRandom();
					}
				}
				else
					Nav.updatePosition = true;
			}
			else if (Style == AIStyle.Feared)
			{
				Vector3 Dist = position - Target.position;
				
				Dist.y = 0.0f;

				Nav.Move(Dist.normalized * Motor.movement.maxForwardSpeed);
			}
		}
	}

	public override void DropLoot()
	{
		Inventory.Spawn(position);
	}
}