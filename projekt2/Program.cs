using PlayerAndCharacter;
using EnemyAndCharacter;

Player p = new();
PlayerCharacter pc = new();
Enemy e = new();
EnemyCharacter ec = new();

shop(p, pc, e);
// Console.WriteLine("semi-auto battler");
// Console.WriteLine("");
// Console.WriteLine("");
// Console.WriteLine("1.new game   2.Quit");

// if (TryParseChois(1, 2) == 1)
// {
//     Console.WriteLine("good");
// }
// Console.ReadLine();

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
        while (TryParseChois(1, 5) != 5)
        {
            Console.WriteLine("kostnad på allt 10 Guld");
            Console.WriteLine($"du har {p.playerGold}");
            Console.WriteLine("");
            Console.WriteLine("1. uppgradera HP (+10)");
            Console.WriteLine("2. uppgradera ATK (+5)");
            Console.WriteLine("3. uppgradera ARMOR (+5)");
            Console.WriteLine("4. uppgradera EVADE (+5%)");

            int[] uppgradeAmount = { 10, 5, 5, 5 };
            int[] uppgrade = { pc.characterHealth, pc.characterDamage, pc.characterArmor, pc.characterEvadeChance };

            if (p.playerGold > 10)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (TryParseChois(1, 5) == i)
                    {
                        uppgrade[i] += uppgradeAmount[i];
                    }
                }
            }else{
                Console.WriteLine("du har inte råd");
            }
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
    }
    return choisToNumber;
}