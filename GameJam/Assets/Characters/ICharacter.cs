using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter {

    /*returns the name of the character*/
    string getName();

    /*returns the maximum hitpoints of the character*/
    int getMaximumLifepoints();

    /*returns the hp after incrementing
     int hitponts: how many hitpoints should be added*/
    int incrementLifepoints(int lifepoints);

    /*returns the hp after decrementing
     int hitpoints: how many hitpoints should be subtracted*/
    int decrementlifepoints(int hitpoints);

    /*returns the speed of the character*/
    int getSpeed();

    /*returns the defence of the character*/
    int getDefence();

    /*returns true if the character is alive*/
    bool isAlive();

    /*Returns the Value of an attack*/
    int getAttackValue(ATTACKS.attacks attack);
   
}
