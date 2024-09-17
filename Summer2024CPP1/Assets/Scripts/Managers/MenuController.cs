using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    //Menu Reference that is in the scene
    public BaseMenu[] allMenus;

    //State reference for the menus - this could be seperated into a different file but this makes it easy for the dictionary to find the appropriate menu
    public enum MenuStates
    {
        MainMenu, Settings, Pause, InGame
    }

    //current state reference
    BaseMenu currentState;

    //Dictionary is a Key Value pair that allows us to set a Key to a specific value.
    //In this case - the key is a MenuState enum, and the value is a BaseMenu object
    //This dictionary is filled up in the start function.
    Dictionary<MenuStates, BaseMenu> menuDictionary = new Dictionary<MenuStates, BaseMenu>();

    //Menu Stack is the stack that we populate as we navigate the menu.  Top most on the stack is displayed and
    //we remove the top element when going back in the menu
    Stack<MenuStates> menuStack = new Stack<MenuStates>();


    //Set initial state in the inspector
    public MenuStates initialState = MenuStates.MainMenu;


    // Start is called before the first frame update
    void Start()
    {
        //Fill the all menus array if it has not been filled already
        if (allMenus.Length <= 0)
        {
            allMenus = gameObject.GetComponentsInChildren<BaseMenu>(true);
        }

        //loop through all the menus and add them to the dictionary and intialize their state
        //Importatant to initalize the menu state with the current context as menu states need a
        //menu controller for context
        foreach (BaseMenu menu in allMenus)
        {
            if (menu == null) continue;

            menu.InitState(this);

            if (menuDictionary.ContainsKey(menu.state)) continue;

            menuDictionary.Add(menu.state, menu);
        }

        SetActiveState(initialState);
        GameManager.Instance.SetMenuController(this);
    }

    //Pop the top element off the stack and grab the next element on the stack and set it active
    public void JumpBack()
    {
        if (menuStack.Count <= 1) SetActiveState(MenuStates.MainMenu);
        else
        {
            menuStack.Pop();
            SetActiveState(menuStack.Peek(), true);
        }
    }

    /// <summary>
    /// Setting active menu state - if we are jumping back we do not need to be pushed into the menu stack - This function handles the exiting and entering of states
    /// </summary>
    /// <param name="newState"></param>
    /// <param name="isJumpingBack"></param>
    public void SetActiveState(MenuStates newState, bool isJumpingBack = false)
    {
        if (!menuDictionary.ContainsKey(newState)) return;
        if (currentState == menuDictionary[newState]) return;

        if (currentState != null)
        {
            currentState.ExitState();
            currentState.gameObject.SetActive(false);
        }

        currentState = menuDictionary[newState];
        currentState.gameObject.SetActive(true);
        currentState.EnterState();

        if (!isJumpingBack) menuStack.Push(newState);
    }
}
