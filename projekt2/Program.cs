using PlayerAndCharacter; // alla spelarens och karaktärens variabler 
using EnemyAndCharacter; // alla fiendens och karaktärens variabler 

int playAgain = 0;
int gamesPlayed = -1; // gör så att alla tutoriellerna är synliga första gången man spelar 

while (playAgain != 2) // så länge spelaren inte väljer quit så fortsätter spelet
{
    // säter allas värden till start värdena 
    Player p = new();
    PlayerCharacter pc = new();
    Enemy e = new();
    EnemyCharacter ec = new();

    p.hasSeenTutorial =+ gamesPlayed; // gör så att tutoriellerna inte dyckerup om man spelar spelet igen

    Console.WriteLine("semi-auto battler");
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("1.New Game   2.Quit");

    playAgain = TryParseChois(1, 2); // spelaren väljer antingen "new game" eller "quit"

    if (playAgain == 1) // om spelaren valde "new game" 
    {
        Tutorial(0, ref p);
        Tutorial(1, ref p);
        Game(p, pc, e, ec);
    }

    Tutorial(4, ref p); // en uppmaning att pröva olika strategier

    gamesPlayed += 5; // gör så att tutoriellerna inte dyckerup om man spelar spelet igen

    Space(10);
}

// deta är hela spelet 
static void Game(Player p, PlayerCharacter pc, Enemy e, EnemyCharacter ec) 
{
    while (p.PlayerHealth > 0 && e.EnemyHealth > 0) // sålänge båda lever 
    {
        // alla spelets delar
        Space(5);
        Fighet(ref p, pc, ref e, ec);
        EnemySpending(p, ref e, ref ec);
        Tutorial(2, ref p);
        Shop(ref p, ref pc, ref e);
        Interest(ref p, ref e);
    }
}

// hela striden och vad man får efter striden 
static void Fighet(ref Player p, PlayerCharacter pc, ref Enemy e, EnemyCharacter ec) 
{
    pc.CharacterHealth = pc.CharacterMaxHealth;
    ec.CharacterHealth = ec.CharacterMaxHealth;

    while (pc.CharacterHealth > 0 && ec.CharacterHealth > 0) // så länge båda karaktärerna lever 
    {
        Space(10);
        pc.CharacterHealth = EnemyTurn(pc, ec);
        Console.WriteLine();
        ec.CharacterHealth = PlayerTurn(pc, ec);
        Console.ReadLine();

        if (pc.CharacterHealth > 0 && ec.CharacterHealth <= 0) // om fienden har 0 HP eller mindre vinner spelaren
        {
            PlayerWin(ref p, ref e);
        }
        else if (pc.CharacterHealth <= 0 && ec.CharacterHealth > 0) // om spelaren har 0 HP eller minder vinner fienden
        {
            EnemyWin(ref p, ref e);
        }
    }
}

static int PlayerTurn(PlayerCharacter pc, EnemyCharacter ec) // när det är spelarens tur att anfalla
{
    if (pc.CharacterHealth > 0 && ec.CharacterHealth > 0) // om båda karaktärna är levande så händer nedanstående 
    {
        if (ec.CharacterArmor > pc.CharacterDamage) // gör så fiendens karaktär inte får HP om den har mer armor än spelarens karaktärs ATK
        {
            ec.CharacterArmor = pc.CharacterDamage;
        }

        ec.CharacterHealth -= pc.CharacterDamage - ec.CharacterArmor;
        ec.CharacterHealth = ECharacterEvade(pc, ec, out int evade);
        if (evade == 0) // om fienden inte undvek
        {
            Console.WriteLine($"Du träfade fiendens gube och den har {ec.CharacterHealth} HP kvar");
        }
    }

    return ec.CharacterHealth; // ger tillbaka fiende karaktärs HP
}

static int EnemyTurn(PlayerCharacter pc, EnemyCharacter ec) // när det är fiendens tur att anfalla
{
    if (pc.CharacterHealth > 0 && ec.CharacterHealth > 0) // om båda karaktärna är levande så händer nedanstående
    {
        if (pc.CharacterArmor > ec.CharacterDamage) // gör så spelarens karaktär inte får HP om den har mer armor än fiende karaktärens ATK
        {
            pc.CharacterArmor = ec.CharacterDamage;
        }

        pc.CharacterHealth -= ec.CharacterDamage - pc.CharacterArmor;
        pc.CharacterHealth = PCharacterEvade(pc, ec, out int evade);

        if (evade == 0) // om spelaren inte undvek 
        {
            Console.WriteLine($"Fienden träfade din gube och den har {pc.CharacterHealth} HP kvar");
        }
    }

    return pc.CharacterHealth; // ger tillbaka spelar karaktärens HP
}

static int PCharacterEvade(PlayerCharacter pc, EnemyCharacter ec, out int evade) // kollar om spelaren undvek
{
    evade = 0;

    // prob kommer slumpa ett tal mellan 1 och 100 och om det är under eller lika med Chance så undviker karaktären
    if (pc.CharacterEvadeProbability <= pc.CharacterEvadeChance) 
    {
        Console.WriteLine("Du undvek fiendens attack");
        pc.CharacterHealth += ec.CharacterDamage - pc.CharacterArmor;
        evade = 1;
    }

    return pc.CharacterHealth; // ger tillbaka spelar karaktärens HP
}

static int ECharacterEvade(PlayerCharacter pc, EnemyCharacter ec, out int evade) // kollar om fienden undvek
{
    evade = 0;

    // prob kommer slumpa ett tal mellan 1 och 100 och om det är under eller lika med Chance så undviker karaktären
    if (ec.CharacterEvadeProbability <= ec.CharacterEvadeChance) 
    {
        Console.WriteLine("Fienden undvek din attack");
        ec.CharacterHealth += pc.CharacterDamage - ec.CharacterArmor;
        evade = 1;
    }

    return ec.CharacterHealth; // ger tillbaka fiende karaktärens HP
}

static void PlayerWin(ref Player p, ref Enemy e) // om spelaren vinner 
{
    e.EnemyHealth -= p.PlayerDamage; // fienden tar skada från spelaren 

    // vinst / förlust intäkt
    p.PlayerGold += 15;
    e.EnemyGold += 10;
}

static void EnemyWin(ref Player p, ref Enemy e) // om fienden vinner 
{
    p.PlayerHealth -= e.EnemyDamage; // spelaren tar skada från fienden

    // vinst / förlust intäkt
    p.PlayerGold += 10;
    e.EnemyGold += 15;
}

// i affären ser du vad du kan köpa, hur mycket HP och DMG du och fienden gör och hur mycket guld du har
static void Shop(ref Player p, ref PlayerCharacter pc, ref Enemy e) 
{
    int chois = 0;
    if (p.PlayerHealth > 0 && e.EnemyHealth > 0) // om båda är vid liv
    {
        while (chois != 5) // sålänge spelaren inte lämnar affären 
        {
            Space(10);
            Console.WriteLine($"Ditt HP {p.PlayerHealth} och ATK {p.PlayerDamage}"); // skriv utt spelar HP och ATK
            Console.WriteLine($"fiendens HP {e.EnemyHealth} och ATK {e.EnemyDamage}"); // skriv utt enemy -||-
            Console.WriteLine("");
            Console.WriteLine("kostnad på allt är 10 guld");
            Console.WriteLine($"du har {p.PlayerGold} guld"); // skriver utt hur mycket guld du har
            Console.WriteLine("");
            Console.WriteLine($"1. uppgradera HP (+10) aktuell HP ({pc.CharacterMaxHealth})");
            Console.WriteLine($"2. uppgradera ATK (+5) aktuell ATK ({pc.CharacterDamage})");
            Console.WriteLine($"3. uppgradera ARMOR (+5) aktuell ARMOR ({pc.CharacterArmor})");
            Console.WriteLine($"4. uppgradera EVADE (+2%) aktuell EVADE ({pc.CharacterEvadeChance}%)");
            Console.WriteLine($"5. quit shop");

            chois = TryParseChois(1, 5); // spelaren bestämmer för att köpa något eller att lämna affären
            Buy(ref p, ref pc, ref chois); // är där transaktionen av värden händer 
        }
    }
}

// om spelaren bestämde sig för att köpa något från affären så görs transaktion av värden här
static void Buy(ref Player p, ref PlayerCharacter pc, ref int chois) 
{
    if (chois != 5 && p.PlayerGold >= 10) // om spelaren har råd och inte lämna affären
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
        PlayerDamageUppgrade(ref p); // uppgraderar spelarens DMG beroende på om alla krav uppfylls
    }
    else if (p.PlayerGold < 10 && chois != 5) // om spelaren inte har råd och inte lämna affären
    {
        Console.WriteLine("du har inte råd");
        Console.ReadLine();
    }

    Tutorial(3, ref p); // ger tips till spelaren efter ha gjort sitt första köp 
}

static void PlayerDamageUppgrade(ref Player p) // gör så att spelaren gör mer DMG efter en vis mängd guld spenderas
{
    for (int i = 1; i <= 25; i++) // upprepas 25 gånger så spelaren kan ha en max DMG på 25 
    {
        if ((p.PlayerGoldSpent / (40 * p.PlayerDamage)) >= i) // för varje 40 guld man spenderar så får man DMG++ 
        {
            p.PlayerDamage ++;
        }
    }
}

static void EnemySpending(Player p, ref Enemy e, ref EnemyCharacter ec) // När och hur fienden uppgraderar sin karaktär 
{
    // logiken för när fienden ska köpa eller inte 
    while (e.EnemyGold >= 110 && (e.EnemyHealth - p.PlayerDamage) > 0 || e.EnemyGold >= 10 && (e.EnemyHealth - p.PlayerDamage) <= 0)
    {
        int random = Random.Shared.Next(1, 5);

        switch (random) // slumpar vad fienden uppgraderar (åter använde GPT lösningen då jag tyckte om den)
        {
            case 1: ec.CharacterMaxHealth += 10; break;
            case 2: ec.CharacterDamage += 5; break;
            case 3: ec.CharacterArmor += 10; break;
            case 4: ec.CharacterEvadeChance += 2; break;
        }

        e.EnemyGold -= 10;
        e.EnemyGoldSpent += 10;
        EnemyDamageUppgrade(ref e); // uppgraderar fiendens DMG beroende på om alla krav uppfylls
    }
}

static void EnemyDamageUppgrade(ref Enemy e) // gör så att fienden gör mer DMG efter en vis mängd guld spenderas
{
    for (int i = 1; i <= 30; i++) // upprepas 30 gånger så fienden kan ha en max DMG på 30  
    {
        if ((e.EnemyGoldSpent / (40 * e.EnemyDamage)) >= i) // för varje 40 guld fienden spenderar så får man DMG++ 
        {
            e.EnemyDamage ++;
        }
    }
}

static void Interest(ref Player p, ref Enemy e) // kollar om båda ska få ränta för pengarna de fåller
{
    for (int i = 1; i <= 10; i++) // upprepas tills man har fåt 10 guld eller det man ska få
    {
        if (p.PlayerGold / 10 >= i) // kollar om spelaren förtjänar ränta
        {
            p.PlayerGold += 1;
        }

        if (e.EnemyGold / 10 >= i) // kollar om fienden förtjänar ränta
        {
            e.EnemyGold += 1;
        }
    }
}

static int TryParseChois(int min, int max) // används för att ta input från spelaren
{
    int choisToNumber = 0;
    while (choisToNumber < min || choisToNumber > max) // så lännge spelaren inte skriver ett giltigt nummer 
    {
        string chois = Console.ReadLine();
        int.TryParse(chois, out choisToNumber);
        if (choisToNumber < min || choisToNumber > max) // om spelaren kav ett ogiltigt nummer 
        {
            Console.WriteLine($"skriv ett tal från {min} till {max}"); // skriver ut vilka nummer man kan skriva 
        }
    }

    return choisToNumber; // ger tillbaka vad spelaren valde 
}

static void Space(int length) // används för att göra mellanrum till annan text 
{
    for (int i = 0; i < length; i++) // görs så länge jag satte det till
    {
        Console.WriteLine("");
    }
}

static void Tutorial(int tutorialSteps, ref Player p) // den här metoden är för att ge spelaren förståelse för hur spelet funkar
{
    // jag valde en array för att jag vet att jag inte kommer behöva mera text än det här. 
    string[] tutorial = [
        "hej, jag är här för att lära dig hur spelet fungerar. Tillat börja med så kommer du att behöva trycka [ENTER] för att   komma ur den här konversationen.",
        "Bra, under stridens gång kommer du att behöva trycka [ENTER] en massa gånger för att fortsätta den. Efter du är klar med striden kommer jag och förklarar vad som kommer efter den.",
        "Nu när striden är slut så har du en möjlighet att uppgradera din gubbe. Eller så kan du spara dina pengar för att få mer pengar. För varje 10 guld du håller får du 1 extra, du kan som max få 10 guld (eller i andra ord om du håller 100 guld)",
        "Nu när du har spenderat ditt första guld kan jag förklara för dig att för varje 40 guld du spenderar kommer du att göra mer skada på din fiende om du vinner striden. Detsamma gäller för din fiende, så håll ett öga på deras attack så att du inte sparar pengar som kan ha rädda dig.",
        "Nu så har du klarat din första match och jag har lärt dig allt jag kan. Nu är det upp till dig att hitta på olicka sätt att vinna över fienden hur du än vill. Lycka till"
    ];

    if (p.hasSeenTutorial < tutorialSteps) // om spelaren har set en torial så kommer den inte vissas igen
    {
        for (int i = 0; i < 5; i++) // kollar vilken tutorial som ska skrivas
        {
            if (tutorialSteps == i)
            {
                Console.WriteLine(tutorial[tutorialSteps]);
            }
        }
        Console.ReadLine(); // gör så att spelaren bestämmer när de ska gå vidare 
        p.hasSeenTutorial++; 
    }
}