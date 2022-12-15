using static System.Console;
using System;
using System.Collections.Generic;
/*
Operating System - Ubuntu 20.04LTS
IDE - VScode 1.71.2

In my program, three classes
1)Program class, contains code to get user input to select a command
2)Canvas class, contains list data structure to store shapes on stack and methods to Add, Delete, Export, Change shape styling and z-index
3)Abstract Shape super class class to store shape information and methods, each individual shape inherits from this class its associated methods to update shape and return svg info

The z-index in the program starts at index 0, so the first shape has an index of 0, each shape has an individual z-index
*/

namespace ShapeZIndex
{
    class Program
    {
        

        static void Main(string[] args)
        {
            WriteLine("NOTICE - the z-index in the program starts at index 0, so the first shape has an index of 0\nEach shape has an individual z-index\n");
            Write("Enter the canvas height: ");
            int height = Convert.ToInt32(ReadLine());

            Write("Enter the canvas width: ");
            int width = Convert.ToInt32(ReadLine());
            Canvas c = new Canvas(height, width);

            /*Keep asking user input until type E to exit program */
            while(true) {
            Write("Set of commands: \n\tA - Add a new shape\n\tD - Delete a shape\n\tU - Update a shape\n\tV - view current canvas\n\tE - Export SVG to HTML\n\tZ - change shapes z-index (swap with another shape)\nEnter a command: ");
            string input = ReadLine();
            switch (input)
            {
                case "A":
                    createShape(c);
                    break;
                case "D":
                    deleteShape(c);
                    break;
                case "U":
                    updateShape(c);
                    break;
                case "V":
                    c.CanvasInfo();
                    break;
                case "E":
                    c.ExportFileToSVG();
                    System.Environment.Exit(0); //Exit the program
                    break;
                case "Z":
                    c.ChangeZIndex();
                    break;
                default:
                    WriteLine("\nWRONG INPUT\n");
                    break;
            }       
        }
        }
        /*Switch case, each case creates shape object */
        public static void createShape(Canvas c) {
            Factory shapeFactory = new Factory();   //Create Shape factory object
            Write("Commands:\nR - Rectangle\nC - Circle\nE - Elipse\nL - Line\nPG - Polygone\nPL - Polyline\nEnter the shape you want to create: ");
            string input = ReadLine();
            switch (input)
            {
                case "R":
                c.Shapes.Add(shapeFactory.generateShape("rectangle"));  //use shape factory method to create shape, it returns the shape specified
                break;
                case "C":
                c.Shapes.Add(shapeFactory.generateShape("circle"));
                break;
                case "E":
                c.Shapes.Add(shapeFactory.generateShape("ellipse"));
                break;
                case "L":
                c.Shapes.Add(shapeFactory.generateShape("line"));
                break;
                case "PG":
                c.Shapes.Add(shapeFactory.generateShape("polygon"));
                break;
                case "PL":
                c.Shapes.Add(shapeFactory.generateShape("polyline"));
                break;
            }
        }

        /*This method just gets the z-index of the shape to be deleted */
        public static void deleteShape(Canvas c) {
            Write("Enter the shape to be deleted z-index: ");
            int input = Convert.ToInt32(ReadLine());
            c.Delete(input);

        }
        /*This method just gets the z-index of the shape to be updated and calls method from within shape object */
        public static void updateShape(Canvas c)
        {
            Write("Enter the z-index of the shape you would like to update: ");
            int input = Convert.ToInt32(ReadLine());
            c.Shapes[input].update();
        } 
        
    }

        
    }

