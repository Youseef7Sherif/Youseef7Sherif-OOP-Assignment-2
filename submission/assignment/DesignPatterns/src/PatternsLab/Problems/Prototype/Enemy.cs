namespace PatternsLab.Problems.Prototype;

public class Weapon
{
    public string Name { get; set; }
    public int Damage { get; set; }
}

public abstract class Enemy
{
    private string _modelData;

    public string Name { get; set; }
    public int Health { get; set; }
    public Weapon Weapon { get; set; }
    public List<string> Abilities { get; set; } = new();
    public string ModelId => _modelData;

    protected Enemy()
    {
        Console.WriteLine("   ...loading 3D model (slow)...");
        Thread.Sleep(500);
        _modelData = "MODEL_" + Guid.NewGuid().ToString("N")[..6];
    }

    public Enemy Clone()
    {
        var clone = (Enemy)this.MemberwiseClone();
        clone.Weapon = new Weapon { Name = Weapon.Name, Damage = Weapon.Damage };
        clone.Abilities = new List<string>(Abilities);
        return clone;// تعبت في فهمها دي جامد والله لحد ما طبقتها لوحدي من غير ما اخد حل جاهز وخلاص 
    }
}

public class Orc : Enemy
{
    public Orc()
    {
        Name = "Orc";
        Health = 100;
        Weapon = new Weapon { Name = "Axe", Damage = 25 };
        Abilities.Add("Rage");
    }
}

public class Elf : Enemy
{
    public Elf()
    {
        Name = "Elf";
        Health = 70;
        Weapon = new Weapon { Name = "Bow", Damage = 18 };
        Abilities.Add("Stealth");
    }
}
public class EnemyRegistry
{
    private readonly Dictionary<string, Enemy> _prototypes = new();

    public void Register(string name, Enemy prototype)
    {
        _prototypes[name] = prototype;// اغير فيه عادي انما مخلهوش يشاور علي ديكشنري جديد 
    }

    public Enemy GetClone(string name)
    {
        if (!_prototypes.ContainsKey(name))
            throw new KeyNotFoundException($"No prototype registered with name: {name}");

        return _prototypes[name].Clone();
    }
}