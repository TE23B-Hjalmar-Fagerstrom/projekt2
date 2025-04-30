using PlayerAndCharacter;
using EnemyAndCharacter;

int playAgain = 0;
int gamesPlayed = 0;

while (playAgain != 2)
{
    Player p = new();
    PlayerCharacter pc = new();
    Enemy e = new();
    EnemyCharacter ec = new();

    p.hasSeenTutorial =+ gamesPlayed;

    Console.WriteLine("semi-auto battler");
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("1.new game   2.Quit");

    playAgain = TryParseChois(1, 2);

    if (playAgain == 1)
    {
        Tutorial(0, ref p);
        Tutorial(1, ref p);
        Game(p, pc, e, ec);
    }

    Tutorial(4, ref p);

    gamesPlayed++;

    Space(10);
}

static void Game(Player p, PlayerCharacter pc, Enemy e, EnemyCharacter ec)
{
    while (p.PlayerHealth > 0 && e.EnemyHealth > 0)
    {
        Space(5);
        Fighet(ref p, pc, ref e, ec);
        EnemySpending(p, ref e, ref ec);
        Tutorial(2, ref p);
        Shop(ref p, ref pc, ref e);
        Interest(ref p, ref e);
    }
}

static void Fighet(ref Player p, PlayerCharacter pc, ref Enemy e, EnemyCharacter ec)
{
    pc.CharacterHealth = pc.CharacterMaxHealth;
    ec.CharacterHealth = ec.CharacterMaxHealth;
    while (pc.CharacterHealth > 0 && ec.CharacterHealth > 0)
    {
        Space(10);
        pc.CharacterHealth = EnemyTurn(pc, ec);
        Console.WriteLine();
        ec.CharacterHealth = PlayerTurn(pc, ec);
        Console.ReadLine();

        if (pc.CharacterHealth > 0 && ec.CharacterHealth <= 0)
        {
            PlayerWin(ref p, ref e);
        }
        else if (pc.CharacterHealth <= 0 && ec.CharacterHealth > 0)
        {
            EnemyWin(ref p, ref e);
        }
    }
}

static int PlayerTurn(PlayerCharacter pc, EnemyCharacter ec)
{
    if (pc.CharacterHealth > 0 && ec.CharacterHealth > 0)
    {
        ec.CharacterHealth -= pc.CharacterDamage - ec.CharacterArmor;
        ec.CharacterHealth = ECharacterEvade(pc, ec, out int evade);
        if (evade == 0)
        {
            Console.WriteLine($"Du träfade fiendens gube och den har {ec.CharacterHealth} HP kvar");
        }
    }
    return ec.CharacterHealth;
}

static int EnemyTurn(PlayerCharacter pc, EnemyCharacter ec)
{
    if (pc.CharacterHealth > 0 && ec.CharacterHealth > 0)
    {
        pc.CharacterHealth -= ec.CharacterDamage - pc.CharacterArmor;
        pc.CharacterHealth = PCharacterEvade(pc, ec, out int evade);
        if (evade == 0)
        {
            Console.WriteLine($"Fienden träfade din gube och den har {pc.CharacterHealth} HP kvar");
        }
    }
    return pc.CharacterHealth;
}

static int PCharacterEvade(PlayerCharacter pc, EnemyCharacter ec, out int evade) // kollar om spelaren undvek
{
    evade = 0;
    if (pc.CharacterEvadeProbability < pc.CharacterEvadeChance)
    {
        Console.WriteLine("Du undvek fiendens attack");
        pc.CharacterHealth += ec.CharacterDamage - pc.CharacterArmor;
        evade = 1;
    }
    return pc.CharacterHealth;
}

static int ECharacterEvade(PlayerCharacter pc, EnemyCharacter ec, out int evade) // kollar om fienden undvek
{
    evade = 0;
    if (ec.CharacterEvadeProbability < ec.CharacterEvadeChance)
    {
        Console.WriteLine("Fienden undvek din attack");
        ec.CharacterHealth += pc.CharacterDamage - ec.CharacterArmor;
        evade = 1;
    }
    return ec.CharacterHealth;
}

static void PlayerWin(ref Player p, ref Enemy e)
{
    e.EnemyHealth -= p.PlayerDamage;

    p.PlayerGold += 15;
    e.EnemyGold += 10;
}

static void EnemyWin(ref Player p, ref Enemy e)
{
    p.PlayerHealth -= e.EnemyDamage;

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
            Space(10);
            Console.WriteLine($"Ditt HP {p.PlayerHealth} och ATK {p.PlayerDamage}"); // skriv utt spelar health och ATK
            Console.WriteLine($"fiendens HP {e.EnemyHealth} och ATK {e.EnemyDamage}"); // skriv utt enemy -||-
            Console.WriteLine("");
            Console.WriteLine("kostnad på allt är 10 guld");
            Console.WriteLine($"du har {p.PlayerGold} guld");
            Console.WriteLine("");
            Console.WriteLine($"1. uppgradera HP (+10) aktuell HP ({pc.CharacterMaxHealth})");
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
            case 1: pc.CharacterMaxHealth += 10; break;
            case 2: pc.CharacterDamage += 5; break;
            case 3: pc.CharacterArmor += 5; break;
            case 4: pc.CharacterEvadeChance += 2; break;
        }
        p.PlayerGold -= 10;
        p.PlayerGoldSpent += 10;
        PlayerDamageUppgrade(ref p);
    }
    else if (p.PlayerGold < 10 && chois != 5)
    {
        Console.WriteLine("du har inte råd");
        Console.ReadLine();
    }
    Tutorial(3, ref p);
}

static void PlayerDamageUppgrade(ref Player p)
{
    int damageUpgrade = 0;

    for (int i = 1; i <= 25; i++)
    {
        if ((p.PlayerGoldSpent / (5 * p.PlayerDamage)) >= i)
        {
            damageUpgrade++;
            p.PlayerDamage += damageUpgrade;
        }
    }
}

static void EnemySpending(Player p, ref Enemy e, ref EnemyCharacter ec)
{
    while (e.EnemyGold >= 110 && (e.EnemyHealth - p.PlayerDamage) > 0 || e.EnemyGold >= 10 && (e.EnemyHealth - p.PlayerDamage) <= 0)
    {
        int random = Random.Shared.Next(1, 5);

        switch (random)
        {
            case 1: ec.CharacterMaxHealth += 10; break;
            case 2: ec.CharacterDamage += 5; break;
            case 3: ec.CharacterArmor += 5; break;
            case 4: ec.CharacterEvadeChance += 2; break;
        }

        e.EnemyGold -= 10;
        e.EnemyGoldSpent += 10;
        EnemyDamageUppgrade(ref e);
    }
}

static void EnemyDamageUppgrade(ref Enemy e)
{
    int damageUpgrade = 0;

    for (int i = 1; i <= 30; i++)
    {
        if ((e.EnemyGoldSpent / (40 * e.EnemyDamage)) >= i)
        {
            damageUpgrade++;
            e.EnemyDamage += damageUpgrade;
        }
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

static void Space(int length)
{
    for (int i = 0; i < length; i++)
    {
        Console.WriteLine("");
    }
}

static void Tutorial(int tutorialSteps, ref Player p) // den här metoden är för att ge spelaren förståelse för hur spelet funkar
{
    // jag valde en array för att jag vet att jag inte kommer behöva mera text än det här. 
    string[] tutorial = [
        "hej, jag är här för att lära dig hur spelet fungerar. Tillat börja med så kommer du att behöva trycka [ENTER] för att komma ur den här konversationen.",
        "Bra, under stridens gång kommer du att behöva trycka [ENTER] en massa gånger för att fortsätta den. Efter du är klar med striden kommer jag och förklarar vad som kommer efter den.",
        "Nu när striden är slut så har du en möjlighet att uppgradera din gubbe. Eller så kan du spara dina pengar för att få mer pengar. För varje 10 guld du håller får du 1 extra, du kan som max få 10 guld (eller i andra ord om du håller 100 guld)",
        "Nu när du har spenderat ditt första guld kan jag förklara för dig att för varje 40 guld du spenderar kommer du att göra mer skada på din fiende om du vinner striden. Detsamma gäller för din fiende, så håll ett öga på deras attack så att du inte sparar pengar som kan ha rädda dig.",
        "Nu så har du klarat din första match och jag har lärt dig allt jag kan. Nu är det upp till dig att hitta på olicka sätt att vinna över fienden hur du än vill. Lycka till"
    ];

    if (p.hasSeenTutorial < tutorialSteps)
    {
        for (int i = 0; i < 5; i++)
        {
            if (tutorialSteps == i)
            {
                Console.WriteLine(tutorial[tutorialSteps]);
            }
        }
        Console.ReadLine();
        p.hasSeenTutorial++;
    }
}