using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Access to UI

public class BattleManager : MonoBehaviour {
    public BattleMenu currentMenu;

    [Header("Selection")]
    public GameObject SelectionMenu;
    public GameObject SelectionInfo;

    [Space(10)]
    [Header("Selection Menu")]
    public Text fight;
    private string fightT;
    public Text bag;
    private string bagT;
    public Text pokemon;
    private string pokemonT;
    public Text run;
    private string runT;

    public BasePokemon userPokemon;

    [Space(10)]
    [Header("Moves")]
    public GameObject movesMenu;
    public GameObject moveDetailsBlank;
    public GameObject moveDetails;

    [Space(10)]
    public Text moveO;
    private string move1T;
    public Text moveT;
    private string move2T;
    public Text moveTH;
    private string move3T;
    public Text moveF;
    private string move4T;

    [Space(10)]
    [Header("Sub UI")]
    public Text name;
    public Text userLvl;
    public GameObject uPanel;
    public Text PP;
    public Text pType;
    public Text mHp;
    public Text cHp;
    
    public Image eBar;
    private float eCHP;

    public GameObject ePanel;

    [Space(10)]
    [Header("Movelist")]
    public BaseAttacks m1;
    public BaseAttacks m2;
    public BaseAttacks m3;
    public BaseAttacks m4;

    [Space(10)]
    [Header("Info")]
    public GameObject InfoMenu;
    public Text dialogueInfo;

    [Space(10)]
    [Header("Navigation")]
    public int currentSelection;

    [Space(10)]
    [Header("Overworld")]
    public GameObject playerCamera;
    public GameObject battleCamera;
    public GameObject player;
     
    private GameManager gm;
    private BasePokemon encounterPokemon;
    bool appeared;
    bool endBattle = false;
    public turnState battleState;

    void Start() {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        encounterPokemon = gm.ePoke;
        
        userPokemon = userPokemon.GetComponent<BasePokemon>();
        m1 = m1.GetComponent<BaseAttacks>();
        m2 = m2.GetComponent<BaseAttacks>();
        m3 = m3.GetComponent<BaseAttacks>();
        m4 = m4.GetComponent<BaseAttacks>();

        eCHP = encounterPokemon.maxHP;
        encounterPokemon.currentHP = encounterPokemon.maxHP;
        userPokemon.currentHP = userPokemon.maxHP;
        
        name.text = userPokemon.name.ToString();
        cHp.text = userPokemon.currentHP.ToString();
        mHp.text = userPokemon.maxHP.ToString();
        userLvl.text = userPokemon.level.ToString();

        move1T = m1.attackName;
        move2T = m2.attackName;
        move3T = m3.attackName;
        move4T = m4.attackName;

        fightT = fight.text;
        bagT = bag.text;
        pokemonT = pokemon.text;
        runT = run.text;
    
        appeared = true;
    }

    void Update() {
        if (appeared == true) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                uPanel.gameObject.SetActive(true);
                ePanel.gameObject.SetActive(true);
                ChangeMenu(BattleMenu.Selection);
                currentSelection = 0;
                battleState = turnState.userTurn;                
                appeared = false;
            }     
        }

        if (appeared == false) {
            switch (battleState) {
                case (turnState.userTurn):                         
            Navigate();
            switch (currentMenu)  {
                case BattleMenu.Fight:
                    if (Input.GetKeyDown(KeyCode.B)) {
                        ChangeMenu(BattleMenu.Selection);
                        currentSelection = 1;
                    }
                    FightMenu();
                    break;

                case BattleMenu.Selection:
                    SelectMenu();
                    break;
            }        
                    break;
                case (turnState.enemyTurn):                                          
                        EnemyTurn();
                        battleState = turnState.enemyStop;
                    break;
                case (turnState.enemyStop):
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        battleState = turnState.userTurn;
                        ChangeMenu(BattleMenu.Selection);
                    }
                    break;
                case (turnState.userStop):
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        if (endBattle == false)
                        {
                            battleState = turnState.enemyTurn;
                        }
                        else if (endBattle == true)
                        {
                            ChangeMenu(BattleMenu.Run);
                        }
                    }
                    break;       
            }
        }
    }
 

    public void UpdateMoveInfo(BaseAttacks m) {
        PP.text = m.pp.ToString();
        pType.text = m.moveType.ToString();
    }

    // Arrow navigation
    public void Navigate()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentSelection == 3 || currentSelection == 0)
            {
                currentSelection = 1;
            }
            else if (currentSelection == 4 || currentSelection == 0)
            {
                currentSelection = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentSelection == 1 || currentSelection == 0)
            {
                currentSelection = 3;
            }
            else if (currentSelection == 2 || currentSelection == 0)
            {
                currentSelection = 4;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentSelection == 1 || currentSelection == 0)
            {
                currentSelection = 2;
            }
            else if (currentSelection == 3 || currentSelection == 0)
            {
                currentSelection = 4;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentSelection == 2 || currentSelection == 0)
            {
                currentSelection = 1;
            }
            else if (currentSelection == 4 || currentSelection == 0)
            {
                currentSelection = 3;
            }
        }

    }

    public void ChangeMenu(BattleMenu m) {
          currentMenu = m;
        switch (m) {
            case BattleMenu.Selection:
                SelectionMenu.gameObject.SetActive(true);
                SelectionInfo.gameObject.SetActive(true);        
                movesMenu.gameObject.SetActive(false);
                moveDetailsBlank.gameObject.SetActive(true);
                moveDetails.gameObject.SetActive(false);
                InfoMenu.gameObject.SetActive(false);
                break;
            case BattleMenu.Fight:
                SelectionMenu.gameObject.SetActive(false);
                SelectionInfo.gameObject.SetActive(false);
                movesMenu.gameObject.SetActive(true);
                moveDetails.gameObject.SetActive(true);
                moveDetailsBlank.gameObject.SetActive(false);
                InfoMenu.gameObject.SetActive(false);
                break;
            case BattleMenu.Info:
                SelectionMenu.gameObject.SetActive(false);
                SelectionInfo.gameObject.SetActive(true);
                movesMenu.gameObject.SetActive(false);
                moveDetailsBlank.gameObject.SetActive(true);
                moveDetails.gameObject.SetActive(false);
                InfoMenu.gameObject.SetActive(false);
                break;
            case BattleMenu.Run:
                SelectionMenu.gameObject.SetActive(false);
                SelectionInfo.gameObject.SetActive(false);
                movesMenu.gameObject.SetActive(false);
                moveDetails.gameObject.SetActive(false);
                moveDetailsBlank.gameObject.SetActive(false);
                InfoMenu.gameObject.SetActive(false);
                battleCamera.SetActive(false);
                playerCamera.SetActive(true);         
                player.GetComponent<PlayerMovement>().isAllowedToMove = true;
                break;           
        }
    }

    public void FightMenu() {
        switch (currentSelection) {
            case 1:
                moveO.text = "> " + move1T;
                moveT.text = move2T;
                moveTH.text = move3T;
                moveF.text = move4T;
                UpdateMoveInfo(m1);
                if (Input.GetKeyDown(KeyCode.Space)) {
                    UpdateDialogue(userPokemon, m1);
                    eTakeDmg(encounterPokemon, m1);
                    FaintCheck(encounterPokemon);
                        battleState = turnState.userStop;

                }
                break;

            case 2:
                moveO.text = move1T;
                moveT.text = "> " + move2T;
                moveTH.text = move3T;
                moveF.text = move4T;
                UpdateMoveInfo(m2);
                if (Input.GetKeyDown(KeyCode.Space)) {
                    UpdateDialogue(userPokemon, m2);
                    eTakeDmg(encounterPokemon, m2);
                    FaintCheck(encounterPokemon);
                        battleState = turnState.userStop;

                }
                break;
            case 3:
                moveO.text = move1T;
                moveT.text = move2T;
                moveTH.text = "> " + move3T;
                moveF.text = move4T;
                UpdateMoveInfo(m3);
                if (Input.GetKeyDown(KeyCode.Space)) {
                    UpdateDialogue(userPokemon, m3);
                    eTakeDmg(encounterPokemon, m3);
                   FaintCheck(encounterPokemon);                 
                        battleState = turnState.userStop;
                
                }
                break;
            case 4:
                moveO.text = move1T;
                moveT.text = move2T;
                moveTH.text = move3T;
                moveF.text = "> " + move4T;
                UpdateMoveInfo(m4);
                if (Input.GetKeyDown(KeyCode.Space)) {
                    UpdateDialogue(userPokemon, m4);
                    eTakeDmg(encounterPokemon, m4);
                   FaintCheck(encounterPokemon);
                    battleState = turnState.userStop;
                }
                break;
        }
    }

    public void SelectMenu() {
        switch (currentSelection) {
            case 1:
                fight.text = "> " + fightT;
                bag.text = bagT;
                pokemon.text = pokemonT;
                run.text = runT;
                if (Input.GetKeyDown(KeyCode.Space)) {
                    ChangeMenu(BattleMenu.Fight);
                    currentSelection = 1;
                }
                break;
            case 2:
                fight.text = fightT;
                bag.text = "> " + bagT;
                pokemon.text = pokemonT;
                run.text = runT;
                break;
            case 3:
                fight.text = fightT;
                bag.text = bagT;
                pokemon.text = "> " + pokemonT;
                run.text = runT;
                break;
            case 4:
                fight.text = fightT;
                bag.text = bagT;
                pokemon.text = pokemonT;
                run.text = "> " + runT;
                if (Input.GetKeyDown(KeyCode.Space)) {
                    ChangeMenu(BattleMenu.Run);
                }
                break;
        }
    }

    public void EnemyTurn() {
        int rng = Random.Range(1,4);
        switch (rng) {
            case 1:
                UpdateDialogue(encounterPokemon, m1);
                uTakeDmg(userPokemon, m1);
                break;
            case 2:
                UpdateDialogue(encounterPokemon, m2);
                uTakeDmg(userPokemon, m2);
                break;
            case 3:
                UpdateDialogue(encounterPokemon, m3);
                uTakeDmg(userPokemon, m3);
                break;
            case 4:
                UpdateDialogue(encounterPokemon, m4);
                uTakeDmg(userPokemon, m4);
                break;
        }
        FaintCheck(userPokemon);
    }
  
    public void FaintCheck(BasePokemon u) {
        if (u.currentHP <= 0) {
            cHp.text = "0";
            
            FaintDialogue(u);
        }
    }

    public void UpdateDialogue(BasePokemon u, BaseAttacks m)
    {
        ChangeMenu(BattleMenu.Info);
        dialogueInfo.text = u.name.ToString() + " used " + m.attackName.ToString() + "!";  
    }

    public void FaintDialogue(BasePokemon u)
    {
        dialogueInfo.text = u.name.ToString() + " has fainted!";
        endBattle = true;

    }
    public void uTakeDmg(BasePokemon u, BaseAttacks a) {
        u.TakeDmg(a.attackDamage);
    }
    public void eTakeDmg(BasePokemon u, BaseAttacks a) {
        eCHP -= a.attackDamage;
        eBar.fillAmount = eCHP / u.maxHP;
        if (eBar.fillAmount <= 0) {
            dialogueInfo.text = u.name.ToString() + " has fainted!";

        }
    }

    public void EndBattle()
    {
        SelectionInfo.gameObject.SetActive(true);
        moveDetailsBlank.gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectionMenu.gameObject.SetActive(false);
            SelectionInfo.gameObject.SetActive(false);
            movesMenu.gameObject.SetActive(false);
            moveDetails.gameObject.SetActive(false);
            moveDetailsBlank.gameObject.SetActive(false);
            InfoMenu.gameObject.SetActive(false);
            battleCamera.SetActive(false);
            playerCamera.SetActive(true);
            player.GetComponent<PlayerMovement>().isAllowedToMove = true;

        }
    }
}

public enum BattleMenu {
    Selection,
    
    Pokemon,
    Bag,
    Fight,
    Info,
    Run
}

public enum turnState {
    userTurn,
    enemyTurn,
    userStop,
    enemyStop
  
}