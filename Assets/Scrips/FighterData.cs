using UnityEngine;

[CreateAssetMenu(fileName = "FighterData", menuName = "Scriptable Objects/FighterData")]
public class FighterData : ScriptableObject
{
    public float maxHealth;
    public string fightername;   ///* falto el Fighter
    public AttackData[] attacks;
    public float chargeTime = 2f; ///*para el tiempo
    public GameObject appearParticles; ///* para las particulas
    public string appearSoundName;///* nueva para los sonidos
    public string damageSoundName; ///* sonidos
    public string deadSoundName; ///* sonidos
    public AttackData GetRandomAttack()
    {
        return attacks[Random.Range(0, attacks.Length)];
    }
}

[System.Serializable]
public class AttackData
{
    public string name;
    public string animationName;
    public string attackSoundName; ///* sonidos
    public float minDamage;
    public float maxDamage;
    public GameObject chargeParticles;
    public GameObject attackParticles;
}
