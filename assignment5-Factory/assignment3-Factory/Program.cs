using System;
using System.IO;
using static System.Console;

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
            ShapeOriginator o = new ShapeOriginator();  //create shape originator which will create shape objects and therefore shape mementos
            ShapeCaretaker c = new ShapeCaretaker();    //create shape caretaker, used to traverse canvas via undo and redo
            Random rnd = new Random();
            //StreamWriter File = new StreamWriter("SVGFile.html");  //Object to write to file
            String input = null;
            
            //User commands
            WriteLine("H - Help\nA - Add Shape\nD - Display Canvas\nS - Save canvas to file\nU - Undo\nR - Redo\nQ - Quit");

            while(input != "Q") //on input Q exit program
            {
                Write("\nEnter a command: ");
                input = ReadLine();     //take user input
                switch(input)
                {
                    case "A":           //Add shape
                        addShape(o, c);
                        break;
                    case "D":           //Display canvas
                    WriteLine("-----------Displaying Canvas-----------");
                        WriteLine(c.DisplayCanvas());
                        break;
                    case "S":           //Save (export) to html file
                        SavetoFile();
                        break;
                    case "U":           //Undo
                        o.restoreFromMemento(c.Undo());
                        break;
                    case "R":           //Redo
                        o.restoreFromMemento(c.Redo());
                        break;
                    case "Q":           //Quit
                        WriteLine("Goodbye!");
                        break;
                    case "H":           //Print commands
                        WriteLine("\nH - Help\nA - Add Shape\nD - Display Canvas\nS - Save canvas to file\nU - Undo\nR - Redo\nQ - Quit");
                        break;
                    default:
                        WriteLine("Wrong command entered");
                        break;
                }
            }
             
            //method to write to file, "SVGfile.html" is the name of the file
            void SavetoFile() {
                using(StreamWriter sw = new StreamWriter(@"SVGFile.html", false))   //second parameter false, indidcate I don't want to append the contents to the file, I want to replace them
                {
                    sw.WriteLine(c.DisplayCanvas());    //writes svgCode to line, using display method which returns string on svg code
                }
                Console.WriteLine("Check \"SVGFile.html\" file in assignment folder to see svg webpage");
            }
            
            //Shapes are constructed using random paratmeters
            void addShape(ShapeOriginator o, ShapeCaretaker c) 
            {
                ShapeFactory factory = new ShapeFactory();
                Console.Write("R - Rectangle\nC - Circle\nE - Ellipse\nL - Line\nPG - Polygon\nPL - Polyline\nPA - Path\n");
                Console.Write("\nEnter a shape:  ");
                input = ReadLine();      //Take user input
                switch(input)
                {
                    case "R":
                    o.set(factory.generateShape("rectangle"));
                    c.addShape(o.storeShapeInMemento());
                    break;
                    case "C":
                    o.set(factory.generateShape("circle"));
                    c.addShape(o.storeShapeInMemento());
                    break;
                    case "E":
                    o.set(factory.generateShape("ellipse"));
                    c.addShape(o.storeShapeInMemento());
                    break;
                    case "L":
                    o.set(factory.generateShape("line"));
                    c.addShape(o.storeShapeInMemento());
                    break;
                    case "PG":
                    o.set(factory.generateShape("polygon"));     //picks random points
                    c.addShape(o.storeShapeInMemento());
                    break;
                    case "PL":
                    o.set(factory.generateShape("polyline"));
                    c.addShape(o.storeShapeInMemento());
                    break;
                    case "PA":
                    o.set(factory.generateShape("path"));
                    c.addShape(o.storeShapeInMemento());
                    break;
                    default: 
                    Console.WriteLine("You entered the wrong command");
                    break;
                    }
            }

        }
    }
}
