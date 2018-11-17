/* Purpose: Interface to enforce animations onto class inheriting actor.
 * Authors: Jared Johannson, Joe Peaden
 */ 

public interface IActorControl
{
    // List of enforced actions
    void MoveHorizontal(float horizontalInput);
    void MoveVertical(float verticalInput);
    //void Sprint();
    //void Dodge();
    //void Jump();

    void ToggleWeapon();
    void LightAttack();
    void HeavyAttack();
    //void Block();

    void DamageTaken(float damage);
    void StaminaLost(float stamina);

    //void ChangePrimaryWeapon();
    //void ChangeSecondaryWeapon();
}