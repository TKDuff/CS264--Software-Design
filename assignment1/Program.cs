using System.Text.RegularExpressions;
using System.IO;
using System;

namespace cRegex
{
    class Program
    {

        static void Main(string[] args)
        {
            //Create table object
            Table t1 = new Table();
            
            //Take user input form command line
            Console.Write("$ tabconv ");
            String input = Console.ReadLine();
            
            //List of commands split into array, interpreted one by one
            String[] commands = input.Split(" ");
            for(int i = 0; i < commands.Length; i++){
                t1.commandInterp(commands[i], i, commands);
                }

        }
          //album.html
          //albumlist.csv
          //list.md
    }

    class Table
    {

        public String[] headers;    //table headers with getters and setters
        public String[,] body;      //table body with getters and setters
        String inputFile = "";      //input file user types in, must be in same location as program
        String outputFile = "";     //file input file will be converted to

        //default no args constructor
        public Table()
        {
        }
        //Each command in array is interpreted seperatly, some trick is used or -o command
        public void commandInterp(String command,int index, String[] commands) {
            switch(command){
                case "-o":
                    inputFile = commands[index-1];     //input file is always before -o command
                    convertInputType();                //call method to converting the input file to an internal arrays
                    outputFile = commands[index+1];    //input file is always after -o command
                    commands[index+1] = "null";        //set file after -o to null
                    WriteToFile();                     //Write to output file, using parsed information
                    break;
                case "-l":
                    printOutputFormats();               //list output formats
                    break;
                case "-i":
                    Console.WriteLine("This is tabconv version 1.4\n"); //prints version
                    break;
                case "-h":
                    Console.WriteLine(                     //list commandsa
                        "Table Converter commands:" + 
                    "\n-v       Verbose Mode for debugging "+
                    "\n-o	<file>,	—output=<file>	 Output	file	specified	by	<file>" +
                    "\n-l,	—list-formats	 	 	 List	formats" +
                    "\n-h,	—help		 	 	 	 Show	usage	message" +
                    "\n-i,	—info		 	 	 	 Show	version	information\n");
                    break;
                case "null":                   //this is awkward but it stop output file after -o command being interpreted as an invalid command, could have handled better
                    break;
                default:
                    if( (index < commands.Length-1) && (commands[index+1] != "-o")){ //stops input file before -o being interpreted as invalid command
                        Console.WriteLine(command + " is not a valid command"); //if command is invalid let user know
                    } 
                    break;
            }
        }

        public void printOutputFormats() {
            Console.WriteLine("The possible formats are:\n\t\tHyperText Markup Language: .html\n\t\tMarkdown: .md\n\t\tComma Separated Values: .csv\n");
        }

        //Determines the type of the input file and parses table values depending on information
        public void convertInputType() {
            switch(getType(inputFile)){     
                case "csv":
                    headers = getCSVHead();
                    body = getCSVBody();
                    break;
                case "html":
                    headers = getHTMLHead();
                    body = getHTMLBody();
                    break;
                case "md":
                    headers = getMDhead();
                    body = getMDBody();
                    break;
                default:
                    Console.WriteLine(inputFile + " does not have a valid extension");
                    break;            
            }
        }

        //StreamWriter creates new file to be written to using name user inputted to command line.
        //Determines file type and calls write method depending said type.
        public void WriteToFile() {
            StreamWriter File = new StreamWriter(outputFile);
            
            switch(getType(outputFile)){
                case "csv":
                    File.Write( printCSV());
                    File.Close();
                    break;
                case "html":
                    File.Write( printHTML());
                    File.Close();
                    break;
                case "md":
                    File.Write( printMD());
                    File.Close();
                    break;
                default:
                    break;            
            }

        }
        //This method determines file type, based on last character
        public String getType(String file)
        {
            char type = (file[file.Length-1]); 
            return type switch
            {
                'v' => "csv",
                'l' => "html",
                'd' => "md",
                _ => $"{file} has incorrect extension"
            };
        }

        //Get csv file tables header (columns) 
        public String[] getCSVHead()
        {
            try {
            string text = System.IO.File.ReadAllText(@inputFile);
            string firstline = text.Substring(0, text.IndexOf(Environment.NewLine));        //get first line
            return firstline.Split(",");            //split line at commas into array
            } catch(FileNotFoundException){         //if file not found
                Console.WriteLine(inputFile + " not found");    //tell user
                System.Environment.Exit(1);                     //exit program
                return null;
            }
            
        }

        //Get html headers (columns), uses regex
        public String[] getHTMLHead()
        {
            try{
            string text = System.IO.File.ReadAllText(@inputFile);   //extract text from file

            string pattern = @"(?<=<th>)(.*?)(?=</th>)";            //regex pattern matching between <th> and </th>
            MatchCollection matched  =  Regex.Matches(text, pattern);   //match for pattern in html file
            String[] header = new String[matched.Count];            //temporary array to store column headers in array
    

            
            for (int count = 0; count < matched.Count; count++) {
            header[count] = matched[count].Value;           //move columns headers from matched array (of type MatchCollection) to header array (of type string)
            }
            
            return header;   //return header, while I could just set it to the accessor "headers" here I wanted to use a setter explicitly in the "convertInputType" method (line 90)
            } catch (FileNotFoundException){    //try catch for file not found, same as in getCSVHead
                Console.WriteLine(inputFile + " not found");
                System.Environment.Exit(1);
                return null;
            }
        }

        public String[] getMDhead()
        {
            try{
            string text = System.IO.File.ReadAllText(@inputFile);    //store file text as string
            int length = text.IndexOf("|\n")-1;     //get length of string -2 to get rid of |\n
            String header = (text.Substring(1, length)); //substring to be split, |one|two|three ---> one|two|three

            return header.Split('|'); //return split array, which is the header information
            } catch(FileNotFoundException) {  //try catch, same as other two methods above
                Console.WriteLine(inputFile + " not found");
                System.Environment.Exit(1);
                return null;
            }

        }


        public String[,] getCSVBody() {
            var lines = File.ReadAllLines(@inputFile);  //store file in a string
            int rowsLength = (lines.Length-1);          //get number of rows, minus one since header not included
            int colunmLength = headers.Length;          //get number of columns
            String[,] body = new String[rowsLength, colunmLength];  //create 2D array

            for (int rowIndex = 1; rowIndex <= rowsLength; rowIndex++) {    //for everyone line in string
                String line = lines[rowIndex];      //get individual line
                String[] split = line.Split(",");   //split into array
                for(int colunmIndex = 0; colunmIndex < colunmLength; colunmIndex++){    //for every column in table
                    body[rowIndex-1,colunmIndex] = split[colunmIndex];  //fill 2d array cell with "split" array word
                }
            }

            return body;
            
        }

        public String[,] getMDBody() {

            var lines = File.ReadAllLines(@inputFile);  //array of lines for each line in file
            int rowsLength = lines.Length;
            int colunmLength = headers.Length;
            int bodyRowIndex = 0;                       //to store row index position for 2d array, see line 221
            String[,] body = new String[rowsLength-2, colunmLength];    //create body 2d array, -2 since it has 2 less rows since it doesn't include the header

            for(int LineIndex = 2; LineIndex < rowsLength; LineIndex++){//since we are not including header, start counting rows from second line, hence bodyRowIndex above exist start from 0
                String current =  lines[LineIndex];     //current line
                String[] split = current.Substring(1, current.Length-2).Split("|"); //split line, not including first | and last |
                for(int colunmIndex = 0; colunmIndex < colunmLength; colunmIndex++){
                    body[bodyRowIndex, colunmIndex] = split[colunmIndex];   //store current split array values in equivalent position in body array
                }
                bodyRowIndex++;     //increment row index (remember, for loop starts at 2)
            }

            return body;
        }

        public String[,] getHTMLBody() {
                
            string text = System.IO.File.ReadAllText(@inputFile);   //store file text in string
            int colunmLength = headers.Length;
            string pattern = @"(?<=<td.*>)(.*?)(?=</td>)";  //regex pattern to match for between column cells
            MatchCollection matched  =  Regex.Matches(text, pattern);   //array of matched values (is not of type string)
            int rowLength = matched.Count / colunmLength;   //number of rows is number of matchvalues divided by number of columns 

            int matchIndex = 0;
            int rowIndex = 0;
            int colunmIndex = 0;
            String[,] body = new String[rowLength, colunmLength];
            
            
            while(matchIndex < matched.Count){ //for every cell in matched array
            colunmIndex = matchIndex % colunmLength;    //current column is found by modules, so if matchIndex was 9 and columnLength was 3, the index for the body would be 0
            body[rowIndex, colunmIndex] = matched[matchIndex].Value; //store matched cell value in body at equivalent position
                if(colunmIndex == colunmLength-1){  //at final column index move onto next row
                    rowIndex++;
                      
                      }
                      matchIndex++; //move onto next cell in match array
                    
                    }
                    return body;
        }



            
        //Print files, all strings are stored in a variable to be written to file using StreamReader

        public String printCSV() {
            String finalText = "";
            int colunmLength = headers.Length-1;
                
                int count = 0;
                while(count < colunmLength){    //for every cell in header column
                    finalText += ("\"" + headers[count] + "\",");   //string is " + word + " +,
                    count++;    //move onto next cell
                }
                finalText += ("\"" + headers[colunmLength] + "\"\n");   //since final cell in csv has no comma, done outside for loop, move onto next line

                for(int rowIndex = 0; rowIndex < body.GetLength(0); rowIndex++){
                    for(int colunmIndex = 0; colunmIndex < colunmLength; colunmIndex++){
                        finalText += ("\"" + body[rowIndex, colunmIndex] + "\",");  //for every cell, string is " + word + " + ,
                    }
                    finalText += ("\"" + body[rowIndex, colunmLength] + "\"");  //at end of line, exclude final comma
                    finalText += ("\n");//move onto next line at end of line
                }
            return finalText;
                    
        }
       
        public String printMD() {
            /*It is not efficient, but for md files, in order to ensure all the pipes are in line, I get the length of the longest word in the colunm
            then using this length I get how much whitespace should be added onto the word in the cell, he why the string "emptySpaces" exists, it adds on white space */
            int[] spacingLength = findLengths();    //method to find longest word in each column
            String emptySpaces = "                                                                                              ";
            String finalText = "|";
            int finalTextLength;
            
            
            for(int i = 0; i < spacingLength.Length; i++){
                //store cell as word plus required whitespace, uses substring to remove whitespace that is not needed
                finalText += (headers[i] + emptySpaces).Substring(0, spacingLength[i]) + "|"; 
                }
            /*For second line in the md file */
            finalTextLength = finalText.Length;
            finalText += "\n";
            int position = 0;
            while(position < finalTextLength){  //for each character in length of above line (so the first line)
                if(finalText[position] == '|'){ //if character in line above at same position is | 
                    finalText += "|";       //then current character is |
                    } else {
                        finalText += "-"; //else current character is -
                        }
                        position++;//move onto next character
                        }
                        
                        
                        
            finalText += "\n"; //move onto next line
            for(int rowIndex = 0; rowIndex < body.GetLength(0); rowIndex++){ //for every row in body
                finalText += "|"; //new |
                for(int colunmIndex = 0; colunmIndex < body.GetLength(1); colunmIndex++){ //for every column cell in row
                    finalText += (body[rowIndex,colunmIndex] + emptySpaces).Substring(0, spacingLength[colunmIndex]) + "|"; //store as word + |
                    }
                    finalText +=  "\n"; //At end of row move onto next line
                    }
                    
                    
            return finalText;
        }

        public int[] findLengths() {
            int[] longest = new int[headers.Length];
            
            for(int j = 0; j < body.GetLength(1); j++){
                int longer = 0;
                for(int i = 0; i < body.GetLength(0); i++){
                    if(body[i,j].Length > longer){
                        longer = body[i,j].Length;
                    } else if(headers[j].Length > longer){
                        longer = headers[j].Length;
                    }
                }
                longest[j] = longer;
            }
            return longest;
        }  

        public String printHTML() {           
            String finalText = "<table>\n\t<tr>"; //html table boiler plate at top of file

            foreach (String item in headers) //for each cell in header
            {
                finalText += "\n\t\t<th>" + item + "</th>"; //store as <th>word</th>
            }

            for(int blockIndex = 0; blockIndex < body.GetLength(0); blockIndex++){
                finalText += "\n\t<tr>"; //new table colunm
                for(int colunmIndex = 0; colunmIndex < body.GetLength(1); colunmIndex++){
                    finalText += "\n\t\t<td>" + body[blockIndex, colunmIndex] + "</td>";    //table cell
                }
                finalText +=  "\n\t</tr>"; //close table column
            }
            finalText += "\n<table>"; //close table tag

            return finalText;

        }
    }
}
