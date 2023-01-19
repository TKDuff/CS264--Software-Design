using System;
using System.IO;
using static System.Console;
using System.Collections.Generic;

/*
Operating System - Ubuntu 20.04LTS
IDE - VScode 1.71.2
*/
namespace assignment3
{
    class Program
    {
        
        static void Main(string[] args)
        {

            
            EmojiFeatureOriginator o = new EmojiFeatureOriginator();  //create EmojiFeature originator which will create EmojiFeature objects and therefore EmojiFeature mementos
            EmojiFeatureCaretaker c = new EmojiFeatureCaretaker();    //create EmojiFeature caretaker, used to traverse canvas via undo and redo    
            EmojiFeatureFactory factory = new EmojiFeatureFactory();

            String input = null;
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
                    case "add":           //add emoji feature
                        addShape(commands[1]);
                        break;
                        
                    case "remove":        //remove emoji feature
                        o.setRemove(commands[1]);
                        break;
                    case "move":           //Add shape
                        o.setMove(commands[1], commands[2], Convert.ToInt32(commands[3]));
                        break;
                    case "draw":           //Display canvas
                        WriteLine( c.DisplayCanvas());
                        break;
                    case "save":           //Save (export) to html file
                        SavetoFile(commands[1] + ".svg");
                        break;
                    case "undo":           //Undo
                        o.restoreFromMemento(c.Undo());       //Undo
                        break;
                    case "redo":           //Redo
                        o.restoreFromMemento(c.Redo());       //Redo
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
                    sw.WriteLine(c.DisplayCanvas());    //writes svgCode to line, using display method which returns string on svg code
                }
                Console.WriteLine($"Emoticon saved to file: {filename}.svg");
            }


            //Shapes are constructed using random paratmeters
            void addShape(string input) 
            {
                EmojiFeatureFactory factory = new EmojiFeatureFactory();
                o.setAdd(input, factory.generateEmojiFeature(input));
                c.addEmojiFeature(o.storeEmojiFeatureInMemento());
            }
        }
    }

    
}
