using System;
using System.IO;
using static System.Console;
/*
Operating System - Ubuntu 20.04LTS
IDE - VScode 1.71.2
*/
namespace Assignment4
{

    class Program
    {
        
        static void Main()
        {
            Random rnd = new Random();
            Canvas c = new Canvas();        //Create canvas which stores the shapes, this is the receiver in terms of the command design pattern
            Invoker i = new Invoker();      //Create class which will store and issue commands

            String input = null;    //User input
            
            WriteLine("H - Help\nA - Add Shape\nD - Display Canvas\nS - Save canvas to file\nU - Undo\nR - Redo\nQ - Quit");
            
            while(input != "Q") //on input Q exit program
            {
                Write("\nEnter a command: ");
                input = ReadLine();     //take user input
                switch(input)
                {
                    case "A":           //Add shape
                        addShape();
                        break;
                    case "D":           //Display canvas
                        WriteLine( c.display());
                        break;
                    case "S":           //Save (export) to html file
                        SavetoFile();
                        break;
                    case "U":           //Undo
                        i.Undo();       //Invoker calls undo command
                        break;
                    case "R":           //Redo
                        i.Redo();       //Invoker calls redo command
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

            //Shapes are constructed using random paratmeters
            void addShape() 
            {
                ShapeFactory factory = new ShapeFactory();
                Console.Write("R - Rectangle\nC - Circle\nE - Ellipse\nL - Line\nPG - Polygon\nPL - Polyline\nPA - Path\n");
                Console.Write("\nEnter a shape:  ");
                input = ReadLine();      //Take user input
                switch(input)
                {
                    //When user types shape, they run "AddShapeCommand" which is passed to the invoker to be ran and stored (for undoing and redoing).
                    //Canvas "c" passed to the invoker aswell, the canvas is the receiver which contains extra logic (storing the shape on canvas)
                    case "R":                               //Factory should just replcase this "new rectangle" part, should ask factory to create new rectanlge and will do all the work, not needing to pass 4 random shapes. Should be i.runCommand(new AddShapeCommand(IFactory rectangle = factory.CreateShape("Rectangle"));
                    i.runCommand(new AddShapeCommand(factory.generateShape("rectangle"), c));    //picks 4 random points
                    break;
                    case "C":
                    i.runCommand(new AddShapeCommand(factory.generateShape("circle"), c));
                    break;
                    case "E":
                    i.runCommand(new AddShapeCommand(factory.generateShape("ellipse"), c));
                    break;
                    case "L":
                    i.runCommand(new AddShapeCommand(factory.generateShape("line"), c));
                    break;
                    case "PG":
                    i.runCommand(new AddShapeCommand(factory.generateShape("polygon"), c));    //call pickPoints commmand to pick random pounts
                    break;
                    case "PL":
                    i.runCommand(new AddShapeCommand(factory.generateShape("polyline"), c));
                    break;
                    case "PA":
                    i.runCommand(new AddShapeCommand(factory.generateShape("path"), c));
                    break;
                    default: 
                    Console.WriteLine("You entered the wrong command");
                    break;
                    }
            }


            //method to write to file, "SVGfile.html" is the name of the file
            void SavetoFile() {
                using(StreamWriter sw = new StreamWriter(@"SVGFile.html", false))   //second parameter false, indidcate I don't want to append the contents to the file, I want to replace them
                {
                    sw.WriteLine(c.display());    //writes svgCode to line, using display method which returns string on svg code
                }
                Console.WriteLine("Check \"SVGFile.html\" file in assignment folder to see svg webpage");
            }

        } 

    }
}

