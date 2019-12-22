
using AssignmentTrial.RuleDef;
using AssignmentTrial.RuleEngineType;

namespace AssignmentTrial.Models
{
    [RuleEngineType(RuleType = typeof(DefaultRuleEngine<TestInput>))]
    public class TestInput
    {
        public string signal { get; set; }
        public string value_type { get; set; }
        public string value { get; set; }
    }
}