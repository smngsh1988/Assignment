using AssignmentTrial.RuleEngineType;
using System.Collections.Generic;

namespace AssignmentTrial.Models
{
    public class TestInputCollection : List<TestInput>
    {
        IRuleEngine<TestInput> ruleEngine;
        List<string> brokenListString = new List<string>();

        public TestInputCollection()
        {
            ruleEngine = RuleEngineFactory<TestInput>.GetEngine();
        }
        public List<TestInput> testInputs;

        public List<TestInput> TestInputs { get => testInputs; set => testInputs = value; }

        internal List<string> ValidateCollection()
        {
            foreach (TestInput tt in this)
            {
                var results = ruleEngine.Validate(tt);
                foreach (var r in results)
                {
                    if (r.IsBroken)
                    {
                        brokenListString.Add($"{r.Name} rule is broken and the error is { r.ErrorMessage}");
                    }
                }
            }

            return brokenListString;
        }
    }
}