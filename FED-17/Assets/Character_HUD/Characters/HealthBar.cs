using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField]
    private Image content;

    [SerializeField]
    private Text name;

    [SerializeField]
    private Text lifepoints;

    private Character character;

    public HealthBar(Character character)
    {
        this.character = character;
        this.name.text = character.getName();
        this.lifepoints.text = character.getMaximumLifepoints().ToString();
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    //	void Update () {
    //        handleBar();
    //	}

    public void handleBar()
    {
        content.fillAmount =
            (float)this.character.getCurrentLifepoints()
            / (float)this.character.getMaximumLifepoints();

        this.lifepoints.text = character.getCurrentLifepoints().ToString();
    }



}
