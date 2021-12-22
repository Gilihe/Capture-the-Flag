using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text itemsCollectedText;
    [SerializeField] private Text playerHealthText;
    [SerializeField] private Text instructionsText;
    [SerializeField] private GameObject buttonWinGame;
    [SerializeField] private GameObject buttonLoseGame;

    [SerializeField] private GameBehavior gameBehavior;

    private void Start()
    {
        gameBehavior.OnItemCollected += ChangeInstructionsText;
        gameBehavior.OnAllItemCollected += DisplayWinText;
        gameBehavior.OnAllItemCollected += ShowWinButton;
        gameBehavior.OnItemPlayerDamage += ChangePlayerHealthText;
        gameBehavior.OnPlayerDeath += ShowLoseButton;
    }

    private void DisplayWinText()
    {
        ChangeItemsCollectedText(gameBehavior.MaxItems);
        instructionsText.text = "You've found all the items!";
    }

    private void ChangeInstructionsText(int items)
    {
        ChangeItemsCollectedText(items);
        int itemsToGo = gameBehavior.MaxItems - items;
        instructionsText.text = $"Item found, {itemsToGo.ToString()} only  more to go!";
    }

    private void ChangeItemsCollectedText(int items)
    {
        itemsCollectedText.text = $"Items Collected: {items.ToString()}";
    }

    private void ChangePlayerHealthText(int health)
    {
        playerHealthText.text = $"Player Health: {health.ToString()}";
    }

    private void ShowWinButton()
    {
        ShowEndButton(true);
    }

    private void ShowLoseButton()
    {
        ShowEndButton(false);
    }

    private void ShowEndButton(bool isWinButton)
    {
        if (isWinButton)
        {
            buttonWinGame.SetActive(true);
        }
        else
        {
            buttonLoseGame.SetActive(true);
        }
        
        Time.timeScale = 0;
    }

    public void ClickEndGameButton()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
