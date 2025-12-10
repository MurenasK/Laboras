public abstract class Player
{
    public string Name { get; set; }
    public string Class { get; set; }
    public int Life { get; set; }
    public int Mana { get; set; }
    public int Dmg { get; set; }
    public int Armor { get; set; }

    public string Race { get; set; }
    public string City { get; set; }

    /// <summary>
    /// Inicializuoja bazinį Player objektą su pagrindinėmis kovos savybėmis.
    /// </summary>
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

    /// <summary>
    /// Sugeneruoja bendrą žaidėjo atributų eilutę lentelės formatui.
    /// </summary>
    public virtual string ToBaseString()
    {
        return string.Format(
            "| {0,-10} | {1,-20} | {2,-15} | {3,-15} | {4,10} | {5,10} |" +
            " {6,10} | {7,10} |",
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

    /// <summary>
    /// Sugeneruoja bendrus žaidėjo duomenis CSV formato eilutei.
    /// </summary>
    public virtual string ToCsvString()
    {
        return $"{GetPlayerType()};{Name};{Class};{Race};{Life};{Mana};" +
            $"{Dmg};{Armor}";
    }

    /// <summary>
    /// Grąžina bazinį žaidėjo tekstinį vaizdavimą.
    /// </summary>
    public override string ToString()
    {
        return ToBaseString();
    }

    /// <summary>
    /// Grąžina žaidėjo tipą (pvz., Heroes, NPC).
    /// </summary>
    public abstract string GetPlayerType();

    /// <summary>
    /// Sukuria gilią žaidėjo objekto kopiją.
    /// </summary>
    public abstract Player Clone();
}
