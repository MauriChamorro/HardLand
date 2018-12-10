using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Level currentLevel;
    public Animator levelTextAnimator;
    public Animator lifeAnimator;
    public Image[] lifes;
    public Text chipCount;
    public LifesUI lifesUI;

    public void LoseLifes(int pActualLifes, int pCantLoseLifes)
    {
        for (int i = 0; i < pCantLoseLifes; i++)
        {
            if (pActualLifes > 0)
            {
                lifeAnimator.SetTrigger("LoseLife" + pActualLifes);
                pActualLifes--;
            }
        }
    }

    private void ActivateLifes()
    {
        foreach (Image life in lifes)
        {
            life.enabled = true;
        }
    }

    public void ActivateTriggerAnimator(string pNameTrigger)
    {
        levelTextAnimator.SetTrigger(pNameTrigger);
    }

    public void ChipPickedUp()
    {
        currentLevel.ChipPickedUp();
        chipCount.text = currentLevel.CurrentChipCant.ToString();
    }

    public void Initilizer(Level pCurrentLevel)
    {
        currentLevel = pCurrentLevel;
        lifesUI.Initializer(currentLevel.Lifes);
        chipCount.text = currentLevel.CurrentChipCant.ToString();
        ActivateLifes();
    }
}
