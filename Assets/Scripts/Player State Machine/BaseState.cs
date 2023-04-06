using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public abstract void EnterState(StateController player);

    public abstract void UpdateState(StateController player);

    public abstract void Move(StateController player, Vector2 inVec);

}
