using UnityEngine;

public class CamFollow : MonoBehaviour
{
	Transform Tr = null;

	public Transform Target = null;

	Vector3 Offset = Vector3.zero;

	void Awake()
	{
		Tr = transform;

		Offset = Tr.position - Target.position;
	}
	
	void Update()
	{
		Tr.position = Target.position + Offset;
	}
}