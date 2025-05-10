using UnityEngine;

public class InputHandler : MonoBehaviour
{

    private InputSystem_Actions playerInput;
    private InputSystem_Actions.PlayerActions playerActions;
    private InputSystem_Actions.UIActions UIActions;


    private GameLogic gameLogic;

    private void Awake()
    {
        playerInput = new InputSystem_Actions();
        playerActions = playerInput.Player;
        UIActions = playerInput.UI;


        gameLogic = GetComponent<GameLogic>();


        #region Enables
        playerActions.Look.Enable();
        playerActions.Jump.Enable();
        playerActions.Move.Enable();
        playerActions.Attack.Enable();
       //playerActions.Weapons.Enable();

        UIActions.Cancel.Enable();
        #endregion

    }

    private void Update()
    {
        if (Time.timeScale == 0)
            return;
       // gameLogic.HandleWeaponInput(playerActions.Weapons.WasPressedThisFrame(), (int)playerActions.Weapons.ReadValue<float>());
        if (playerActions.Attack.WasPressedThisFrame())
            gameLogic.HandleMouseClick();

    }
}