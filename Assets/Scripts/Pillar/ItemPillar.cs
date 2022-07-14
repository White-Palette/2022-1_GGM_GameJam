using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class ItemPillar : Pillar
{
    public enum ItemType
    {
        AddCombo,
        MaintainCombo,
        Guard,
        PushDragon
    }

    public abstract class Item
    {
        public int Count;
        public abstract ItemType Type { get; }
        public abstract void Use();
    }

    public class AddComboItem : Item
    {
        public override ItemType Type => ItemType.AddCombo;

        public override void Use()
        {
            ComboManager.Instance.AddCombo(Random.Range(5, 10));
        }

        public AddComboItem()
        {
            Count = 1;
        }
    }

    public class MaintainComboItem : Item
    {
        public override ItemType Type => ItemType.MaintainCombo;

        public override void Use()
        {
            ComboManager.Instance.FreezeCombo(5f);
        }

        public MaintainComboItem()
        {
            Count = 1;
        }
    }

    public class GuardItem : Item
    {
        public override ItemType Type => ItemType.Guard;

        public override void Use()
        {
            PlayerController.Instance.AddGuard();
        }

        public GuardItem()
        {
            Count = 1;
        }
    }

    public class FreezeDragonItem : Item
    {
        public override ItemType Type => ItemType.PushDragon;

        public override void Use()
        {
            Chaser.Instance.Freeze(5f);
        }

        public FreezeDragonItem()
        {
            Count = 1;
        }
    }

    public static Item[] Items = new Item[]
    {
        new AddComboItem(),
        new MaintainComboItem(),
        new GuardItem(),
        new FreezeDragonItem()
    };

    [SerializeField] SpriteRenderer _icon = null;

    public override void TowerEvent()
    {
        int random = Random.Range(0, Items.Sum(x => x.Count));
        for (int i = 0; i < Items.Length; i++)
        {
            if (random < Items[i].Count)
            {
                Items[i].Use();
                break;
            }
            else
            {
                random -= Items[i].Count;
            }
        }
    }

    public override void Initialize()
    {
        base.Initialize();
        _icon.DOFade(1, 0.2f).From();
    }
}
