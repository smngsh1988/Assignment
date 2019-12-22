using System;
using System.Collections.Generic;

namespace AssignmentTrial.RuleEngineType
{
    public abstract class RuleEngineBase<AnyType>
    {
        public Dictionary<string, List<ValidationAttribute>> Rules { get; set; }
        public abstract void BuildRuleSet();

        public RuleEngineBase()
        {
            Rules = new Dictionary<string, List<ValidationAttribute>>();
            BuildRuleSet();
        }
    }

    public abstract class ValidationAttribute : Attribute
    {
        public string Name { get; set; }
        public string Message { get; set; }

        public ValidationAttribute()
        {

        }

        public ValidationAttribute(string name, string message)
        {
            this.Name = name;
            this.Message = message;
        }

        public abstract BrokenRule Validate(object value, ValidationContext context);
    }

    public class BrokenRule
    {
        public string Name { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsBroken { get; set; }

        public BrokenRule()
        {
            IsBroken = false;
        }
    }

    public class ValidationContext
    {
        public object SourceObject { get; set; }
    }
}