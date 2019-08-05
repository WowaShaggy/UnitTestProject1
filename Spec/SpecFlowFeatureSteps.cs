using System;
using TechTalk.SpecFlow;

namespace Spec
{
    [Binding]
    public class SpecFlowFeatureSteps
    {
        [When(@"I get number of news")]
        public void WhenIGetNumberOfNews()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"Number of news is (.*)")]
        public void ThenNumberOfNewsIs(int p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
