using UnityEngine;
using System.Collections;

public enum InfoType {
    Name,
    HeadPortrait,
    Level,
    Power,
    Exp,
    Diamond,
    Coin,
    Energy,
    Toughen,
    HP,
    Damage,
    Equip,
    All
}

public class PlayerInfo : MonoBehaviour {

    public static PlayerInfo _instance;



    private string _name;
    private string _headPortrait;
    private int _level = 1;
    private int _power = 1;
    private int _exp = 0;
    private int _diamond ;
    private int _coin;
    private int _energy;
    private int _toughen;
    private int _hp;
    private int _damage;
    private PlayerType _playerType;
    //private int _helmID =0 ;
    //private int _clothID =0 ;
    //private int _weaponID =0 ;
    //private int _shoesID=0;
    //private int _necklaceID=0;
    //private int _braceletID=0;
    //private int _ringID=0;
    //private int _wingID=0;


    public InventoryItem helmInventoryItem;
    public InventoryItem clothInventoryItem;
    public InventoryItem weaponInventoryItem;
    public InventoryItem shoesInventoryItem;
    public InventoryItem necklaceInventoryItem;
    public InventoryItem braceletInventoryItem;
    public InventoryItem ringInventoryItem;
    public InventoryItem wingInventoryItem;

    public float energyTimer = 0;
    public float toughenTimer = 0;

    public delegate void OnPlayerInfoChangedEvent( InfoType type );
    public event OnPlayerInfoChangedEvent OnPlayerInfoChanged;

    #region get set methd
    public string Name {
        get {
            return _name;
        }
        set {
            _name = value;
        }
    }
    public string HeadPortrait {
        get {
            return _headPortrait;
        }
        set {
            _headPortrait = value;
        }
    }
    public int Level {
        get {
            return _level;
        }
        set {
            _level = value;
        }
    }
    public int Power {
        get {
            return _power;
        }
        set {
            _power = value;
        }
    }
    public int Exp {
        get {
            return _exp;
        }
        set {
            _exp = value;
        }
    }
    public int Diamond {
        get {
            return _diamond;
        }
        set {
            _diamond = value;
        }

    }
    public int Coin {
        get {
            return _coin;
        }
        set {
            _coin = value;
        }
    }
    public int Energy {
        get {
            return _energy;
        }
        set {
            _energy = value;
        }
    }
    public int Toughen {
        get {
            return _toughen;
        }
        set {
            _toughen = value;
        }
    }
    public int HP{
        get{
            return _hp;
        }
        set{
            _hp = value;
        }
    }
    public int Damage{
        get{return _damage;}
        set{_damage = value;}
    }

    public PlayerType PlayerType
    {
        get
        {
            return _playerType;
        }

        set
        {
            _playerType = value;
        }
    }

    //public int HelmID{
    //    get{return _helmID; }
    //    set{_helmID=value;}
    //}
    //public int ClothID{
    //    get{return _clothID;}
    //    set{_clothID=value;}
    //}
    //public int WeaponID{
    //    get{
    //        return _weaponID;
    //    }
    //    set{
    //        _weaponID = value;
    //    }
    //}
    //public int ShoesID{
    //    get{
    //        return _shoesID;
    //    }
    //    set{
    //        _shoesID = value;
    //    }
    //}
    //public int NecklaceID{
    //    get{return _necklaceID;}
    //    set{ _necklaceID=value; }
    //}
    //public int BraceletID{
    //    get{return _braceletID;}
    //    set{_braceletID=value;}
    //}
    //public int RingID{
    //    get{return _ringID;}
    //    set{_ringID=value;}
    //}
    //public int WingID{
    //    get{
    //        return _wingID;
    //    }
    //    set{
    //        _wingID=value;
    //    }
    //}

    #endregion

    #region unity event
    void Awake() {
        _instance = this;
    }

    void Start() {
        Init();
    }

    void Update() {
        //实现体力和历练的自动增长
        if (this.Energy < 100) {
            energyTimer += Time.deltaTime;
            if (energyTimer > 60) {
                Energy += 1;
                energyTimer -= 60;
                OnPlayerInfoChanged(InfoType.Energy);
            }
        } else {
            this.energyTimer = 0;
        }
        if (this.Toughen < 50) {
            toughenTimer += Time.deltaTime;
            if (toughenTimer > 60) {
                Toughen += 1;
                toughenTimer -= 60;
                OnPlayerInfoChanged(InfoType.Toughen);
            }
        } else {
            toughenTimer = 0;
        }

    }
    #endregion

    void Init() {
        this.Coin = 9870;
        this.Diamond = 1234;
        this.Energy = 78;
        this.Exp = 123;
        this.HeadPortrait = "头像底板女性";
        this.Level = 12;
        this.Name="千颂伊";
        this.Toughen = 34;
        this._playerType = PlayerType.Warrior;

        //this.BraceletID = 1001;
        //this.WingID = 1002;
        //this.RingID = 1003;
        //this.ClothID = 1004;
        //this.HelmID = 1005;
        //this.WeaponID = 1006;
        //this.NecklaceID = 1007;
        //this.ShoesID = 1008;

        InitHPDamagePower();

        OnPlayerInfoChanged( InfoType.All );
    }
    public void ChangeName(string newName) {
        this.Name = newName;
        OnPlayerInfoChanged(InfoType.Name);
    }
    public void DressOn( InventoryItem it ) {
        
        it.IsDressed = true;
        //首先检测有没有穿上相同类型的装备
        bool isDressed = false;
        InventoryItem inventoryItemDressed = null;
        switch (it.Inventory.EquipTYPE) {
            case EquipType.Bracelet:
                if (braceletInventoryItem != null) {
                    isDressed = true;
                    inventoryItemDressed = braceletInventoryItem;
                }
                    braceletInventoryItem = it;
                break;
            case EquipType.Cloth:
                if (clothInventoryItem != null) {
                    isDressed = true;
                    inventoryItemDressed = clothInventoryItem;
                }
                    clothInventoryItem = it;
                break;
            case EquipType.Helm:
                if (helmInventoryItem != null) {
                    isDressed = true;
                    inventoryItemDressed = helmInventoryItem;
                }
                    helmInventoryItem = it;
                break;
            case EquipType.Necklace:
                if (necklaceInventoryItem != null) {
                    isDressed = true;
                    inventoryItemDressed = necklaceInventoryItem;
                }
                    necklaceInventoryItem = it;
                break;
            case EquipType.Ring:
                if (ringInventoryItem != null) {
                    isDressed = true;
                    inventoryItemDressed = ringInventoryItem;
                }
                    ringInventoryItem = it;
                break;
            case EquipType.Shoes:
                if (shoesInventoryItem != null) {
                    isDressed = true;
                    inventoryItemDressed = shoesInventoryItem;
                }
                    shoesInventoryItem = it;
                break;
            case EquipType.Weapon:
                if (weaponInventoryItem != null) {
                    isDressed = true;
                    inventoryItemDressed = weaponInventoryItem;
                }
                    weaponInventoryItem = it;
                break;
            case EquipType.Wing:
                if(wingInventoryItem != null) {
                    isDressed = true;
                    inventoryItemDressed = wingInventoryItem;
                }
                    wingInventoryItem = it;
                break;
        }
        //有
        if (isDressed) {
            inventoryItemDressed.IsDressed = false;
            InventoryUI._instance.AddInventoryItem( inventoryItemDressed );
        }
        OnPlayerInfoChanged(InfoType.Equip);
        // 把已经存在的脱掉 放到背包
        //没有
        //直接穿上
    }

    public void DressOff(InventoryItem it) {
        it.IsDressed = false;
        switch (it.Inventory.EquipTYPE) {
            case EquipType.Bracelet:
                if (braceletInventoryItem != null) {
                    braceletInventoryItem = null;
                }
                break;
            case EquipType.Cloth:
                if (clothInventoryItem != null) {
                    clothInventoryItem = null;
                }
                break;
            case EquipType.Helm:
                if (helmInventoryItem != null) {
                    helmInventoryItem=null;
                }
                break;
            case EquipType.Necklace:
                if (necklaceInventoryItem != null) {
                    necklaceInventoryItem = null;
                }
                break;
            case EquipType.Ring:
                if (ringInventoryItem != null) {
                    ringInventoryItem = null;
                }
                break;
            case EquipType.Shoes:
                if (shoesInventoryItem != null) {
                    shoesInventoryItem = null;
                }
                break;
            case EquipType.Weapon:
                if (weaponInventoryItem != null) {
                    weaponInventoryItem = null;
                }
                break;
            case EquipType.Wing:
                if (wingInventoryItem != null) {
                    wingInventoryItem = null;
                }
                break;
        }

        InventoryUI._instance.AddInventoryItem(it);
        OnPlayerInfoChanged(InfoType.Equip);
    }

    public void InventoryUse( InventoryItem it,int count ) {
        //使用效果
        //TODO
        //处理物品使用后是否还存在
        it.Count-=count;
        if (it.Count <= 0) {
            InventoryManager._instance.inventoryItemList.Remove(it);
        }
    }

    //取得需要个数的金币数
    public bool GetCoin(int count) {
        if (Coin >= count) {
            Coin -= count;
            OnPlayerInfoChanged(InfoType.Coin);
            return true;
        }
        return false;
    }

    public void AddCoin(int count) {
        this.Coin += count;
        OnPlayerInfoChanged(InfoType.Coin);
    }

    public int GetOverallPower() {
        float power = this.Power;
        if (helmInventoryItem != null) {
            power += helmInventoryItem.Inventory.Power * (1 + (helmInventoryItem.Level - 1) / 10f);
        }
        if (clothInventoryItem != null) {
            power += clothInventoryItem.Inventory.Power * (1 + (clothInventoryItem.Level - 1) / 10f);
        }

        if (weaponInventoryItem != null) {
            power += weaponInventoryItem.Inventory.Power * (1 + (weaponInventoryItem.Level - 1) / 10f);
        }
        if (shoesInventoryItem != null) {
            power += shoesInventoryItem.Inventory.Power * (1 + (shoesInventoryItem.Level - 1) / 10f);
        }
        if (necklaceInventoryItem != null) {
            power += necklaceInventoryItem.Inventory.Power * (1 + (necklaceInventoryItem.Level - 1) / 10f);
        }
        if (braceletInventoryItem != null) {
            power += braceletInventoryItem.Inventory.Power * (1 + (braceletInventoryItem.Level - 1) / 10f);
        }
        if (ringInventoryItem != null) {
            power += ringInventoryItem.Inventory.Power * (1 + (ringInventoryItem.Level - 1) / 10f);
        }
        if (wingInventoryItem != null) {
            power += wingInventoryItem.Inventory.Power * (1 + (wingInventoryItem.Level - 1) / 10f);
        }
        return (int)power;
    }
    void InitHPDamagePower() {
        this.HP = this.Level * 100;
        this.Damage = this.Level * 50;
        this.Power = this.HP + this.Damage;
        
    }

    void PutonEquip(int id) {
        if (id == 0) return;
        Inventory inventory = null;
        bool isExit = InventoryManager._instance.inventoryDict.TryGetValue(id, out inventory);
       
        this.HP += inventory.HP;
        this.Damage += inventory.Damage;
        this.Power += inventory.Power;
    }
    void PutoffEquip(int id) {
        if (id == 0) return;
        Inventory inventory = null;
        InventoryManager._instance.inventoryDict.TryGetValue(id, out inventory);
        this.HP -= inventory.HP;
        this.Damage -= inventory.Damage;
        this.Power -= inventory.Power;
    }
}
