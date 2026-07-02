using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

public class FightManager : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onFightReady;
    [SerializeField] ///* Cancelando Ataque
    private UnityEvent onCancelFight; ///*NO Attack
    [SerializeField] ///* Cancelando Ataque
    private UnityEvent onFightStart;///*NO Attack
    [SerializeField]
    private int minimumFighters = 2;
    [SerializeField]
    private int maximumFighters = 2;
    [SerializeField]
    private PoolManager poolManager; ///* falto este
    private List<Fighter> fighters = new List<Fighter>();
    public void AddFighter(Fighter fighter)
    {
        if (fighters.Count < maximumFighters && !fighters.Contains(fighter))
        {   ///* agregando los sonidos que mas o menos se escuchan
            poolManager.GetObject(fighter.FighterData.appearParticles, fighter.transform.position);
            SoundManager.instance.Play(fighter.FighterData. appearSoundName);
            fighters.Add(fighter);
            if (fighters.Count >= minimumFighters)
            {
                onFightReady?.Invoke();
            }
        }
    } ///* cancelar pelea cunado terminan de pelear
    public void RemoveFighter(Fighter fighter)
    {
        if (fighters.Contains(fighter))
        {
            fighters.Remove(fighter); ///* poniendoles nuevas abajo para cancelar pelea
            if (fighters.Count < minimumFighters)
            {
                onCancelFight ?. Invoke();
            }
        }
    }
    public void StartFight()
    { ///* cancelar pelea otra vexz
        onFightStart ?. Invoke();
        StartCoroutine(FightCoroutine());
    }
    private IEnumerator FightCoroutine()
    { ///* agregandoles IFS
        foreach (Fighter fighter in fighters)
        {
            fighter.Health.InitializeHealth();
        }
        while (fighters.Count > 1)
        {
            Fighter attacker = fighters[Random.Range(0, fighters.Count)];
            Fighter defender = fighters[Random.Range(0, fighters.Count)];
            while (defender == attacker)
            {
                defender = fighters[Random.Range(0, fighters.Count)];
            }
            AttackData attackData = attacker.FighterData.GetRandomAttack();
            attacker.transform.LookAt (defender. transform);
            defender.transform. LookAt(attacker.transform);
            attacker.Animator.Play("Charge", 0, 0f);
            poolManager.GetObject(attackData.chargeParticles, attacker.transform.position);
            yield return new WaitForSeconds(attacker.FighterData. chargeTime);
            ///* Separarlas para particulas eso creo
            attacker.Animator.Play(attackData.animationName, 0, 0f);
            SoundManager . instance.Play(attackData. attackSoundName); ///* sonido ******************************************
            poolManager.GetObject(attackData.chargeParticles, attacker.transform.position);
            yield return null;
            yield return new WaitForSeconds(attacker.Animator.GetCurrentAnimatorStateInfo(0).length);
            ///* poolManager.GetObject(attackData.attackParticles, defender.transform.position);
            poolManager.GetObject(attackData.chargeParticles,attacker.transform.position);
            Health defenderHealth = defender.GetComponent<Health>();
            SoundManager.instance.Play(defender.FighterData.damageSoundName); ///* sonido ************************************
            defenderHealth. TakeDamage (Random.Range(attackData.minDamage, attackData.maxDamage));
            if (defenderHealth.CurrentHealth <= 0)
            {   ///* nesecitamos diviciones de paranras
                SoundManager.instance.Play(defender.FighterData.damageSoundName); ///* sonido ************************************
                RemoveFighter(defender);
                FighterWin(attacker);
            }
            yield return new WaitForSeconds(1.5f);
        }
    }
    private void FighterWin(Fighter winner)
    {
        Debug. Log(winner.name + " wins the fight!");
    }
}
