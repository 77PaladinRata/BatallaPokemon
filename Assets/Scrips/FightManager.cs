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
    [SerializeField] ///* Nueva JJJJJJJJJJJJJJ
    private UnityEvent<string> onFightEnd; ///* not - END 
    [SerializeField]  ///* cuanto le queda de vida
    private UnityEvent<DamageTarget> onDamageTaken; ///* numero de vida
    [SerializeField]
    private int minimumFighters = 2;
    [SerializeField]
    private int maximumFighters = 2;
    [SerializeField]
    private PoolManager poolManager; ///* falto este
    private List<Fighter> fighters = new List<Fighter>();
    private DamageTarget damageTarget = new DamageTarget(); ///* PV creo?
    public void AddFighter(Fighter fighter)
    {
        if (fighters.Count < maximumFighters && !fighters.Contains(fighter))
        {   ///* agregando los sonidos que mas o menos se escuchan
            poolManager.GetObject(fighter.FighterData.appearParticles, fighter.transform.position);
            SoundManager.instance.Play(fighter.FighterData. appearSoundName);
            fighters.Add(fighter); ///* Agregando Dialogo RPG              ///*  I CAST MAGIC Digimon
            DialogSystem.Instance.ShowDialog(fighter.FighterData.fighterName + " has joined the fight!"); 
            fighter.Health.InitializeHealth(); ///* antes del parcial}}}}}}}}}}}}}}
            fighter.Animator.Play("Idle", 0, 0f); ///* antes del parcial}}}}}}}}}}}
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
            poolManager.GetObject(attackData.chargeParticles, attacker.transform.position); ///* Dialogo de ataque
            DialogSystem. Instance.ShowDialog(attacker.FighterData. fighterName + " attacks with " + attackData.name + "!"); ///*********
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
            ///* agregando MAS codigo ÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑ
            float damage = Random. Range(attackData.minDamage, attackData.maxDamage);
            damageTarget.SetDamageTarget(defender.transform, damage);
            defenderHealth.TakeDamage (damage);
            onDamageTaken?.Invoke(damageTarget);
            if (defenderHealth.CurrentHealth <= 0)
            {   ///* nesecitamos diviciones de paranras
                SoundManager.instance.Play(defender.FighterData.damageSoundName); ///* sonido ************************************
                RemoveFighter(defender);            ///* Dialogo de ataque
                DialogSystem. Instance.ShowDialog(attacker.FighterData.fighterName + " wins the fight!"); ///***** 
                FighterWin(attacker);
            }
            yield return new WaitForSeconds(1.5f);
        }
    }
    ///*private void FighterWin(Fighter winner)
    ///*{
        ///*Debug. Log(winner.name + " wins the fight!");
    ///*} ///* Poniendo cosas Nuevas
    private void FighterWin(Fighter winner)
    {
        onFightEnd ?. Invoke(winner.FighterData.fighterName);
        winner.Animator.Play("Victory", 0, 0f);
        winner.transform.LookAt(Camera.main. transform);
    }
}
