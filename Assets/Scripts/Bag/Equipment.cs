using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EquipType
{
    Helmet,
    Drill,
    Shoe

}
public class Equipment : Buyable
{
    public int Stamina {  get; set; }//ÃÂ¡¶
    public int Oxygen { get; set; }
    public int DrillForce {  get; set; }
    public int SpeedAdd {  get; set; }
    public EquipType equipType { get; set; }
    public Equipment(int stamina,int oxygen,int drillForce,int speedAdd,EquipType equipType ) 
    {
        Stamina = stamina;
        Oxygen = oxygen;
        DrillForce = drillForce;
        SpeedAdd = speedAdd;
        this.equipType = equipType;

    }

}
