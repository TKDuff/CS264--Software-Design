using System;
using System.IO;
using static System.Console;

namespace CommandBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Receiver r = new Receiver();        //Create canvas which stores the shapes, this is the receiver in terms of the command design pattern
            Invoker i = new Invoker();      //Create class which will store and issue commands
            String input = null;    //User input
            string firstCommand;
            
            string addL= "add{ left-eye |right-eye | left-brow | right-brow | mouth }\n";
            string removeL = "remove { left-eye | right-eye | left-brow | right-brow | mouth }\n";
            string moveL = "move { left-eye | right-eye | left-brow | right-brow | mouth } {up | down | left | right } value\n";
            string styleL = "style { left-eye | right-eye | left-brow | right-brow | mouth } { A | B | C}\n";
            string restL = "save { <file> }\ndraw\nundo\nredo\nhelp\nquit";
            string help = addL + removeL + moveL + styleL +restL;
            WriteLine(help);
            

            while(input != "quit") //on input Q exit program
            {
                Write("\nEnter a command: ");
                input = ReadLine();     //take user input
                string[] commands = input.Split(" "); 
                firstCommand = commands[0];
                
                switch(firstCommand)
                {
                    case "add":           //Add shape
                        i.runCommand(new AddFeatureCommand(commands[1], r));
                        break;
                    case "remove":           //Add shape
                        i.runCommand(new RemoveFeatureCommand(commands[1], r));
                        break;
                    case "move":           //Add shape
                        if(commands[2] == "left" || commands[2] == "right")
                        {
                            i.runCommand(new MoveFeatureHorizontalCommand(commands[1], commands[2], Convert.ToInt32(commands[3]) ,r));
                        } else {
                            i.runCommand(new MoveFeatureVerticalCommand(commands[1], commands[2], Convert.ToInt32(commands[3]) ,r));
                        }
                        break;
                    case "draw":           //Display canvas
                        WriteLine( r.Draw());
                        break;
                    case "save":           //Save (export) to html file
                        SavetoFile(commands[1] + ".svg");
                        break;
                    case "undo":           //Undo
                        i.Undo();       //Invoker calls undo command
                        break;
                    case "redo":           //Redo
                        i.Redo();       //Invoker calls redo command
                        break;
                    case "quit":           //Quit
                        WriteLine("Goodbye!");
                        break;
                    case "help":           //Print commands
                        WriteLine(help);
                        break;
                    default:
                        WriteLine("Wrong command entered");
                        break;
                }
            }


            void SavetoFile(string filename) {
                using(StreamWriter sw = new StreamWriter(filename, false))   //second parameter false, indidcate I don't want to append the contents to the file, I want to replace them
                {
                    sw.WriteLine(r.Draw());    //writes svgCode to line, using display method which returns string on svg code
                }
                Console.WriteLine($"Emoticon saved to file: {filename}.svg");
            }
        }

    }
}
