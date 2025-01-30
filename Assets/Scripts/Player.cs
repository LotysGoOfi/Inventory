using System;
using UnityEngine;

[Serializable]
public class Player
{
    [SerializeField]
    private int helthPoint;

    [SerializeField]
    private int arrmorHead;

    [SerializeField]
    private int arrmorBody;

    public Player(int helthPoint, int arrmorHead, int arrmorBody)
    {
        this.helthPoint = helthPoint;
        this.arrmorHead = arrmorHead;
        this.arrmorBody = arrmorBody;
    }

    public int ArrmorHead { get => arrmorHead; set => arrmorHead = value; }
    public int ArrmorBody { get => arrmorBody; set => arrmorBody = value; }
    public int HelthPoint { get => helthPoint; set => helthPoint = value; }

    public int Heal(int number)
    {
        if (helthPoint + number > 100)
        {
            helthPoint = 100;
            return helthPoint;
        }
        helthPoint += number;
        return helthPoint;
    }

    public int Damage(int damage,TypeArmor typeDamage)
    {
        var arrmor = typeDamage switch
        {
            TypeArmor.Head => arrmorHead,
            TypeArmor.Body => arrmorBody,
            _ => 0,
        };
        var newDamage = damage - arrmor;
        if (newDamage > 0)
        {
            if (helthPoint <= newDamage) {  return 0 ; }
            helthPoint -= newDamage;
        }

        return helthPoint;
    }
}
