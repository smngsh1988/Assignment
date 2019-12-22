using AssignmentTrial.RuleDef;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AssignmentTrial.RuleEngineType
{
    public class XmlRuleEngine<AnyType> : RuleEngineBase<AnyType>, IRuleEngine<AnyType>
    {


        public override void BuildRuleSet()
        {
            System.Type value = typeof(AnyType);

            var props = value.GetProperties();
            var xdoc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\RuleConfigXML\" + $"{value.Name}RuleConfig" + ".xml");

            foreach (var prop in props)
            {
                var rulesAtts = new List<ValidationAttribute>();
                foreach (var itm in xdoc.Descendants("Property"))
                {
                    if (itm.Attribute("Name").Value == prop.Name)
                    {
                        foreach (var item in itm.Descendants("Validator"))
                        {

                            var validationType = item.Attribute("Type").Value;
                            if (validationType == "CheckFieldDatatype")
                            {
                                var errmsg = item.Attribute("ErrorMessage").Value;
                                rulesAtts.Add(new CheckFieldDatatypeAttribute(prop.Name, errmsg));
                            }
                            if (validationType == "RequiredField")
                            {
                                var errmsg = item.Attribute("ErrorMessage").Value;
                                rulesAtts.Add(new RequiredFieldAttribute(prop.Name, errmsg));
                            }
                            if (validationType == "CheckDateTime")
                            {
                                var errmsg = item.Attribute("ErrorMessage").Value;
                                rulesAtts.Add(new CheckDateTimeAttribute(prop.Name, errmsg));
                            }
                            if (validationType == "MaxLengthField")
                            {
                                var errmsg = item.Attribute("ErrorMessage").Value;
                                var max = Convert.ToInt32(item.Attribute("Max").Value);
                                rulesAtts.Add(new MaxLengthFieldAttribute(prop.Name, errmsg, max));
                            }
                        }
                    }
                }
                var ruleItems = new List<ValidationAttribute>();

                foreach (var rule in rulesAtts)
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
                var rules = Rules[prop.Name];// prop.GetCustomAttributes(typeof(ValidationAttribute), true);
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