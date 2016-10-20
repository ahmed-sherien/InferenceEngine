using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RulesValidation();
            ForwardChaining();
            RulesValidation();
            CreateRules();
            RulesValidation();
            Console.ReadLine();
        }
        public static void ForwardChaining()
        {
            var facts = Engine.ForwardChaining();
            Console.WriteLine("----------------------------------");
            Console.WriteLine("New facts:");
            Console.WriteLine("----------");
            foreach (var fact in facts)
            {
                Console.WriteLine(fact);
            }
            Console.WriteLine("----------------------------------");
            Console.WriteLine($"Number of facts: {DataStore.Facts.Count}");
            Console.WriteLine("----------------------------------");
        }
        public static void RulesValidation()
        {
            Console.WriteLine("----------------------------------");
            var rules_validity = Engine.ValidateRules();
            Console.WriteLine("Rules Validity:");
            Console.WriteLine("---------------");
            foreach (var rule_validity in rules_validity)
            {
                Console.WriteLine($"Rule: '{rule_validity.Item1}' is {(rule_validity.Item2 ? "valid" : "invalid")}");
            }
            Console.WriteLine("----------------------------------");
        }
        public static void CreateRules()
        {
            var new_rules = Engine.CreateRules();
            Console.WriteLine("----------------------------------");
            Console.WriteLine("New rules:");
            Console.WriteLine("----------");
            foreach (var rule in new_rules)
            {
                Console.WriteLine(rule);
            }
            Console.WriteLine("----------------------------------");
        }
    }
}
