using Unity;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Element : MonoBehaviour {

  [SerializeField]
  private float dashRange;
  public float DashRange { get { return dashRange; } }

  [SerializeField]
  private float attackRange;
  public float AttackRange { get { return attackRange; } }

  [SerializeField]
  private float blockRange;
  public float BlockRange { get { return blockRange; } }


  protected bool executing;
  public bool Executing { get { return executing; } }

  [SerializeField]
  protected Ability abilities;

  public abstract void Dash(InputAction.CallbackContext context);

  public abstract void Attack(InputAction.CallbackContext context);

  public abstract void Block(InputAction.CallbackContext context);
}