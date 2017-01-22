using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class Character : ICharacter
{
    protected int maximumLifepoints;
    protected int currentLifepoints;
    protected string name;
    protected int defence;
    protected int speed;
    protected int attack;
    private HealthBar bar;

    public Character(int maximumHitpoints,
        int defence,
        int speed,
        string name,
        int attack)
    {
        this.maximumLifepoints
            = this.currentLifepoints
            = maximumHitpoints;
        this.name = name;
        if (defence <= 50)  //max defence is 50
        {
            this.defence = defence;
        }
        else
        {
            this.defence = 50;
        }
        this.speed = speed;
        this.attack = attack;
        this.bar = new HealthBar(this);
    }

    public string getName()
    {
        return this.name;
    }

    public int getSpeed()
    {
        return this.speed;
    }

    public int getDefence()
    {
        return this.defence;
    }


    public int getMaximumLifepoints()
    {
        return this.maximumLifepoints;
    }

    public int getCurrentLifepoints()
    {
        return this.currentLifepoints;
    }

    public int incrementLifepoints(int lifepoints)
    {
        if (isAlive())
        {
            this.currentLifepoints = this.currentLifepoints + lifepoints;
            return this.currentLifepoints;
        }
        else
        {
            return currentLifepoints;
        }
    }

    public int decrementlifepoints(int hitpoints)
    {
        int decrementValue = ((int)(hitpoints * (1 - this.defence / 100)) + 1);
        this.currentLifepoints = this.currentLifepoints - decrementValue;
        this.bar.handleBar();
        return decrementValue;
    }

    public bool isAlive()
    {
        return this.maximumLifepoints - this.currentLifepoints > 0;
    }

    public int getAttackValue(ATTACKS.attacks attack)
    {
        switch (attack)
        {
            case ATTACKS.attacks.WAVE_PUNCH:
                return ATTACKS.WAVE_PUNCH_DMG + this.attack;
            case ATTACKS.attacks.SHOCK_WAVE:
                return ATTACKS.SHOCK_WAVE_DMG + this.attack;
            case ATTACKS.attacks.KICK:
                return ATTACKS.KICK_DMG + this.attack;

            case ATTACKS.attacks.PUNCH:
                return ATTACKS.PUNCH_DMG + this.attack;
            default:
                return 0;
        }
    }

    public void collidedWithElementOfDeath()
    {
        this.currentLifepoints = 0;
    }
}
*/