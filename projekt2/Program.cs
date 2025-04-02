using PlayerAndCharacter;
using EnemyAndCharacter;

Player p = new();
PlayerCharacter pc = new();
Enemy e = new();
EnemyCharacter ec = new();

Console.WriteLine("semi-auto battler");
Console.WriteLine("");
Console.WriteLine("");
Console.WriteLine("1.new game   2.Quit");

if (TryParseChois(1, 2) == 1)
{
    Console.WriteLine("good");
    Shop(ref p, ref pc, ref e);
    Console.WriteLine($"{pc.CharacterHealth}");
}
Console.ReadLine();

static void Game(Player p, PlayerCharacter pc, Enemy e, EnemyCharacter ec)
{
    while (p.PlayerHealth > 0 && e.EnemyHealth > 0)
    {
        Fighet(ref p, pc, ref e, ec);
        Shop(ref p, ref pc, ref e);
        Interest(ref p, ref e);
    }
}

static void Fighet(ref Player p, PlayerCharacter pc, ref Enemy e, EnemyCharacter ec)
{
    while (pc.CharacterHealth > 0 && ec.CharacterHealth > 0)
    {

    }
}

static void Shop(ref Player p, ref PlayerCharacter pc, ref Enemy e)
{
    int chois = 0;
    if (p.PlayerHealth > 0 && e.EnemyHealth > 0)
    {
        while (chois != 5)
        {
            Console.WriteLine("kostnad på allt är 10 guld");
            Console.WriteLine($"du har {p.PlayerGold} guld");
            Console.WriteLine("");
            Console.WriteLine($"1. uppgradera HP (+10) aktuell HP ({pc.CharacterHealth})");
            Console.WriteLine($"2. uppgradera ATK (+5) aktuell atk ({pc.CharacterDamage})");
            Console.WriteLine($"3. uppgradera ARMOR (+5) aktuell ARMOR ({pc.CharacterArmor})");
            Console.WriteLine($"4. uppgradera EVADE (+2%) aktuell EVADE ({pc.CharacterEvadeChance}%)");
            Console.WriteLine($"5. quit shop");

            chois = TryParseChois(1, 5);
            Buy(ref p, ref pc, ref chois);
        }
    }
}

static int PCharacterEvade(PlayerCharacter pc, EnemyCharacter ec)
{
    if (pc.CharacterEvadeProbability < pc.CharacterEvadeChance)
    {
        Console.WriteLine("du undvek fiendens attack");
        pc.CharacterHealth += ec.CharacterDamage;
    }
    return pc.CharacterHealth;
}

static int ECharacterEvade(PlayerCharacter pc, EnemyCharacter ec)
{
    if (ec.CharacterEvadeProbability < ec.CharacterEvadeChance)
    {
        Console.WriteLine("fienden undvek din attack");
        ec.CharacterHealth += pc.CharacterDamage;
    }
    return ec.CharacterHealth;
}

static int TryParseChois(int min, int max)
{
    int choisToNumber = 0;
    while (choisToNumber < min || choisToNumber > max)
    {
        string chois = Console.ReadLine();
        int.TryParse(chois, out choisToNumber);
        if (choisToNumber < min || choisToNumber > max)
        {
            Console.WriteLine($"skriv ett tal mellan {min} och {max}");
        }
    }
    return choisToNumber;
}

static void Buy(ref Player p, ref PlayerCharacter pc, ref int chois)
{
    if (chois != 5 && p.PlayerGold >= 10)
    {
        switch (chois) // chatGPT: jag frågade om den kunde göra en mindre if-sats fyld lösning 
        {
            case 1: pc.CharacterHealth += 10; break;
            case 2: pc.CharacterDamage += 5; break;
            case 3: pc.CharacterArmor += 5; break;
            case 4: pc.CharacterEvadeChance += 2; break;
        }
        p.PlayerGold -= 10;
        p.PlayerGoldSpent += 10;
    }
    else if (p.PlayerGold < 10 && chois != 5)
    {
        Console.WriteLine("du har inte råd");
        Console.ReadLine();
    }
}

static void Interest(ref Player p, ref Enemy e)
{
    for (int i = 0; i < 10; i++)
    {
        if (p.PlayerGold / 10 == i)
        {
            p.PlayerGold += 1 * i;
        }

        if (e.EnemyGold / 10 == i)
        {
            e.EnemyGold += 1 * i;
        }
    }
}