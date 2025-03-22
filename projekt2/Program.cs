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
}
Console.ReadLine();

static void Game(Player p, PlayerCharacter pc, Enemy e, EnemyCharacter ec)
{
    while (p.playerHealth > 0 && e.EnemyHealth > 0)
    {

    }
}

static void Fighet(PlayerCharacter pc, EnemyCharacter ec)
{
    while (pc.characterHealth > 0 && ec.characterHealth > 0)
    {

    }
}

static void shop(Player p, PlayerCharacter pc, Enemy e)
{
    if (p.playerHealth > 0 && e.EnemyHealth > 0)
    {
        int chois = 0;
        while (chois != 5)
        {
            Console.WriteLine("kostnad på allt är 10 guld");
            Console.WriteLine($"du har {p.playerGold} guld");
            Console.WriteLine("");
            Console.WriteLine($"1. uppgradera HP (+10) aktuell HP ({pc.characterHealth})");
            Console.WriteLine($"2. uppgradera ATK (+5) aktuell atk ({pc.characterDamage})");
            Console.WriteLine($"3. uppgradera ARMOR (+5) aktuell ARMOR ({pc.characterArmor})");
            Console.WriteLine($"4. uppgradera EVADE (+2%) aktuell EVADE ({pc.characterEvadeChance}%)");
            Console.WriteLine($"5. quit shop");

            chois = TryParseChois(1, 5);

            buy(p, pc, chois);
        }
    }
}

static int pCharacterEvade(PlayerCharacter pc, EnemyCharacter ec)
{
    if (pc.characterEvadeProbability < pc.characterEvadeChance)
    {
        Console.WriteLine("du undvek fiendens attack");
        pc.characterHealth += ec.characterDamage;
    }
    return pc.characterHealth;
}

static int eCharacterEvade(PlayerCharacter pc, EnemyCharacter ec)
{
    if (ec.characterEvadeProbability < ec.characterEvadeChance)
    {
        Console.WriteLine("fienden undvek din attack");
        ec.characterHealth += pc.characterDamage;
    }
    return ec.characterHealth;
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

static int buy(Player p, PlayerCharacter pc, int chois)
{
    int[] uppgradeAmount = { 10, 5, 5, 2 };
    int[] uppgrade = { pc.characterHealth, pc.characterDamage, pc.characterArmor, pc.characterEvadeChance };

    if (p.playerGold > 10)
    {
        for (int i = 0; i < 5; i++)
        {
            if (chois == i)
            {
                uppgrade[i] += uppgradeAmount[i];
                return p.playerGold -= 10;
            }
        }
        return uppgrade[chois];
    }
    else
    {
        Console.WriteLine("du har inte råd");
    }
    return p.playerGoldSpent += 10;
}