 using UnityEngine;
 using System.Collections;
 
 public class PointerBlock : MonoBehaviour
 {
     public static bool blockedByUI = false;
    void OnMouseOver()
     {
         print (gameObject.name);
         blockedByUI = true;
     }

     void OnMouseExit(){
        print ("NO LONGER");
         blockedByUI = false;
     }
 }