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

string chois = Console.ReadLine();
int.TryParse(chois, out int choisToNumber);

while (choisToNumber != 1 || choisToNumber != 2)
{
    Console.WriteLine("skriv 1 för att starta en match eller 2 för att lämna");
    chois = Console.ReadLine();
    int.TryParse(chois, out choisToNumber);
}

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

static void shop(Player p, Enemy e)
{
    if (p.playerHealth > 0 && e.EnemyHealth > 0)
    {

    }
}

static void characterEvade(PlayerCharacter pc, EnemyCharacter ec)
{
    if (pc.characterEvadeProbability < pc.characterEvadeChance)
    {
        Console.WriteLine("du undvek fiendens attack");
        pc.characterHealth += ec.characterDamage;
    }

    if (ec.characterEvadeProbability < ec.characterEvadeChance)
    {
        Console.WriteLine("fienden undvek din attack");
        ec.characterHealth += pc.characterDamage;
    }
}