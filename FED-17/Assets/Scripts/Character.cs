using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public static int WAVE_PUNCH_DMG = 10;
    public static int SHOCK_WAVE_DMG = 8;
    public static int KICK_DMG = 5;
    public static int PUNCH_DMG = 3; 

    public enum attacks
    {
        WAVE_PUNCH,
        SHOCK_WAVE,
        KICK,
        PUNCH
    }

	[SerializeField]
	int maximumLifepoints;
	[SerializeField]
    int currentLifepoints;
	[SerializeField]
    string playerName;
	[SerializeField]
    int defence;
	[SerializeField]
    int speed;
	[SerializeField]
    int attack;

	private HealthBar bar;

	void Start()
	{
		currentLifepoints = maximumLifepoints;
        if (defence > 50)  //max defence is 50
        {
            defence = 50;
        }
		//healthBar = new HealthBar(this);
	}

	public string getName()
    {
        return this.playerName;
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
        //this.bar.handleBar();
        return decrementValue;
    }

    public bool isAlive()
    {
        return this.maximumLifepoints - this.currentLifepoints > 0;
    }

   public int getAttackValue(Character.attacks attack)
    {
        switch(attack)
        {
            case Character.attacks.WAVE_PUNCH:
                return Character.WAVE_PUNCH_DMG + this.attack;
            case Character.attacks.SHOCK_WAVE:
                return Character.SHOCK_WAVE_DMG + this.attack;
            case Character.attacks.KICK:
                return Character.KICK_DMG + this.attack;
            case Character.attacks.PUNCH:
                return Character.PUNCH_DMG + this.attack;
            default:
                return 0;
        }
    } 

    public void collidedWithElementOfDeath()
    {
        this.currentLifepoints = 0;
    }
}
