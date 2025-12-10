public abstract class Player
{
    public string Name { get; set; }
    public string Class { get; set; }
    public int Life { get; set; }
    public int Mana { get; set; }
    public int Dmg { get; set; }
    public int Armor { get; set; }

    public string Race { get; set; }    

    public Player(string name, string playerClass, int life, int mana,
        int dmg, int armor)
    {
        Name = name;
        Class = playerClass;
        Life = life;
        Mana = mana;
        Dmg = dmg;
        Armor = armor;
    }

    public virtual string ToBaseString()
    {
        return string.Format(
            "| {0,-10} | {1,-20} | {2,-15} | {3,-15} | {4,10} | {5,10} | {6,10} | {7,10} |",
            GetPlayerType(),
            Name,
            Class,
            Race,
            Life,
            Mana,
            Dmg,
            Armor
        );
    }

    public virtual string ToCsvString()
    {
        return $"{GetPlayerType()};{Name};{Class};{Race};{Life};{Mana};{Dmg};{Armor}";
    }



    public override string ToString()
    {
        return ToBaseString();
    }

    public abstract string GetPlayerType();
    public abstract Player Clone();
}
