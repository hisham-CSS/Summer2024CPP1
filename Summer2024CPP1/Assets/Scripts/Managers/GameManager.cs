using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    public Action<int> OnLifeValueChanged;
    //public UnityEvent<int> OnLifeValueChanged;

    //Private Lives Variable
    private int _lives = 10;

    //public variable for getting and setting lives
    public int lives
    {
        get
        {
            return _lives;
        }
        set
        {
            //all lives lost (zero counts as a life due to the check)
            if (value < 0)
            {
                GameOver();
                return;
            }

            //lost a life
            if (value < _lives)
            {
                Respawn();
            }

            //cannot roll over max lives
            if (value > maxLives)
            {
                value = maxLives;
            }

            _lives = value;
            OnLifeValueChanged?.Invoke(_lives);

            Debug.Log($"Lives value on {gameObject.name} has changed to {lives}");
        }
    }

    //max lives that are possible
    [SerializeField] private int maxLives = 10;
    [SerializeField] private PlayerController playerPrefab;

    [HideInInspector] public PlayerController PlayerInstance => playerInstance;
    private PlayerController playerInstance;
    private Transform currentCheckpoint;
    private MenuController currentMenuController;

    private void Awake()
    {
        //if we are the first instance of the gamemanager object - ensure that our instance variable is filled and we cannot be destroyed when loading new levels.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return; // early exit out of the function
        }

        //if we are down here in execution - that means that the above if statement didn't run - which means we are a clone
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentMenuController) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            currentMenuController.SetActiveState(MenuController.MenuStates.Pause);
        }
            


        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (SceneManager.GetActiveScene().name == "Title")
        //        SceneManager.LoadScene("Level");
        //    else
        //        SceneManager.LoadScene("Title");
        //}

    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void GameOver()
    {
        Debug.Log("Game Over, change it to move to a specific level");
        SceneManager.LoadScene("Title");
    }

    void Respawn()
    {
        playerInstance.transform.position = currentCheckpoint.position;
    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        playerInstance = Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity);
        currentCheckpoint = spawnLocation;
    }   
    
    public void UpdateCheckpoint(Transform updatedCheckpoint)
    {
        currentCheckpoint = updatedCheckpoint;
    }

    public void SetMenuController(MenuController menuController)
    {
        currentMenuController = menuController;
    }
}
