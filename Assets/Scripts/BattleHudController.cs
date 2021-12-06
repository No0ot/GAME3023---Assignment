using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHudController : MonoBehaviour
{
    //https://youtu.be/zKRMkD28-xY - Make A Game Like Pokemon in Unity | #7 - Battle System Setup
    //https://youtu.be/Vm9PsK7r6UY - Make A Game Like Pokemon in Unity | #9 - Implementing Attacks in Battle System

    [SerializeField] private Text player_name_;
    [SerializeField] private Text player_level_;
    [SerializeField] private Slider player_hp_bar_;
    [SerializeField] private Text player_hp_text_;
    [SerializeField] private Slider player_mp_bar_;
    [SerializeField] private Text player_mp_text_;
    [SerializeField] private GameObject player_placement_;
    private GameObject player_sprite_;

    [SerializeField] private Text enemy_name_;
    [SerializeField] private Text enemy_level_;
    [SerializeField] private Slider enemy_hp_bar_;
    [SerializeField] private Text enemy_hp_text_;
    [SerializeField] private Slider enemy_mp_bar_;
    [SerializeField] private Text enemy_mp_text_;
    [SerializeField] private GameObject enemy_placement_;
    private GameObject enemy_sprite_;

    [SerializeField] private List<Button> player_action_list_ = new List<Button>();
    [SerializeField] private List<Button> player_ability_list_ = new List<Button>();

    private Creature player_creature_;
    private Creature enemy_creature_;

    public void SetPlayerData(Creature player_creature)
    {
        player_name_.text = player_creature.GetBaseStats().GetName();
        player_level_.text = "Lv." + player_creature.GetLevel();
        player_hp_bar_.value = (player_creature.GetHP() / player_creature.GetMaxHP());
        player_hp_text_.text = "(" + player_creature.GetHP() + "/" + player_creature.GetMaxHP() + ")";
        player_mp_bar_.value = (player_creature.GetMP() / player_creature.GetMaxMPFromBase());
        player_mp_text_.text = "(" + player_creature.GetMP() + "/" + player_creature.GetMaxMPFromBase() + ")";
        player_sprite_ = Instantiate(player_creature.GetBaseStats().GetSpriteObj(), player_placement_.transform);
        if (player_sprite_)
        {
            player_sprite_.transform.localPosition = new Vector3(0f, 0f, 0f);
            player_sprite_.transform.localScale = new Vector3(Mathf.Abs(player_sprite_.transform.localScale.x),
                                                            player_sprite_.transform.localScale.y,
                                                            player_sprite_.transform.localScale.z);
        }
        player_creature_ = player_creature;
    }

    public void SetEnemyData(Creature enemy_creature)
    {
        enemy_name_.text = enemy_creature.GetBaseStats().GetName();
        enemy_level_.text = "Lv." + enemy_creature.GetLevel();
        enemy_hp_bar_.value = (enemy_creature.GetHP() / enemy_creature.GetMaxHP());
        enemy_hp_text_.text = "(" + enemy_creature.GetHP() + "/" + enemy_creature.GetMaxHP() + ")";
        enemy_mp_bar_.value = (enemy_creature.GetMP() / enemy_creature.GetMaxMPFromBase());
        enemy_mp_text_.text = "(" + enemy_creature.GetMP() + "/" + enemy_creature.GetMaxMPFromBase() + ")";
        if(enemy_sprite_ == null)
            enemy_sprite_ = Instantiate(enemy_creature.GetBaseStats().GetSpriteObj(), enemy_placement_.transform);

        if (enemy_sprite_)
        {
            enemy_sprite_.transform.localPosition = new Vector3(0f, 0f, 0f);
            enemy_sprite_.transform.localScale = new Vector3(Mathf.Abs(enemy_sprite_.transform.localScale.x) * -1f,
                                                            enemy_sprite_.transform.localScale.y,
                                                            enemy_sprite_.transform.localScale.z);
        }
        enemy_creature_ = enemy_creature;
    }

    public void UpdateHP()
    {
        player_hp_bar_.value = (player_creature_.GetHP() / player_creature_.GetMaxHP());
        player_hp_text_.text = "(" + player_creature_.GetHP() + "/" + player_creature_.GetMaxHP() + ")";
        enemy_hp_bar_.value = (enemy_creature_.GetHP() / enemy_creature_.GetMaxHP());
        enemy_hp_text_.text = "(" + enemy_creature_.GetHP() + "/" + enemy_creature_.GetMaxHP() + ")";
    }

    public void UpdateMP()
    {
        player_mp_bar_.value = (player_creature_.GetMP() / player_creature_.GetMaxMPFromBase());
        player_mp_text_.text = "(" + player_creature_.GetMP() + "/" + player_creature_.GetMaxMPFromBase() + ")";
        enemy_mp_bar_.value = (enemy_creature_.GetMP() / enemy_creature_.GetMaxMPFromBase());
        enemy_mp_text_.text = "(" + enemy_creature_.GetMP() + "/" + enemy_creature_.GetMaxMPFromBase() + ")";
    }

    public void SetActiveActionList(bool value)
    {
        foreach (var item in player_action_list_)
        {
            item.gameObject.SetActive(value);
        }
    }

    public void SetActiveAbilityList(bool value)
    {
        foreach (var item in player_ability_list_)
        {
            item.gameObject.SetActive(value);
        }
    }

    public void SetInteractableAbilityList(bool value)
    {
        foreach (var item in player_ability_list_)
        {
            item.interactable = value;
        }
    }

    public void SetAbilityNames(List<Ability> abilities)
    {
        for (int i = 0; i < player_ability_list_.Count; i++)
        {
            if (i<abilities.Count)
            {
                player_ability_list_[i].transform.GetComponentInChildren<Text>().text = abilities[i].GetBase().GetName(); //set button text
                player_ability_list_[i].transform.GetComponent<BattleHudAbilityButtonController>().SetId(i);
            }
            else
            {
                player_ability_list_[i].transform.GetComponentInChildren<Text>().text = "NULL";
                player_ability_list_[i].transform.GetComponent<BattleHudAbilityButtonController>().SetId(-1);
            }
        }
    }

    public void ResetHud()
    {
        Destroy(enemy_sprite_);
    }
}
