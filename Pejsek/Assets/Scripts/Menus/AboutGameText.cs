using UnityEngine;
using TMPro;

public class AboutGameText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI aboutGameText = default;
    
    void Awake() {
        aboutGameText.text = 
        "<b>Pejsek záchranář – Nehoda vlaku</b> je výuková mobilní hra pro děti, vytvořena ve spolupráci ZZS JmK a VUT FIT formou bakalářské práce. " + 
        "Cílem hry je předat správný postup přivolání první pomoci při mimořádné události nehody vlaku." + System.Environment.NewLine +
        "Autor práce: Jan Krejčí" + System.Environment.NewLine +
        "Hlas Pejska: Michal Isteník";
    }
}
