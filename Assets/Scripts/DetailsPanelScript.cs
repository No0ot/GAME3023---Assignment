using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetailsPanelScript : MonoBehaviour
{
    public BattleUnit playerMonReference;

    [SerializeField] Image monPortrait;
    [SerializeField] TMP_Text hpNum;
    [SerializeField] TMP_Text mpNum;
    [SerializeField] TMP_Text spdNum;
    [SerializeField] TMP_Text atkNum;
    [SerializeField] TMP_Text defNum;
    [SerializeField] TMP_Text expNum;
    // Start is called before the first frame update

    public void UpdateDetails()
    {
        monPortrait.sprite = playerMonReference.GetBattleCreature().GetPortraitSprite();
        hpNum.text = playerMonReference.GetBattleCreature().GetHP() + " / " + playerMonReference.GetBattleCreature().GetMaxHP();
        mpNum.text = playerMonReference.GetBattleCreature().GetMP() + " / " + playerMonReference.GetBattleCreature().GetMaxMP();
        spdNum.text = playerMonReference.GetBattleCreature().GetSpeed() + "";
        defNum.text = playerMonReference.GetBattleCreature().GetDefense() + "";
        atkNum.text = playerMonReference.GetBattleCreature().GetAttack() + "";
        expNum.text = playerMonReference.experience + " / " + playerMonReference.experienceNeeded;
    }
}
