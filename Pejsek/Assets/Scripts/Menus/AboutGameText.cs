using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AboutGameText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI aboutGameText = default;
    
    void Awake() {
        aboutGameText.text = 
        "<b>Pejsek záchranář – Nehoda vlaku</b> je výuková mobilní hra pro děti, vytvořená ve spolupráci ZZS JmK a VUT FIT formou bakalářské práce. " + 
        "Cílem hry je předat korektní postup při volání první pomoci při mimořádné události nehody vlaku." + System.Environment.NewLine + 
        "Licence... ?" + System.Environment.NewLine + 
        "Autor práce: Jan Krejčí";
    }
}
