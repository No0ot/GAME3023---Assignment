using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RuneDetailsPanelScript : MonoBehaviour
{
    public Item itemRef;

    [SerializeField] TMP_Text name;
    [SerializeField] TMP_Text hpNum;
    [SerializeField] TMP_Text mpNum;
    [SerializeField] TMP_Text spdNum;
    [SerializeField] TMP_Text atkNum;
    [SerializeField] TMP_Text defNum;

    // Update is called once per frame
    public void UpdateDetails()
    {
        name.text = itemRef.name;
        hpNum.text = "+" + itemRef.addedHP;
        mpNum.text = "+" + itemRef.addedMP;
        spdNum.text = "+" + itemRef.addedSpeed;
        atkNum.text = "+" + itemRef.addedAttack;
        defNum.text = "+" + itemRef.addedDefense;

    }
}
