using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{

    static GameStates currentGameState = GameStates.MainMenu;
    static BattleTurn currentBattleTurn = BattleTurn.PlayerAttack;
    [SerializeField] private int health = 100;
    [SerializeField] private int enemyHealth = 100;
    [SerializeField] private int damage;
    [SerializeField] private int enemyDamage;
    [SerializeField] private GameObject[] menuPanels;
    [SerializeField] private Button[] BattleButtons;
    [SerializeField] private bool playerHeavyAttack = false;
    [SerializeField] private bool playerQuickAttack = false;
    [SerializeField] private bool enemyHeavyAttack = false;
    [SerializeField] private bool enemyQuickAttack = false;
    [SerializeField] private Text attack;
    [SerializeField] private Text uhealth;
    [SerializeField] private Text eHealth;
    private bool endTrigger;

    #region States
    public enum BattleTurn
    {
        PlayerAttack,
        Enemy,
        PlayerDefend,
    }
    public enum GameStates
    {
        MainMenu,
        Battle,
        Victory,
        Defeat,
        OptionsMenu
    }

    #endregion

    #region Game States
    public void SetState(GameStates newState)
    {
        currentGameState = newState;
        for (int i = 0; i < menuPanels.Length; i++)
        {
            menuPanels[i].SetActive(false);
        }
        switch (currentGameState)
        {
            case GameStates.MainMenu:
                menuPanels[0].SetActive(true);
                break;
            case GameStates.OptionsMenu:
                menuPanels[1].SetActive(true);
                break;
            case GameStates.Battle:
                menuPanels[2].SetActive(true);
                break;
            case GameStates.Defeat:
                menuPanels[3].SetActive(true);
                break;
            case GameStates.Victory:
                menuPanels[4].SetActive(true);
                break;
            default:
                currentGameState = GameStates.MainMenu;
                menuPanels[0].SetActive(true);
                break;
        }
    }

    public void OpenOptionsMenu()
    {
        SetState(GameStates.OptionsMenu);
    }
    public void PlayGame()
    {
        SetState(GameStates.Battle);
    }
    public void MainMenu()
    {
        SetState(GameStates.MainMenu);
    }
    private void Update()
    {
        if (endTrigger == false)
        {
            if (health <= 0)
            {
                SetState(GameStates.Defeat);
                endTrigger = true;

            }

            if (enemyHealth <= 0)
            {
                SetState(GameStates.Victory);
                endTrigger = true;

            }
        }
        Turn(currentBattleTurn);

        uhealth.text = health.ToString();
        eHealth.text = enemyHealth.ToString();

    }
    #endregion

    #region Battle State

    void Turn(BattleTurn turn)
    {
        currentBattleTurn = turn;
        for (int i = 0; i < BattleButtons.Length; i++)
        {
            BattleButtons[i].interactable = false;
        }

        switch (currentBattleTurn)
        {
            case BattleTurn.PlayerAttack:
                BattleButtons[0].interactable = true;
                BattleButtons[1].interactable = true;
                break;
            case BattleTurn.Enemy:
                enemysTurn();
                break;
            case BattleTurn.PlayerDefend:
                BattleButtons[2].interactable = true;
                BattleButtons[3].interactable = true;
                break;

            default:
                currentBattleTurn = BattleTurn.PlayerAttack;
                BattleButtons[0].interactable = true;
                BattleButtons[1].interactable = true;
                break;

        }

    }


    #endregion

    #region Battle Controls
    public void quickAttack()
    {
        int qd = Random.Range(11, 21);

        damage = qd;

        playerQuickAttack = true;

        Turn(BattleTurn.Enemy);
    }

    public void heavyAttack()
    {

        int hd = Random.Range(21, 31);

        damage = hd;

        playerHeavyAttack = true;

        Turn(BattleTurn.Enemy);
    }
    public void shield()
    {

        int bl = Random.Range(1, 11);

        if (bl >= 5 && (enemyQuickAttack = true))
        {
            enemyDamage = 0;
        }
            health -= enemyDamage;

        Turn(BattleTurn.PlayerAttack);
    }
    public void dodge()
    {
        int dg = Random.Range(1, 11);

        if (dg >= 3 && (enemyHeavyAttack = true))
        {
            enemyDamage = 0;
        }

        health -= enemyDamage;


        Turn(BattleTurn.PlayerAttack);
    }
    public void enemysTurn()
    {

        enemyHeavyAttack = false;
        enemyQuickAttack = false;

        int bd = Random.Range(1, 11);

        if (bd >= 3 && (playerQuickAttack = true))
            damage = 0;

        if(bd >= 4 && (playerHeavyAttack = true))
            damage = 0;

        enemyHealth -= damage;

        int horq = Random.Range(1, 3);
        

        if (horq == 1)
        {
            enemyDamage = Random.Range(11, 21);
            attack.text = "Enemy Is Doing A Quick Attack";
            enemyQuickAttack = true;
        }
        if (horq == 2)
        {
            enemyDamage = Random.Range(21, 31);
            attack.text = "Enemy Is Doing A Heavy Attack";
            enemyHeavyAttack = true;
        }

        playerQuickAttack = false;
        playerHeavyAttack = false;

        Turn(BattleTurn.PlayerDefend);

    }
    #endregion

}




