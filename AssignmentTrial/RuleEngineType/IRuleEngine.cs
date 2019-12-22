using System.Collections.Generic;

namespace AssignmentTrial.RuleEngineType
{
    public interface IRuleEngine<AnyType>
    {
        List<BrokenRule> Validate(AnyType value);
    }
}