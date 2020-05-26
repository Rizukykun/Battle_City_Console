using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Battle_City_Console
{
    class Program
    {
        //Variáveis Globais
        static int Yx = 40; //Posição do tanque em x
        static int Yy = 35; //Posição do tanque em y
        static int Pos = 0; //Posição do tanque na string
        static int charside = 1; //Direção e sentido do tanque
        static int PosT = 0; //Posição do tiro na string
        static int ShotX = 0; //Posição do tiro em x
        static int ShotY = 0; //Posição do tiro em y
        static StringBuilder map = new StringBuilder(@"mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm>mmmmmmmmmmttttttttttttttttttttttttttttmmmmttttttttttttttttttttttttttttmmmmmmmmmm>mmmmmmmmmttttttttttttttttttttttttttttttmmttttttttttttttttttttttttttttttmmmmmmmmm>mmmmmmmtttttttttttttttttt...tttttttttttttttttttttttt...ttttttttttttttttttmmmmmmm>mmmmmmtttttttttttttttttt....tttttttttttttttttttttttt....ttttttttttttttttttmmmmmm>mmmmmmttttttttttttttttt.ttt..tttttttttttttttttttttt..ttt.tttttttttttttttttmmmmmm>mmmmmmtttettttttttttttt.tttt..ttttttttteettttttttt..tttt.tttttttttttttetttmmmmmm>mmmmmtttttttttttttttttttttttt..hhhhhhhhhhhhhhhhhh..ttttttttttttttttttttttttmmmmm>mmmmmtttttttttttttttttttttttth....................httttttttttttttttttttttttmmmmm>mmmmtttttttttttttttttttttttthh....................hhttttttttttttttttttttttttmmmm>mmmmttttttttttttttttttttttth........................htttttttttttttttttttttttmmmm>mmmttttttttttttttttttttttth..........................htttttttttttttttttttttttmmm>mmmtttttttttttttttttttttth............................httttttttttttttttttttttmmm>mmttttttttttttttttttttttth............................htttttttttttttttttttttttmm>mmttttttttttttttttttwwwtth......tttt........tttt......httwwwttttttttttttttttttmm>mmmmmmmmm......wwwwwwwwwww.....ttttttt....ttttttt.....wwwwwwwwwww......mmmmmmmmm>mmmmmmmmmm....wwwwwwwwwwww....ttttttttt..ttttttttt....wwwwwwwwwwww....mmmmmmmmmm>mmmmmmmmmmm....wwwwwwwwwww.....tttetttt..ttttettt.....wwwwwwwwwww....mmmmmmmmmmm>mmtttttwwttttttttwwwwwwwww.......tttttt..tttttt.......wwwwwwwwwttttttttwwtttttmm>mmtttetwwtttttttttttwwwtthh......ttttt....ttttt......hhttwwwtttttttttttwwtetttmm>mmmttttwwttttttttttttttttth..........................htttttttttttttttttwwttttmmm>mmmttttwwtttttttttttttttttth........................httttttttttttttttttwwttttmmm>mmmttttwwttttttttttttttttttth......................htttttttttttttttttttwwttttmmm>mmmttttwwtttttttttttttttttttth....................httttttttttttttttttttwwttttmmm>mmmttttwwttttttttttttttttttth......................htttttttttttttttttttwwttttmmm>mmmmtttwwtttttttttttttttttth....h..h........h..h....httttttttttttttttttwwtttmmmm>mmmmtttwwttttttttttttttttth...hhh..h........h..hhh...htttttttttttttttttwwtttmmmm>mmmmtttwwttttttttttttthhhh...httthhh........hhhttth...hhhhtttttttttttttwwtttmmmm>mmmmtttwwttttttttttttth.....htttttth........htttttth.....htttttttttttttwwtttmmmm>mmmmtttwwttttttttttttth....httttttth...hh...httttttth....htttttttttttttwwtttmmmm>mmmmtetwwttttttttttttth....htttttttthhhtthhhtttttttth....htttttttttttttwwtetmmmm>mmmmmttwwttttttttttttthhhhhhtttttttttttttttttttttttthhhhhhtttttttttttttwwttmmmmm>mmmmmttwwttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttwwttmmmmm>mmmmmm...........tttttttttttttttttttttttttttttttttttttttttttttt...........mmmmmm>mmmmmmm..........ttttttttttttttttttttttyttttttttttttttttttttttt..........mmmmmmm>mmmmmmmmttttttttt.tttttttttttttttttttttttttttttttttttttttttttt.ttttttttmmmmmmmmm>mmmmmmmmmmtttttttt.ttttttttttttttttttt....ttttttttttttttttttt.tttttttmmmmmmmmmmm>mmmmmmmmmmmmttttett.tttttttttttttttttt.pp.tttttttttttttttttt.tttetttmmmmmmmmmmmm>mmmmmmmmmmmmmmttttt.tttttttttttttttttt.pp.tttttttttttttttttt.tttttmmmmmmmmmmmmmm>mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm");
        [STAThread] //Necessário para ler o teclado?
        static void Main(string[] args)
        {
            //Variáveis necessárias
            Console.SetWindowSize(80, 40);
            Console.CursorVisible = false;
            //Render inicial
            GeraMapa(map.ToString());
            //Jogo
            while (true)
            {
                int prevYx = Yx;
                int prevYy = Yy;
                Pos = (81 * (prevYy - 1)) + prevYx - 1;
                if (MoveY())
                {
                    map[Pos] = 't';
                    Console.SetCursorPosition(prevYx - 1, prevYy - 1);
                    GeraMapa(map[Pos].ToString());
                    Pos = (81 * (Yy - 1)) + Yx - 1;
                    if (charside == 1)
                    {
                        map[Pos] = '╩';
                    }
                    else if (charside == 2)
                    {
                        map[Pos] = '╠';
                    }
                    else if (charside == 3)
                    {
                        map[Pos] = '╦';
                    }
                    else
                    {
                        map[Pos] = '╣';
                    }
                    Console.SetCursorPosition(Yx - 1, Yy - 1);
                    GeraMapa(map[Pos].ToString());
                }
                Shoot();
                ShootFly();
                Thread.Sleep(60);
                ShootFly();
                Console.SetWindowSize(80, 40);
                Console.CursorVisible = false;
                //Console.SetCursorPosition(0, 0);
                //GeraMapa(map.ToString());
            }
        }
        /// <summary>
        /// Método renderizador do mapa do jogo
        /// </summary>
        /// <param name="map"></param>
        static void GeraMapa(string map)
        {
            foreach (char c in map)
            {
                if (c == 'm') //Bordas intransponíveis
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write('█');
                }
                else if (c == 't') //Terreno Trafegável
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write('█');
                }
                else if (c == '.') //Paredes Destrutíveis
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write('▒');
                }
                else if (c == 'e') //Inimigos
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.Write('█');
                }
                else if (c == 'w') //Água 
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write('█');
                }
                else if (c == 'h') //Tijolos Resistentes
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write('▓');
                }
                else if (c == 'p') //Emblema a ser protegido
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.Write('█');
                }
                else if (c == 'y' || c == '╩' || c == '╠' || c == '╦' || c == '╣') //Personagem
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.Black;
                    if (charside == 1)
                        Console.Write('╩');
                    else if (charside == 2)
                        Console.Write('╠');
                    else if (charside == 3)
                        Console.Write('╦');
                    else
                        Console.Write('╣');
                }
                else if (c == '▲')
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write('▲');
                }
                else if (c == '►')
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write('►');
                }
                else if (c == '▼')
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write('▼');
                }
                else if (c == '◄')
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write('◄');
                }
                else if (c == '>') //Pula Linha
                {
                    Console.Write("\n");
                }
            }
        }
        static bool MoveY()
        {
            if (Keyboard.IsKeyDown(Key.W) && map[Pos - 81] == 't')
            {
                Yy--;
                charside = 1;
                return true;
            }
            else if (Keyboard.IsKeyDown(Key.S) && map[Pos + 81] == 't')
            {
                Yy++;
                charside = 3;
                return true;
            }
            else if (Keyboard.IsKeyDown(Key.A) && map[Pos - 1] == 't')
            {
                Yx--;
                charside = 4;
                return true;
            }
            else if (Keyboard.IsKeyDown(Key.D) && map[Pos + 1] == 't')
            {
                Yx++;
                charside = 2;
                return true;
            }
            return false;
        }
        static void Shoot()
        {
            if (Keyboard.IsKeyDown(Key.K))
            {
                if (map[Pos] == '╩')
                {
                    PosT = Pos - 81;
                    if (map[PosT] == 't')
                    {
                        Console.SetCursorPosition(Yx - 1, Yy - 2);
                        map[PosT] = '▲';
                        ShotX = Yx - 1;
                        ShotY = Yy - 2;
                        GeraMapa(map[PosT].ToString());
                    }
                    else if (map[PosT] == '.')
                    {
                        Console.SetCursorPosition(Yx - 1, Yy - 2);
                        map[PosT] = 't';
                        GeraMapa(map[PosT].ToString());
                    }
                    else if (map[PosT] == 'h')
                    {
                        Console.SetCursorPosition(Yx - 1, Yy - 2);
                        map[PosT] = '.';
                        GeraMapa(map[PosT].ToString());
                    }
                }
                else if (map[Pos] == '╠')
                {
                    PosT = Pos + 1;
                    if (map[PosT] == 't')
                    {
                        Console.SetCursorPosition(Yx, Yy - 1);
                        map[PosT] = '►';
                        ShotX = Yx;
                        ShotY = Yy - 1;
                        GeraMapa(map[PosT].ToString());
                    }
                    else if (map[PosT] == '.')
                    {
                        Console.SetCursorPosition(Yx, Yy - 1);
                        map[PosT] = 't';
                        GeraMapa(map[PosT].ToString());
                    }
                    else if (map[PosT] == 'h')
                    {
                        Console.SetCursorPosition(Yx, Yy - 1);
                        map[PosT] = '.';
                        GeraMapa(map[PosT].ToString());
                    }
                }
                else if (map[Pos] == '╦')
                {
                    PosT = Pos + 81;
                    if (map[PosT] == 't')
                    {
                        Console.SetCursorPosition(Yx - 1, Yy);
                        map[PosT] = '▼';
                        ShotX = Yx - 1;
                        ShotY = Yy;
                        GeraMapa(map[PosT].ToString());
                    }
                    else if (map[PosT] == '.')
                    {
                        Console.SetCursorPosition(Yx - 1, Yy);
                        map[PosT] = 't';
                        GeraMapa(map[PosT].ToString());
                    }
                    else if (map[PosT] == 'h')
                    {
                        Console.SetCursorPosition(Yx - 1, Yy);
                        map[PosT] = '.';
                        GeraMapa(map[PosT].ToString());
                    }
                }
                else
                {
                    PosT = Pos - 1;
                    if (map[PosT] == 't')
                    {
                        Console.SetCursorPosition(Yx - 2, Yy - 1);
                        map[PosT] = '◄';
                        ShotX = Yx - 2;
                        ShotY = Yy - 1;
                        GeraMapa(map[PosT].ToString());
                    }
                    else if (map[PosT] == '.')
                    {
                        Console.SetCursorPosition(Yx - 2, Yy - 1);
                        map[PosT] = 't';
                        GeraMapa(map[PosT].ToString());
                    }
                    else if (map[PosT] == 'h')
                    {
                        Console.SetCursorPosition(Yx - 2, Yy - 1);
                        map[PosT] = '.';
                        GeraMapa(map[PosT].ToString());
                    }
                }
            }
        }
        static void ShootFly()
        {
            if (map[PosT] == '▲')
            {
                if (map[PosT - 81] == 't')
                {
                    Console.SetCursorPosition(ShotX, ShotY - 1);
                    map[PosT - 81] = '▲';
                    GeraMapa(map[PosT - 81].ToString());
                    Console.SetCursorPosition(ShotX, ShotY);
                    map[PosT] = 't';
                    GeraMapa(map[PosT].ToString());
                    PosT = PosT - 81;
                    ShotY = ShotY - 1;
                }
                else if (map[PosT] == '▲' && map[PosT - 81] != 't')
                {
                    Console.SetCursorPosition(ShotX, ShotY);
                    map[PosT] = 't';
                    GeraMapa(map[PosT].ToString());
                    if (map[PosT - 81] == '.')
                    {
                        map[PosT - 81] = 't';
                        Console.SetCursorPosition(ShotX, ShotY - 1);
                        GeraMapa(map[PosT - 81].ToString());
                    }
                    else if (map[PosT - 81] == 'h')
                    {
                        map[PosT - 81] = '.';
                        Console.SetCursorPosition(ShotX, ShotY - 1);
                        GeraMapa(map[PosT - 81].ToString());
                    }
                }
            }
            else if (map[PosT] == '►')
            {
                if (map[PosT + 1] == 't')
                {
                    Console.SetCursorPosition(ShotX + 1, ShotY);
                    map[PosT + 1] = '►';
                    GeraMapa(map[PosT + 1].ToString());
                    Console.SetCursorPosition(ShotX, ShotY);
                    map[PosT] = 't';
                    GeraMapa(map[PosT].ToString());
                    PosT = PosT + 1;
                    ShotX = ShotX + 1;
                }
                else if (map[PosT + 1] != 't')
                {
                    Console.SetCursorPosition(ShotX, ShotY);
                    map[PosT] = 't';
                    GeraMapa(map[PosT].ToString());
                    if (map[PosT + 1] == '.')
                    {
                        map[PosT + 1] = 't';
                        Console.SetCursorPosition(ShotX + 1, ShotY);
                        GeraMapa(map[PosT + 1].ToString());
                    }
                    else if (map[PosT + 1] == 'h')
                    {
                        map[PosT + 1] = '.';
                        Console.SetCursorPosition(ShotX + 1, ShotY);
                        GeraMapa(map[PosT + 1].ToString());
                    }
                }
            }
            else if (map[PosT] == '▼')
            {
                if (map[PosT + 81] == 't')
                {
                    Console.SetCursorPosition(ShotX, ShotY + 1);
                    map[PosT + 81] = '▼';
                    GeraMapa(map[PosT + 81].ToString());
                    Console.SetCursorPosition(ShotX, ShotY);
                    map[PosT] = 't';
                    GeraMapa(map[PosT].ToString());
                    PosT = PosT + 81;
                    ShotY = ShotY + 1;
                }
                else if (map[PosT + 81] != 't')
                {
                    Console.SetCursorPosition(ShotX, ShotY);
                    map[PosT] = 't';
                    GeraMapa(map[PosT].ToString());
                    if (map[PosT + 81] == '.')
                    {
                        map[PosT + 81] = 't';
                        Console.SetCursorPosition(ShotX, ShotY + 1);
                        GeraMapa(map[PosT + 81].ToString());
                    }
                    else if (map[PosT + 81] == 'h')
                    {
                        map[PosT + 81] = '.';
                        Console.SetCursorPosition(ShotX, ShotY + 1);
                        GeraMapa(map[PosT + 81].ToString());
                    }
                }
            }
            else if (map[PosT] == '◄')
            {
                if (map[PosT - 1] == 't')
                {
                    Console.SetCursorPosition(ShotX - 1, ShotY);
                    map[PosT - 1] = '◄';
                    GeraMapa(map[PosT - 1].ToString());
                    Console.SetCursorPosition(ShotX, ShotY);
                    map[PosT] = 't';
                    GeraMapa(map[PosT].ToString());
                    PosT = PosT - 1;
                    ShotX = ShotX - 1;
                }
                else if (map[PosT - 1] != 't')
                {
                    Console.SetCursorPosition(ShotX, ShotY);
                    map[PosT] = 't';
                    GeraMapa(map[PosT].ToString());
                    if (map[PosT - 1] == '.')
                    {
                        map[PosT - 1] = 't';
                        Console.SetCursorPosition(ShotX - 1, ShotY);
                        GeraMapa(map[PosT - 1].ToString());
                    }
                    else if (map[PosT - 1] == 'h')
                    {
                        map[PosT - 1] = '.';
                        Console.SetCursorPosition(ShotX - 1, ShotY);
                        GeraMapa(map[PosT - 1].ToString());
                    }
                }
            }
        }
    }
}
