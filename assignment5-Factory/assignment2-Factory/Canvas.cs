using static System.Console;
using System.Collections.Generic;
using System.IO;
using System;

class Canvas
{
    int height, width;
    public List<Shape> Shapes = new List<Shape>();  //list that stores each individual shape
    StreamWriter File;  //Object to write to file
    string code;        //String that stores actual svg code to be written to html file
    
    public Canvas(int height, int width){
        this.height = height;
        this.width = width;
        File = new StreamWriter("SVGFile.html");    //Create new html file, this is where the svg code will be written to
    }

    //Simple method, adds shape to list
    public void Add(Shape s) {
        Shapes.Add(s);
    }

    //Method to delete a shape
    public void Delete(int index) {
        if(index >= Shapes.Count) { //If tyring to remove using index out of stack range (z-index range)
            WriteLine("Cannot delete at z-index " + index + ", must insert shape using number between z-index values");
        } else {
            Shapes.RemoveAt(index); //Remove shape from list
            for(int i = index; i < Shapes.Count; i++){  //decrement each z-index value for each shape who was ontop of the deleted shape in the stack
                Shapes[i].zIndex--;
            }
            Shape.ListzIndex = Shape.ListzIndex >= 0 ? Shape.ListzIndex-- : Shape.ListzIndex = 0; //special case if list contained only one shape, stops z-index being -1
        }
    }

   //Method to export file to svg
    public void ExportFileToSVG() {
        /*Singleton object created here */
        Singleton singleObject = Singleton.GetInstance();

        for(int i = 0; i < Shapes.Count; i++){
            /*##########################################################################################################################################################*/
            /*Singleton design pattern is used here, the display output method utilises the singleton style object (named singleObject) by calling the addStyle() method*/
            code += "\t\t\t" + Shapes[i].svgLine() + singleObject.addStyle() + "\n";  //Going through list form bottom to top, add each shapes svg line (.svgLine()) to the string in order
        }

        File.Write("<html>\n\t<body>\n\t\t<svg width=\"" + width + "\" height=\"" + height + "\">\n"); //boilerplate html
        File.Write(code);   //this is what adds the svg code to the html file
        File.Write("\n\t\t</svg>\n\t</body>\n</html>"); //boilerplate html
        File.Close();   //stop writing to file
        WriteLine("Export complete, open the file \"SVGFile.html\" to see shapes on web-page");
    }



    //Method which displays canvas information
    public void CanvasInfo() {

        WriteLine("\nCurrent Canvas View");
        for(int i = Shapes.Count-1; i >= 0; i--){   //for each shape object in list
            WriteLine(Shapes[i].info());           //write the shapes infromation (done by calling the method .info())
        }
        WriteLine("The globla z-index is " + (Shape.ListzIndex-1) + "\n\n"); //Global z-index is the total number of shapes on the stack
    }

    //Method to change shapes z-index, it is swapping to shapes
    //Takes in first z-index, then second z-index, then uses classic swap method to swap to shapes on stack 
    public void ChangeZIndex() {
        Write("Enter the z-index of the shape who's z-index you want to change: ");
        int A = Convert.ToInt32(ReadLine());

        Write("Enter the z-index of the shape you want to swap it with: ");
        int B = Convert.ToInt32(ReadLine());

        bool AValid = A < Shapes.Count && A >= 0;   //check user input is valid
        bool BValid = B < Shapes.Count && B >= 0;   //check user input is valid
        if(AValid && BValid){
        Shapes[A].zIndex = B;   //swap shapes z-index
        Shapes[B].zIndex = A;
        Shape temp = Shapes[A]; //swap shapes position in list
        Shapes[A] = Shapes[B];
        Shapes[B] = temp;
        } else if(!AValid){
            WriteLine("Cannot swap shape at" + A + " since z-index not valid");
        } else {
            WriteLine(B + " is not a valid z-index in the stack to swap a shape with");
        }

    }

    //Method to change an individual shapes style
    /*
    public void styling()
    {       Singleton singleObject = Singleton.GetInstance();
            Write("Commands:\nstroke - to change stroke\nfill - to change shape fill\nstrokeW - to change stroke width\n\nEnter which styling you want to change: ");
            string input = ReadLine();  //user selects which style they would like to change
            switch (input)
            {
                //In each case, selected style is changed by changing shapes style attribute
                //Access the shape using Shapes list
                case "stroke":
                Write("Enter the new stroke colour: ");
                input = ReadLine();
                singleObject.setStroke(input);
                break;
                case "fill":
                Write("Enter the new fill colour: ");
                input = ReadLine();
                singleObject.setFill(input);
                break;
                case "strokeW":
                Write("Enter the new stroke width (number): ");
                input = ReadLine();
                singleObject.setStrokeWidth(input);
                break;
                default:
                WriteLine(input + " is not valid");
                break;
            }
            
        }*/
    }