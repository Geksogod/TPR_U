using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menuButton;
    [SerializeField]
    private GameObject newGameMenu;
    [SerializeField]
    private GameObject settingsMenu;

    [Header("Logo Settings")]
    [SerializeField]
    private GameObject logoChoseObject;
    [SerializeField]
    private FractionItem[] logoItems = new FractionItem[] { };
    private FractionItem currentLogo;
    [SerializeField]
    private GameObject bandle;
    [SerializeField]
    private TextMeshProUGUI bandleName;
    private int currentLogoIndex;
    [Header("New Game Settings")]
    [SerializeField]
    private InputField inputFieldName;
    [SerializeField]
    private InputField inputFieldNick;
    [SerializeField]
    private TMP_Dropdown inputFieldCharacter;
    [SerializeField]
    private Button startNewGame;

    void Start()
    {
        ShowMenuButton(true);
        CloseNewGameMenu();
        CloseSettings();
        ChangeLogo();
    }

    private void ShowMenuButton(bool isShow)
    {
        menuButton.SetActive(isShow);
    }

    public void OpenNewGameMenu()
    {
        settingsMenu.SetActive(false);
        newGameMenu.SetActive(true);
    }

    public void CloseNewGameMenu()
    {
        newGameMenu.SetActive(false);
    }

    public void OpenSettings()
    {
        newGameMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }

    private void Update()
    {
        startNewGame.interactable = !string.IsNullOrEmpty(inputFieldName.text) && !string.IsNullOrEmpty(inputFieldNick.text) && !string.IsNullOrEmpty(inputFieldCharacter.options[inputFieldCharacter.value].text);
    }
    public void StartNewGame()
    {
        //FractionItem fractionItem = ScriptableObject.CreateInstance<FractionItem>();
        //AssetDatabase.CreateAsset(fractionItem, "Assets/MyData1.asset");
        //AssetDatabase.SaveAssets();
        //AssetDatabase.Refresh();
        SceneManager.LoadScene("HumanScene");

    }
    public void NextLogo()
    {
        if (currentLogoIndex + 1 < logoItems.Length)
            currentLogoIndex++;
        else
            currentLogoIndex = 0;
        ChangeLogo();
    }

    public void PreviusLogo()
    {
        if (currentLogoIndex - 1 > 0)
            currentLogoIndex--;
        else
            currentLogoIndex = logoItems.Length-1;
        ChangeLogo();
    }


    private void ChangeLogo()
    {
        currentLogo = logoItems[currentLogoIndex];
        logoChoseObject.GetComponent<Image>().sprite = currentLogo.GetSprite();
        bandle.GetComponent<Animator>().SetTrigger("Deploy");
        bandle.GetComponent<Animator>().SetTrigger("Idle");
        bandleName.text = currentLogo.GetName();
        for (int i = 0; i < bandle.GetComponentsInChildren<SkinnedMeshRenderer>().Length; i++)
        {
            bandle.GetComponentsInChildren<SkinnedMeshRenderer>()[i].sharedMaterial = currentLogo.GetMaterial();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
