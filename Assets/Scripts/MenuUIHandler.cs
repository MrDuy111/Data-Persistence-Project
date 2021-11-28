using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject UsernameInput;

    public string playerName;

    public static MenuUIHandler Instance;

    public int bestScore;
    public string bestPlayer;

    [SerializeField] GameObject canvas;
    [SerializeField] Text bestScoreText;

    private MainManager mainManager;

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            MainManager.SaveData data = JsonUtility.FromJson<MainManager.SaveData>(json);

            bestScore = data.bestScore;
            bestPlayer = data.bestPlayerName;
            bestScoreText.text = "Best Score: " + bestPlayer + ": " + bestScore;
        }
    }

    public void StartNew()
    {      
        Debug.Log("Start was Clicked");
        SceneManager.LoadScene(1);
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getPlayerNameInput()
    {
        playerName = UsernameInput.GetComponent<InputField>().text;
        Debug.Log(playerName);
    }

    public string getPlayerName()
    {
        return playerName;
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit(); // original code to quit Unity player
#endif
        Debug.Log("Exit was Clicked");
    }
}
