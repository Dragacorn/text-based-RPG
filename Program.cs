using System;
//https://patorjk.com/software/taag/#p=display&f=Doom&t=OVERKILL
namespace text_based_RPG
{
    class Program
    {
        
        static void Main(string[] args)
        {
            setup();
        }

        static void setup() {
            Random rand = new Random();

            room[,] level = {
                {new room(room.d, enemy.randGroup(3)), new room(room.d, new enemy[] {new enemy(5, 50, 20, 7, 20, 7, 10)}), new room(room.d, enemy.randGroup(4))},
                {new room(room.v, enemy.randGroup(2)), new room(room.ur, enemy.randGroup(1)), new room(room.tr, enemy.randGroup(1))},
                {new room(room.ur, enemy.randGroup(0)), new room(room.h, enemy.randGroup(2)), new room(room.ul, enemy.randGroup(1))}
            };

            equipment h = new equipment(0, type.ATK, 3, 2, 0, 0, 0);
            equipment r = new equipment(0, type.ATK, 5, 0, 0, 0, 0);
            equipment l = new equipment(0, type.ATK, 3, 3, 0, 1, 0);
            equipment c = new equipment(0, type.ATK, 2, 3, 0, 1, 0);
            equipment b = new equipment(0, type.ATK, 2, 0, 1, 1, 2);
            equipment a = new equipment(2, type.ATK, 5, 5, 5, 5, 1);

            character player = new character(type.ATK, 0, 0, h, r, l, c, b, a);

            gameLoop(level, player);
        }
        static void gameLoop(room[,] floor, character player) {
            room curRoom = floor[player.getPos()[0],player.getPos()[1]];
            curRoom.loadRoom(player);
            player.doTurn(floor);
            enemy.doTurn(curRoom, player);


            if (player.getStats()[0] <= 0) {
                gameEnd();
            } else {
                Console.WriteLine("\n\nPress Enter to start next turn.");
                Console.ReadKey(true);
                Console.Clear();
                gameLoop(floor, player);
            }
        }
        static void gameEnd()
        {
            Console.WriteLine("\n\nPress Enter to Exit Terminal or type \"r\" to restart.");
            string exit = Console.ReadLine();
            switch (exit.ToUpper()) {
                case "R":
                    Console.Clear();
                    setup();
                    break;
                case "": 
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    gameEnd();
                    break;
            }
        }
    }
}