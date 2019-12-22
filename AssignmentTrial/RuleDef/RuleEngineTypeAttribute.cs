using System;

namespace AssignmentTrial.RuleDef
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RuleEngineTypeAttribute : Attribute
    {
        public Type RuleType { get; set; }

        public RuleEngineTypeAttribute() : base()
        {

        }
        public RuleEngineTypeAttribute(string ruleType)
        {
        }

    }
}