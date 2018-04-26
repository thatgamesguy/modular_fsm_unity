using UnityEngine;
using System.Collections.Generic;

public class FSM : MonoBehaviour
{

    private static readonly string SCRIPT_NAME = typeof(FSM).Name;

    private List<FSMState> fsmStates = new List<FSMState>();

    public GlobalStateData.FSMStateID PreviousStateID
    {
        get
        {
            return previousState.ID;
        }
    }

    public GlobalStateData.FSMStateID CurrentStateID
    {
        get
        {
            return currentState.ID;
        }
    }

    private FSMState previousState;
    public FSMState PreviousState
    {
        get
        {
            return previousState;
        }
    }

    private FSMState currentState;
    public FSMState CurrentState
    {
        get
        {
            return currentState;
        }
    }

    private FSMState defaultState;



    void OnDisable()
    {
        if (currentState != null)
            currentState.Exit();
    }

    public void AddState(FSMState state)
    {

        if (state == null)
        {
            Debug.LogWarning(SCRIPT_NAME + ": null state not allowed");
            return;
        }

        // First State inserted is also the Initial state
        //   the state the machine is in when the simulation begins
        if (fsmStates.Count == 0)
        {
            fsmStates.Add(state);
            currentState = state;
            defaultState = state;
            return;
        }

        // Add the state to the List if it´s not inside it
        foreach (FSMState tmpState in fsmStates)
        {
            if (state.ID == tmpState.ID)
            {
                Debug.LogError(SCRIPT_NAME + ": Trying to add a state that was already inside the list, " + state.ID);
                return;
            }
        }

        //If no state in the current then add the state to the list
        fsmStates.Add(state);
    }

    public void DeleteState(GlobalStateData.FSMStateID stateID)
    {

        if (stateID == GlobalStateData.FSMStateID.None)
        {
            Debug.LogWarning(SCRIPT_NAME + ": no state id");
            return;
        }


        // Search the List and delete the state if it´s inside it
        foreach (FSMState state in fsmStates)
        {
            if (state.ID == stateID)
            {
                fsmStates.Remove(state);
                return;
            }
        }

        Debug.LogError(SCRIPT_NAME + ": The state passed was not on the list");

    }

    public void PerformTransition(GlobalStateData.FSMTransistion trans)
    {
        // Check for NullTransition before changing the current state
        if (trans == GlobalStateData.FSMTransistion.None)
        {
            Debug.LogError(SCRIPT_NAME + ": Null transition is not allowed");
            return;
        }

        // Check if the currentState has the transition passed as argument
        GlobalStateData.FSMStateID id = currentState.GetOutputState(trans);
        if (id == GlobalStateData.FSMStateID.None)
        {
            Debug.LogError(SCRIPT_NAME + ": Current State does not have a target state for this transition");
            return;
        }


        // Update the currentStateID and currentState		
        //currentStateID = id;
        foreach (FSMState state in fsmStates)
        {
            if (state.ID == id)
            {
                // Store previous state and call exit method.
                previousState = currentState;
                previousState.Exit();

                // Update current state and call enter method.
                currentState = state;
                currentState.Enter();

                break;
            }
        }
    }

    public void ClearStates()
    {
        fsmStates.Clear();
    }

    public void Reset()
    {
        currentState = defaultState;
        if (currentState != null)
        {
            currentState.Enter();
        }
    }


}

