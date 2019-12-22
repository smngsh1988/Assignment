using AssignmentTrial.RuleEngineType;
using System;

namespace AssignmentTrial.RuleDef
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class CheckDateTimeAttribute : ValidationAttribute
    {
        public int Max { get; set; }

        public CheckDateTimeAttribute() : base()
        {

        }
        public CheckDateTimeAttribute(string name, string message)
            : base(name, message)
        {
        }

        public override BrokenRule Validate(object value, ValidationContext context)
        {
            BrokenRule rule = new BrokenRule();

            if (null != value || !string.IsNullOrWhiteSpace(value.ToString()))
            {
                if (value.ToString().Length >= Max)
                {
                    rule.IsBroken = true;
                    rule.Name = this.Name;
                    rule.ErrorMessage = this.Message;
                }
            }
            return rule;
        }
    }
}