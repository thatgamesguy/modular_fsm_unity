using UnityEngine;
using System.Collections;

[RequireComponent (typeof(FSM))]
public class ExampleController : MonoBehaviour
{
	private FSM _statemachine;
	
	void Start ()
	{
		_statemachine = GetComponent<FSM> ();
	}

	/// <summary>
	/// This performs a transistion from one state to another and is invoked in an individual states Reason method.
	/// </summary>
	public void SetTransistion (GlobalStateData.FSMTransistion tran)
	{
		_statemachine.PerformTransition (tran);
	}
	
	// Update is called once per frame
	void Update ()
	{
		_statemachine.CurrentState.Reason ();
		_statemachine.CurrentState.Act ();
	}
}
