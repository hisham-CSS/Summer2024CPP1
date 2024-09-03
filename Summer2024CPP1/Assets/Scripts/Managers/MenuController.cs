using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public BaseMenu[] allMenus;
    public enum MenuStates
    {
        MainMenu, Settings, Pause, InGame
    }
    public MenuStates initialState = MenuStates.MainMenu;
    BaseMenu currentState;
    Dictionary<MenuStates, BaseMenu> menuDictionary = new Dictionary<MenuStates, BaseMenu>();
    Stack<MenuStates> menuStack = new Stack<MenuStates>();
    // Start is called before the first frame update
    void Start()
    {
        if (allMenus == null) return;

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
    public void JumpBack()
    {
        if (menuStack.Count <= 1) SetActiveState(MenuStates.MainMenu);
        else
        {
            menuStack.Pop();
            SetActiveState(menuStack.Peek(), true);
        }
    }
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
