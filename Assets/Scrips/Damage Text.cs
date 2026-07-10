using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Text damageText;
    private void Awake()
    {
        damageText. text = "";
        animator = GetComponent<Animator>();
    }
    public void ShowDamage(DamageTarget damageTarget)
    {
        damageText.text = damageTarget.damage. ToString("F0");
        transform.position = Camera.main.WorldToScreenPoint(damageTarget.target.position);
        animator.Play("Show", 0, 0f);
    }
}

[System.Serializable]
public class DamageTarget
{
    public Transform target;
    public float damage;
    public void SetDamageTarget(Transform target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }
}