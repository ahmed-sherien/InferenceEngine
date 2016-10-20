using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class DataStore
    {
        static DataStore()
        {
            Facts = new List<Fact>
            {
                new Fact { Object = "Ahmed", Adjective = "Human" },
                new Fact { Object = "Salwa", Adjective = "Human" },
                new Fact { Object = "Anas", Adjective = "Human" },
                new Fact { Object = "ANOS", Adjective = "Software" },
                new Fact { Object = "Julie", Adjective = "Cat" },
                new Fact { Object = "Tomboss", Adjective = "Cat" },
                new Fact { Object = "Julie", Adjective = "Mortal" },
                new Fact { Object = "Tomboss", Adjective = "Mortal" },
            };
            Rules = new List<Rule>
            {
                new Rule { If = "Human", Then = "Mortal" },
            };
        }
        public static List<Fact> Facts { get; set; }
        public static List<Rule> Rules { get; set; }
    }

    public class Fact
    {
        public string Object { get; set; }
        public string Adjective { get; set; }
        public override string ToString()
        {
            return $"{Object} is a {Adjective}";
        }

    }

    public class Rule
    {
        public string If { get; set; }
        public string Then { get; set; }
        public override string ToString()
        {
            return $"If it is {If}, then it is {Then}";
        }
    }
}
