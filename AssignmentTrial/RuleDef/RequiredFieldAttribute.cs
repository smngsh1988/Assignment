﻿using AssignmentTrial.RuleEngineType;
using System;

namespace AssignmentTrial.RuleDef
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class RequiredFieldAttribute : ValidationAttribute
    {
        public RequiredFieldAttribute() : base()
        {

        }
        public RequiredFieldAttribute(string name, string message) : base(name, message)
        {
        }

        public override BrokenRule Validate(object value, ValidationContext context)
        {
            BrokenRule rule = new BrokenRule();

            if (null == value || string.IsNullOrWhiteSpace(value.ToString()))
            {
                rule.IsBroken = true;
                rule.ErrorMessage = this.Message;
                rule.Name = this.Name;
            }

            return rule;
        }
    }
}