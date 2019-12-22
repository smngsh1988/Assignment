using System.Collections.Generic;

namespace AssignmentTrial.RuleEngineType
{
    public class DefaultRuleEngine<AnyType> : RuleEngineBase<AnyType>, IRuleEngine<AnyType>
    {
        public override void BuildRuleSet()
        {
            var value = typeof(AnyType);

            var props = value.GetProperties();

            foreach (var prop in props)
            {
                var rulesAttrs = prop.GetCustomAttributes(typeof(ValidationAttribute), true);

                var ruleItems = new List<ValidationAttribute>();

                foreach (var rule in rulesAttrs)
                {
                    var ruleAttribute = rule as ValidationAttribute;
                    ruleItems.Add(ruleAttribute);
                }
                Rules[prop.Name] = ruleItems;
            }
        }

        public List<BrokenRule> Validate(AnyType value)
        {
            var results = new List<BrokenRule>();

            var props = value.GetType().GetProperties();

            foreach (var prop in props)
            {
                var rules = Rules[prop.Name];
                foreach (var rule in rules)
                {
                    var ruleAttribute = rule as ValidationAttribute;
                    var ruleResult = ruleAttribute.Validate(prop.GetValue(value), new ValidationContext { SourceObject = value });
                    if (ruleResult.IsBroken)
                    {
                        results.Add(ruleResult);
                    }
                }
            }
            return results;
        }

    }
}