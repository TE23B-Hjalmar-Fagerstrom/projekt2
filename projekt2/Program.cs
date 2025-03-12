using PlayerAndCharacter;
using EnemyAndCharacter;

Console.WriteLine("semi-auto battler");
Console.WriteLine("");
Console.WriteLine("");
Console.WriteLine("1.new game   2.Quit");

string chois = Console.ReadLine (); 
int.TryParse(chois, out int choisToNumber);

while (choisToNumber != 1 || choisToNumber != 2)
{
    Console.WriteLine("skriv 1 för att starta en match eller 2 för att lämna");
    chois = Console.ReadLine (); 
    int.TryParse(chois, out choisToNumber);
}

static void Game() 
{

}