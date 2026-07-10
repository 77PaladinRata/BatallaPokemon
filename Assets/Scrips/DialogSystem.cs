using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogSystem : MonoBehaviour
{
    public static DialogSystem Instance { get; private set; }
    [SerializeField]
    private Text dialogText;
    [SerializeField]
    private float timeBetweenWords = 0.25f;
    private Animator animator;
    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }
    public void ShowDialog(string dialog)
    {
        StopAllCoroutines();
        StartCoroutine(ShowDialogCoroutine(dialog));
    }
    private IEnumerator ShowDialogCoroutine(string dialog)
    {
        dialogText.text = "";
        animator.Play("Show", 0, 0f);
        yield return null;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);///* elimine las que no use 
        foreach (char letter in dialog.ToCharArray())                                   ///* por saturacion de sonidos
        {                                                                               
            dialogText.text += letter;                                      ///* sonido del profe  ///*
            yield return new WaitForSeconds(timeBetweenWords);  ///*Chibi Three ///* "DialogLetter"  ///* Chibi Three
            SoundManager.instance.Play("Click Two"); ///* Aqui va el sondido por cada letra ///* NotaGuitarra
        }
        yield return new WaitForSeconds(1f);
        animator.Play("Hide", 0, 0f);
    }
    public void StopDialog()
    {
        StopAllCoroutines();
        dialogText.text = "";
        animator.Play("Hide", 0, 0f);
    }
}
