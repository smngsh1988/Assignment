using System.Configuration;

namespace AssignmentTrial.RuleEngineType
{
    public static class RuleEngineFactory<AnyType> where AnyType : class
    {
        public static IRuleEngine<AnyType> GetEngine()
        {
            IRuleEngine<AnyType> ruleEngine;

            string configurationString = ConfigurationManager.AppSettings["RuleEngineType"];

            if (configurationString.ToLower() == "xmlruleengine")
            {
                ruleEngine = new XmlRuleEngine<AnyType>();
            }
            else
            {
                ruleEngine = new DefaultRuleEngine<AnyType>();
            }

            return ruleEngine;

        }
    }
}