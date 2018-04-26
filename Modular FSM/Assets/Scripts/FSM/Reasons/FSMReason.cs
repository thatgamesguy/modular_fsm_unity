using UnityEngine;


public abstract class FSMReason
{
    public GlobalStateData.FSMTransistion Transition { get; set; }
    public GlobalStateData.FSMStateID GoToState { get; set; }
    public abstract bool ChangeState();
    public virtual void Enter()
    {
    }
    public virtual void Exit()
    {
    }

    // Used to determine if a character has a clean shot to the player.
    protected bool LayerInPath(Vector2 objPosition, Vector2 characterPos, LayerMask mask)
    {
        var heading = objPosition - characterPos;
        var distance = heading.magnitude;
        var dir = heading / distance;

        Ray2D ray = new Ray2D(characterPos, dir);
        Debug.DrawRay(ray.origin, ray.direction, Color.black);



        var hit = Physics2D.Raycast(ray.origin, ray.direction, distance, mask);
        if (hit.collider != null)
        {
            return true;
        }

        return false;

    }

    protected bool CloseToObject(Vector2 objPos, Vector2 characterPos, float distance)
    {
        return Vector2.Distance(characterPos, objPos) < distance;
    }
}

