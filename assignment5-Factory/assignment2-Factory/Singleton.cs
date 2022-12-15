using static System.Console;
using System;
using System.Collections.Generic;
public sealed class Singleton
    {
        
        private string stroke {get; set;} = "black";
        private string fill {get; set;} = "grey";
        private string strokeWidth {get; set;} = "2";

        // The Singleton's constructor should always be private to prevent
        private Singleton() { }

        private static Singleton _instance;

        public static Singleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }

        // Finally, any singleton should define some business logic, which can
        // be executed on its instance.
        public string addStyle()
        {
            return "\" style=\"stroke:" + "black" + ";fill:" + "grey" + ";stroke-width:" + 2 +"\" />";
        }
    }