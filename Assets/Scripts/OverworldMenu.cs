using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OverworldMenu : MonoBehaviour
{
    public GameObject menuObject;
    public GameObject MainMenu;
    public GameObject StatsMenu;
    //public GameObject InvMenu;
    public int menuOpen;

    //Stat Menu Variables
    public CharacterStatHandler charStats;
    public GameObject bobObj, thozosObj, stevphenObj, janetObj;
    public TextMeshProUGUI johnHP, johnBash, johnSlash, johnPoke;
    public TextMeshProUGUI bobHP, bobBash, bobSlash, bobPoke;
    public TextMeshProUGUI thozosHP, thozosBash, thozosSlash, thozosPoke;
    public TextMeshProUGUI janetHP, janetBash, janetSlash, janetPoke;
    public TextMeshProUGUI stevphenHP, stevphenBash, stevphenSlash, stevphenPoke;
    public TextMeshProUGUI cLevelText, nLevelText, expToNext;
    public OmniDirectionalMovement johnMovement;

    void Start()
    {
        menuObject.SetActive(false);
        Debug.Log("Overworld Menu: " + this.gameObject.name);
    }

    public void handleMenu()
    {
        if (menuObject.activeSelf)
        {
            menuObject.SetActive(false);
            menuOpen = 0;
        }
        else
        {
            menuObject.SetActive(true);
            switchMenu(menuOpen);
        }
    }
    void Update()
    {
        if (menuObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
            {
                handleMenu();
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (menuOpen == 1)
                {
                    switchMenu(0);
                    menuOpen = 0;
                }
                else
                {
                    switchMenu(menuOpen + 1);
                    menuOpen++;
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                handleMenu();
            }
        }
    }

    //Menu Tab Handler
    public void switchMenu(int menu)
    {
        switch (menu)
        {
            case 0: //Main
                MainMenu.SetActive(true);
                StatsMenu.SetActive(false);
                //InvMenu.SetActive(false);
                break;
            case 1: //Stats
                MainMenu.SetActive(false);
                StatsMenu.SetActive(true);
                assignStats();
                //InvMenu.SetActive(false);
                break;
            //case 2: //Inventory
        }
    }

    //Stats Menu
    public void assignStats()
    {
        //Open Correct Stat Sheets
        if (johnMovement.bobActive)
        {
            bobObj.SetActive(true);
        }
        if (johnMovement.thozosActive)
        {
            thozosObj.SetActive(true);
        }
        if (johnMovement.janetActive)
        {
            janetObj.SetActive(true);
        }
        if (johnMovement.stephvenActive)
        {
            stevphenObj.SetActive(true);
        }
        //Set John's Stats
        johnHP.text = ("HP: " + charStats.John.currentHP.ToString() + "/" + charStats.John.characterHP.ToString());
        johnBash.text = ("Bash: " + ((int)(charStats.John.bashMultiplier * 100)).ToString() + "% (" + ((int)(25 * charStats.John.bashMultiplier)).ToString() + ")");
        johnSlash.text = ("Slash: " + ((int)(charStats.John.slashMultiplier * 100)).ToString() + "% (" + ((int)(15 * charStats.John.slashMultiplier)).ToString() + ")");
        johnPoke.text = ("Poke: " + ((int)(charStats.John.pokeMultiplier * 100)).ToString() + "% (" + ((int)(15 * charStats.John.pokeMultiplier)).ToString() + ")");
        //Set Bob's Stats
        bobHP.text = ("HP: " + charStats.Bob.currentHP.ToString() + "/" + charStats.Bob.characterHP.ToString());
        bobBash.text = ("Bash: " + ((int)(charStats.Bob.bashMultiplier * 100)).ToString() + "% (" + ((int)(25 * charStats.Bob.bashMultiplier)).ToString() + ")");
        bobSlash.text = ("Slash: " + ((int)(charStats.Bob.slashMultiplier * 100)).ToString() + "% (" + ((int)(15 * charStats.Bob.slashMultiplier)).ToString() + ")");
        bobPoke.text = ("Poke: " + ((int)(charStats.Bob.pokeMultiplier * 100)).ToString() + "% (" + ((int)(15 * charStats.Bob.pokeMultiplier)).ToString() + ")");
        //Set Thozos's Stats
        thozosHP.text = ("HP: " + charStats.Thozos.currentHP.ToString() + "/" + charStats.Thozos.characterHP.ToString());
        thozosBash.text = ("Bash: " + ((int)(charStats.Thozos.bashMultiplier * 100)).ToString() + "% (" + ((int)(25 * charStats.Thozos.bashMultiplier)).ToString() + ")");
        thozosSlash.text = ("Slash: " + ((int)(charStats.Thozos.slashMultiplier * 100)).ToString() + "% (" + ((int)(15 * charStats.Thozos.slashMultiplier)).ToString() + ")");
        thozosPoke.text = ("Poke: " + ((int)(charStats.Thozos.pokeMultiplier * 100)).ToString() + "% (" + ((int)(15 * charStats.Thozos.pokeMultiplier)).ToString() + ")");
        //Set Janet's Stats
        janetHP.text = ("HP: " + charStats.Janet.currentHP.ToString() + "/" + charStats.Janet.characterHP.ToString());
        janetBash.text = ("Bash: " + ((int)(charStats.Janet.bashMultiplier * 100)).ToString() + "% (" + ((int)(25 * charStats.Janet.bashMultiplier)).ToString() + ")");
        janetSlash.text = ("Slash: " + ((int)(charStats.Janet.slashMultiplier * 100)).ToString() + "% (" + ((int)(15 * charStats.Janet.slashMultiplier)).ToString() + ")");
        janetPoke.text = ("Poke: " + ((int)(charStats.Janet.pokeMultiplier * 100)).ToString() + "% (" + ((int)(15 * charStats.Janet.pokeMultiplier)).ToString() + ")");
        //Set Stevphen's Stats
        stevphenHP.text = ("HP: " + charStats.Stephven.currentHP.ToString() + "/" + charStats.Stephven.characterHP.ToString());
        stevphenBash.text = ("Bash: " + ((int)(charStats.Stephven.bashMultiplier * 100)).ToString() + "% (" + ((int)(25 * charStats.Stephven.bashMultiplier)).ToString() + ")");
        stevphenSlash.text = ("Slash: " + ((int)(charStats.Stephven.slashMultiplier * 100)).ToString() + "% (" + ((int)(15 * charStats.Stephven.slashMultiplier)).ToString() + ")");
        stevphenPoke.text = ("Poke: " + ((int)(charStats.Stephven.pokeMultiplier * 100)).ToString() + "% (" + ((int)(15 * charStats.Stephven.pokeMultiplier)).ToString() + ")");
        //Set Level Text
        cLevelText.text = ("Level " + charStats.partyLevel.ToString());
        nLevelText.text = ("Exp to level " + (charStats.partyLevel + 1).ToString() + ":");
        expToNext.text = ((charStats.expToNext - charStats.partyEXP).ToString() + " EXP");
    }

    //Inventory Menu
}
