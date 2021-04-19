using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using V_AnimationSystem;

public class GameAssets : MonoBehaviour {
        
    // Internal instance reference
    private static GameAssets _i; 

    // Instance reference
    public static GameAssets i {
        get {
            if (_i == null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>(); 
            return _i; 
        }
    }


    public Transform pfSwordSlash;
        


    public static class UnitAnimTypeEnum {

        static UnitAnimTypeEnum() {
            FieldInfo[] fieldInfoArr = typeof(UnitAnimTypeEnum).GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo fieldInfo in fieldInfoArr) {
                if (fieldInfo != null) {
                    fieldInfo.SetValue(null, UnitAnimType.GetUnitAnimType(fieldInfo.Name));
                }
            }
        }

        public static UnitAnimType dSwordTwoHandedBack_Idle;
        public static UnitAnimType dSwordTwoHandedBack_Walk;
        public static UnitAnimType dSwordTwoHandedBack_Sword;
        public static UnitAnimType dSwordTwoHandedBack_Sword2;
            
        public static UnitAnimType dMinion_Idle;
        public static UnitAnimType dMinion_Walk;
        public static UnitAnimType dMinion_Attack;
            
        public static UnitAnimType dShielder_Idle;
        public static UnitAnimType dShielder_Walk;

        public static UnitAnimType dSwordShield_Idle;
        public static UnitAnimType dSwordShield_Walk;

    }

}
