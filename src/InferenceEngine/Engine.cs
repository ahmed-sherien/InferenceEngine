using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public static class Engine
    {
        public static List<Fact> ForwardChaining()
        {
            var new_facts = ForwardChaining(new List<Fact>());
            return new_facts;
        }
        public static List<Fact> ForwardChaining(List<Fact> facts)
        {
            var new_facts = new List<Fact>();
            foreach (var fact in DataStore.Facts)
            {
                var matching_rules = ForwardMatch(DataStore.Rules, fact);
                if (!matching_rules.Any()) continue;
                var added_facts = Execute(matching_rules, fact);
                new_facts.AddRange(added_facts);
            }
            facts.AddRange(new_facts);
            DataStore.Facts.AddRange(new_facts);
            if (new_facts.Any()) ForwardChaining(facts);
            return facts;
        }
        public static List<Tuple<Rule, bool>> ValidateRules()
        {
            var result = new List<Tuple<Rule, bool>>();
            foreach (var rule in DataStore.Rules)
            {
                result.Add(new Tuple<Rule, bool>(rule, ValidateRule(rule)));
            }
            return result;
        }
        public static bool ValidateRule(Rule rule)
        {
            var objects_matched_if = DataStore.Facts.Where(f => f.Adjective == rule.If).Select(f => f.Object).Distinct().ToList();
            var objects_matched_rule = DataStore.Facts.Where(f => f.Adjective == rule.Then && objects_matched_if.Contains(f.Object)).Select(f => f.Object).Distinct().ToList();
            return objects_matched_if.Count == objects_matched_rule.Count;
        }
        public static List<Rule> CreateRules()
        {
            var new_rules = new List<Rule>();
            var adjectives = DataStore.Facts.Select(f => f.Adjective).Distinct().ToList();
            foreach (var adjective in adjectives)
            {
                var objects = DataStore.Facts.Where(f => f.Adjective == adjective)
                                             .Select(f => f.Object)
                                             .Distinct()
                                             .Select(o => new { Object = o, Adjectives = DataStore.Facts.Where(f => f.Object == o).Select(f => f.Adjective) })
                                             .ToList();
                foreach (var compared_adjective in adjectives)
                {
                    if (adjective == compared_adjective) continue;
                    if(objects.All(o => o.Adjectives.Contains(compared_adjective)) && !DataStore.Rules.Any(r => r.If == adjective && r.Then == compared_adjective))
                    {
                        new_rules.Add(new Rule { If = adjective, Then = compared_adjective });
                    }
                }
            }
            DataStore.Rules.AddRange(new_rules);
            return new_rules;
        }
        public static List<Rule> ForwardMatch(List<Rule> rules, Fact fact)
        {
            return rules.Where(r => r.If == fact.Adjective &&
                                    !DataStore.Facts.Where(f => f.Object == fact.Object)
                                              .Select(f => f.Adjective)
                                              .Contains(r.Then))
                        .ToList();
        }
        public static List<Rule> Select(List<Rule> rules)
        {
            return rules.OrderBy(f => f.If).ToList();
        }
        public static List<Fact> Execute(List<Rule> rules, Fact fact)
        {
            var to_be_added = new List<Fact>();
            foreach (var rule in rules)
            {
                var new_fact = new Fact { Object = fact.Object, Adjective = rule.Then };
                to_be_added.Add(new_fact);
            }
            return to_be_added;
        }
    }
}
