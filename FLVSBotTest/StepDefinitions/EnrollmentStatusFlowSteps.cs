// <copyright file="EnrollmentStatusFlowSteps.cs" company="Microsoft">
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
//    THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR
//    OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//    ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//    OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

namespace FLVSBotTest.StepDefinitions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using global::Bdd.Core.Entities;
    using global::Bdd.Core.Utils;
    using global::Bdd.Core.Web.Executors;
    using global::Bdd.Core.Web.Executors.UI;
    using global::Bdd.Core.Web.StepDefinitions;
    using global::Bdd.Core.Web.Utils;
    using global::FLVSBotTest.Entities;
    using global::FLVSBotTest.Properties;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using TechTalk.SpecFlow;

    [Binding]
    public class EnrollmentStatusFlowSteps : WebStepDefinitionBase
    {
        [When(@"I interact with bot using the ""(.*)"" message")]
        public async Task WhenIInteractWithBotUsingTheMessage(string text)
        {
            await Task.Delay(12000).ConfigureAwait(false);
            this.Get<ElementPage>().FindElementByXPath(nameof(Resources.Bot_textInput)).SendKeys(text);
            this.Get<ElementPage>().FindElementByXPath(nameof(Resources.Bot_textInput)).SendKeys(Keys.Enter);
        }

        [StepDefinition(@"I should see the response ""(.*)"" from bot")]
        public void ThenIShouldSeeTheResponseFromBot(string response)
        {
            Assert.IsNotNull(this.Get<ElementPage>().FindElementByXPath(nameof(Resources.Bot_Response), response).Text);
        }

        [Then(@"I see the following EnrollmentOptions")]
        public void ThenISeeTheFollowingEnrollmentOptions(Table options)
        {
            bool flag = false;
            var action = this.Get<ElementPage>().FindElementsByXPath(nameof(Resources.Bot_actionItem));
            foreach (var x in options.Rows.ToList())
            {
                flag = action.Any(y => y.Text.EqualsIgnoreCase(x["EnrollmentOptions"].ToString()));
                this.VerifyThat(() => Assert.IsTrue(flag, $"The enrollement option {x["EnrollmentOptions"].ToString()} is not present"));
            }
        }

        [Then(@"I click on ""(.*)"" option")]
        public void ThenIClickOnOption(string option)
        {
            this.Get<ElementPage>().FindElementByXPath(nameof(Resources.Bot_Response), option).Click();
        }

        [Then(@"I validate the reponse of the adaptive card for ""(.*)""")]
        public void ThenIValidateTheReponseOfTheAdaptiveCardFor(string card)
        {
            if (card == "placement update")
            {
                Assert.IsNotNull(this.Get<ElementPage>().FindElementByXPath(nameof(Resources.Bot_Response), "Once the preferred start date has arrived, expect to be placed within just a few days, although the process can, in rare instances take longer. We recommend checking your ").Text);
                Assert.IsNotNull(this.Get<ElementPage>().FindElementByXPath(nameof(Resources.Bot_Response), "If you have been waiting more than 14 days to be placed in your course, you may transfer to a live agent now for assistance or submit a help ticket (avg response time).").Text);
                Assert.IsNotNull(this.Get<ElementPage>().FindElementByXPath(nameof(Resources.PlacementUpdate_image)).GetAttribute("src"));
            }
        }
    }
}
