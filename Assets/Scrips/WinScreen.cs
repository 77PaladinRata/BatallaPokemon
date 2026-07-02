using UnityEngine;
using UnityEngine.UI;
public class WinScreen : MonoBehaviour
{
    [SerializeField]
    private Text winText;
    private Animator animator;
    private bool isHidden = true;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void ShowWinScreen(string winner)
    {
        winText.text = winner + " Wins!";
        animator.Play("Show", 0, 0f);
        isHidden = false;
    }
    public void HideWinScreen()
    {
        if (isHidden) return;
        isHidden = true;
        animator.Play("Hide", 0, 0f);
    }
}
