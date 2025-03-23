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
    shop(p, pc, e);
    Console.WriteLine($"{pc.CharacterHealth}");
}
Console.ReadLine();

static void Game(Player p, PlayerCharacter pc, Enemy e, EnemyCharacter ec)
{
    while (p.PlayerHealth > 0 && e.EnemyHealth > 0)
    {

    }
}

static void Fighet(PlayerCharacter pc, EnemyCharacter ec)
{
    while (pc.CharacterHealth > 0 && ec.CharacterHealth > 0)
    {

    }
}

static (int, int, int) shop(Player p, PlayerCharacter pc, Enemy e)
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
        }
    }
    return buy(p, pc, chois);
}

static int pCharacterEvade(PlayerCharacter pc, EnemyCharacter ec)
{
    if (pc.CharacterEvadeProbability < pc.CharacterEvadeChance)
    {
        Console.WriteLine("du undvek fiendens attack");
        pc.CharacterHealth += ec.CharacterDamage;
    }
    return pc.CharacterHealth;
}

static int eCharacterEvade(PlayerCharacter pc, EnemyCharacter ec)
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

static (int, int, int) buy(Player p, PlayerCharacter pc, int chois)
{
    int[] uppgradeAmount = { 10, 5, 5, 2 };
    int[] uppgrade = { pc.CharacterHealth, pc.CharacterDamage, pc.CharacterArmor, pc.CharacterEvadeChance, pc.CharacterEvadeProbability };

    if (p.PlayerGold >= 10 && chois != 5)
    {
        for (int i = 0; i < 4; i++)
        {
            if (chois == i)
            {
                uppgrade[i-1] += uppgradeAmount[i-1];
                p.PlayerGold -= 10; p.PlayerGoldSpent += 10;
            }
        }
    }
    else if (p.PlayerGold < 10 && chois != 5)
    {
        Console.WriteLine("du har inte råd");
    }
    return (p.PlayerGold, uppgrade[chois-1], p.PlayerGoldSpent);
}