using UnityEngine;

/// <summary>
/// An interface for different AI states
/// </summary>
/// 
public interface IEnemyState
{
    /// <summary>
    /// If the character is at the current state, then this method will be executed in the Update() method of monobehavior.
    /// </summary>
    void UpdateState();

    void OnTriggerEnter(Collider other);

    void OnCollisionEnter(Collision other);

    void OnTriggerExit(Collider other);

    /// <summary>
    /// Transition to Wander state
    /// </summary>
    void ToWanderState();

    /// <summary>
    /// Transition to Chase state
    /// </summary>
    void ToChaseState();

    /// <summary>
    /// Transition to Idle state
    /// </summary>
    void ToIdleState();

    /// <summary>
    /// Transition to Arrive state, in this state, the AI will simply go to a specific point in the map and transition to idle state
    /// </summary>
    void ToArriveState();
}