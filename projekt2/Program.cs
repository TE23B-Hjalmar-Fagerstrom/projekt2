using PlayerAndCharacter;
using EnemyAndCharacter;

Player p = new();
PlayerCharacter pc = new();
Enemy e = new();
EnemyCharacter ec = new();



int playAgain = 0;

while (playAgain != 2)
{
    Console.WriteLine("semi-auto battler");
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("1.new game   2.Quit");

    playAgain = TryParseChois(1, 2);

    if (playAgain == 1)
    {
        Game(p, pc, e, ec);
    }
}

static void Game(Player p, PlayerCharacter pc, Enemy e, EnemyCharacter ec)
{
    while (p.PlayerHealth > 0 && e.EnemyHealth > 0)
    {
        Fighet(ref p, pc, ref e, ec);
        Shop(ref p, ref pc, ref e);
        EnemySpending(p, ref e, ref ec);
        Interest(ref p, ref e);
    }
}

static void Fighet(ref Player p, PlayerCharacter pc, ref Enemy e, EnemyCharacter ec)
{
    while (pc.CharacterHealth > 0 && ec.CharacterHealth > 0)
    {
        PlayerTurn(ref pc, ref ec);
        Console.WriteLine();
        EnemyTurn(ref pc, ref ec);
        Console.ReadLine();

        if (pc.CharacterHealth > 0)
        {
            PlayerWin(ref p, ref e);
        }
        else
        {
            EnemyWin(ref p, ref e);
        }
    }
}

static void PlayerTurn(ref PlayerCharacter pc, ref EnemyCharacter ec)
{
    if (pc.CharacterHealth > 0 && ec.CharacterHealth > 0)
    {
        ec.CharacterHealth -= pc.CharacterDamage - ec.CharacterArmor;
        PCharacterEvade(ref pc, ec, out int evade);
        if (evade == 0)
        {
            Console.WriteLine($"Du träfade fiendens gube och den har {ec.CharacterHealth} HP kvar");
        }
    }
}

static void EnemyTurn(ref PlayerCharacter pc, ref EnemyCharacter ec)
{
    if (pc.CharacterHealth > 0 && ec.CharacterHealth > 0)
    {
        pc.CharacterHealth -= ec.CharacterDamage - pc.CharacterArmor;
        ECharacterEvade(pc, ref ec, out int evade);
        if (evade == 0)
        {
            Console.WriteLine($"Fienden träfade din gube och den har {pc.CharacterHealth} HP kvar");
        }
    }
}

static void PCharacterEvade(ref PlayerCharacter pc, EnemyCharacter ec, out int evade) // kollar om spelaren undvek
{
    evade = 0;
    if (pc.CharacterEvadeProbability < pc.CharacterEvadeChance)
    {
        Console.WriteLine("Du undvek fiendens attack");
        pc.CharacterHealth += ec.CharacterDamage - pc.CharacterArmor;
        evade = 1;
    }
}

static void ECharacterEvade(PlayerCharacter pc, ref EnemyCharacter ec, out int evade) // kollar om fienden undvek
{
    evade = 0;
    if (ec.CharacterEvadeProbability < ec.CharacterEvadeChance)
    {
        Console.WriteLine("Fienden undvek din attack");
        ec.CharacterHealth += pc.CharacterDamage - ec.CharacterArmor;
        evade = 1;
    }
}

static void PlayerWin(ref Player p, ref Enemy e)
{
    int damageUpgrade = 0;
    for (int i = 1; i <= 25; i++)
    {
        if (p.PlayerGoldSpent / 20 >= i)
        {
            damageUpgrade += 1;
        }
    }
    e.EnemyHealth -= p.PlayerDamage + damageUpgrade;

    p.PlayerGold += 15;
    e.EnemyGold += 10;
}

static void EnemyWin(ref Player p, ref Enemy e)
{
    int damageUpgrade = 0;
    for (int i = 1; i <= 25; i++)
    {
        if (e.EnemyGoldSpent / 20 >= i)
        {
            damageUpgrade += 1;
        }
    }
    p.PlayerHealth -= e.EnemyDamage + damageUpgrade;

    p.PlayerGold += 10;
    e.EnemyGold += 15;
}

static void Shop(ref Player p, ref PlayerCharacter pc, ref Enemy e)
{
    int chois = 0;
    if (p.PlayerHealth > 0 && e.EnemyHealth > 0)
    {
        while (chois != 5)
        {
            Console.WriteLine(""); // skriv utt spelar och enemy helth
            Console.WriteLine("kostnad på allt är 10 guld");
            Console.WriteLine($"du har {p.PlayerGold} guld");
            Console.WriteLine("");
            Console.WriteLine($"1. uppgradera HP (+10) aktuell HP ({pc.CharacterHealth})");
            Console.WriteLine($"2. uppgradera ATK (+5) aktuell ATK ({pc.CharacterDamage})");
            Console.WriteLine($"3. uppgradera ARMOR (+5) aktuell ARMOR ({pc.CharacterArmor})");
            Console.WriteLine($"4. uppgradera EVADE (+2%) aktuell EVADE ({pc.CharacterEvadeChance}%)");
            Console.WriteLine($"5. quit shop");

            chois = TryParseChois(1, 5);
            Buy(ref p, ref pc, ref chois);
        }
    }
}

static void Buy(ref Player p, ref PlayerCharacter pc, ref int chois)
{
    if (chois != 5 && p.PlayerGold >= 10)
    {
        switch (chois) // ChatGPT: jag frågade om den kunde göra en mindre if-sats fyld lösning 
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

static void EnemySpending(Player p, ref Enemy e, ref EnemyCharacter ec)
{
    while (e.EnemyGold >= 110 && e.EnemyHealth - p.PlayerDamage > 0 || e.EnemyGold >= 10 && e.EnemyHealth - p.PlayerDamage <= 0)
    {
        int random = Random.Shared.Next(1, 5);

        switch (random)
        {
            case 1: ec.CharacterHealth += 10; break;
            case 2: ec.CharacterDamage += 5; break;
            case 3: ec.CharacterArmor += 5; break;
            case 4: ec.CharacterEvadeChance += 2; break;
        }
        e.EnemyGold -= 10;
        e.EnemyGoldSpent += 10;
    }
}

static void Interest(ref Player p, ref Enemy e)
{
    for (int i = 1; i <= 10; i++)
    {
        if (p.PlayerGold / 10 >= i)
        {
            p.PlayerGold += 1;
        }

        if (e.EnemyGold / 10 >= i)
        {
            e.EnemyGold += 1;
        }
    }
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