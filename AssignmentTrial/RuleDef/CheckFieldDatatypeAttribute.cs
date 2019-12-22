using AssignmentTrial.RuleEngineType;
using System;

namespace AssignmentTrial.RuleDef
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class CheckFieldDatatypeAttribute : ValidationAttribute
    {
        public CheckFieldDatatypeAttribute() : base()
        {

        }
        public CheckFieldDatatypeAttribute(string name, string message) : base(name, message)
        {
        }

        public override BrokenRule Validate(object value, ValidationContext context)
        {
            BrokenRule rule = new BrokenRule();
            var DataType = ((AssignmentTrial.Models.TestInput)context.SourceObject).value_type;
            var signal = ((AssignmentTrial.Models.TestInput)context.SourceObject).signal;
            try
            {
                if (null == value || string.IsNullOrWhiteSpace(value.ToString()))
                {
                    rule.IsBroken = true;
                    rule.ErrorMessage = $"signal: { signal} {this.Message}";
                    rule.Name = this.Name;
                }

                if (DataType.Equals("String", StringComparison.OrdinalIgnoreCase))
                {
                    var val = Convert.ToString(value);
                }
                else if (DataType.Equals("Integer", StringComparison.OrdinalIgnoreCase))
                {
                    var val = Convert.ToInt32(value);
                }
                else if (DataType.Equals("DateTime", StringComparison.OrdinalIgnoreCase))
                {
                    var val = Convert.ToDateTime(value);
                    if (val > DateTime.Now)
                    {
                        rule.IsBroken = true;
                        rule.ErrorMessage = $"signal: {signal} should not be in future";
                        rule.Name = this.Name;
                    }
                }
                else
                {
                    rule.IsBroken = true;
                    rule.ErrorMessage = $"signal: {signal} No maching datatype defined";
                    rule.Name = this.Name;
                }
            }
            catch (Exception e)
            {
                rule.IsBroken = true;
                rule.ErrorMessage = $"signal: { signal} value is not a matching value type";
                rule.Name = this.Name;
            }

            return rule;
        }
    }
}