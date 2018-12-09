using UnityEngine;
using UnityEngine.UI;

public class LifesUI : MonoBehaviour
{
    private int lifes;

    private void Start()
    {
        lifes = GeneralGameValues.PlayerLifes;
    }

    public void LoseLife()
    {
        lifes--;
        transform.GetChild(lifes).gameObject.GetComponent<Image>().enabled = false;
    }
}
