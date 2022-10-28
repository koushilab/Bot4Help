// <copyright file="RandomWebOperationsSteps.cs" company="Microsoft">
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
    using System.IO;
    using System.Threading.Tasks;
    using global::Bdd.Core.Entities;
    using global::Bdd.Core.Utils;
    using global::Bdd.Core.Web.Executors;
    using global::Bdd.Core.Web.Executors.UI;
    using global::Bdd.Core.Web.StepDefinitions;
    using global::Bdd.Core.Web.Utils;
    using global::FLVSBotTest.Entities;
    using global::FLVSBotTest.Properties;
    using NUnit.Framework;
    using Ocaramba.Types;
    using OpenQA.Selenium;
    using TechTalk.SpecFlow;

    [Binding]
    public class RandomWebOperationsSteps : WebStepDefinitionBase
    {
        private IWebElement submit = null;
        private IWebElement input = null;
        private dynamic data;

        [Given(@"I have launched ""(.*)"" site")]
        public void GivenIHaveLaunchedSite(string url)
        {
            var user = this.ScenarioContext.GetCredential<Credentials>(this.FeatureContext, "admin", "input=Credentials.xlsx");
            Assert.IsNotNull(user);

            // user = this.ScenarioContext.GetCredential("user", "input=Credentials.xlsx");
            this.Get<UrlPage>().NavigateToUrl(Resources.ResourceManager.GetString(url));
        }

        [Given(@"I have entered ""(.*)"" into the ""(.*)"" field")]
        public void GivenIHaveEnteredIntoTheField(string searchText, string searchBox)
        {
            this.Get<ElementPage>().WaitUntilPageLoad();
            this.submit = this.Get<ElementPage>().FindElementByXPath(searchBox);
            this.submit.SendKeys(searchText);
        }

        [When(@"I press submit")]
        public void WhenIPressSubmit()
        {
            this.submit.Submit();
        }

        [Then(@"the ""(.*)"" should be shown on the screen")]
        public void ThenTheSearchResultsShouldBeShownOnTheScreen(string searchResults)
        {
            var key = searchResults.Replace(" ", string.Empty);
            this.Get<ElementPage>().WaitUntilElementIsVisible(key);
            var results = this.Get<ElementPage>().FindElementByXPath(key);
            Assert.IsNotNull(results);
        }

        [Given(@"I have clicked ""(.*)"" button")]
        public async Task GivenIHaveClickedButton(string buttonName)
        {
            var filePath = @"TestData\Sample.xlsx".GetFullPath();
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            this.input = this.Get<ElementPage>().GetElement(buttonName);
            this.input.Click();

            // Wait for the File-save popup
            await Task.Delay(1000).ConfigureAwait(false);

            // TO DO: Test
            this.input.SendKeys(filePath);
            this.input.SendKeys("{ENTER}");

            // Wait for the file to download
            await Task.Delay(1000).ConfigureAwait(false);
        }

        [When(@"I enter ""(.*)""")]
        [When(@"I hit ""(.*)""")]
        public void WhenIEnter(string text)
        {
            this.input.SendKeys(text);
        }

        [Then(@"the value should be set")]
        public void ThenTheValueShouldBeSet()
        {
            var result = this.input.GetAttribute("value");
            this.VerifyThat(() => Assert.IsTrue(!string.IsNullOrWhiteSpace(result)));
        }

        [Then(@"I verify if data in the file is ""(.*)""")]
        public void ThenIVerifyIfDataInTheFileIs(string expectedData)
        {
            var dataInFile = File.ReadAllText(Constants.FilePath.GetFullPath());
            this.VerifyThat(() => Assert.AreEqual(expectedData, dataInFile));
        }

        [Given(@"I double-click ""(.*)"" button")]
        public void GivenIDouble_ClickButton(string key)
        {
            var link = this.Get<ElementPage>().GetElement(nameof(Resources.ClickHereToTest));
            this.Get<ElementPage>().DoubleClick(link);
        }

        [Then(@"the output should show ""(.*)""")]
        public void ThenTheOutputShouldShow(string text)
        {
            var textArea = this.Get<ElementPage>().GetElement(new ElementLocator(Ocaramba.Locator.TagName, "textarea"));
            var result = this.Get<ElementPage>().WaitUntil(() => textArea.GetAttribute("value").Contains(text));
            Assert.IsTrue(result);
        }

        [StepDefinition(@"I enter path of a file to be uploaded")]
        public void WhenIEnterPathOfAFileToBeUploaded()
        {
            this.input.SendKeys(Constants.FilePath.GetFullPath());
        }

        [When(@"I search the downloads tab for the ""(.*)"" file")]
        public async Task WhenISearchTheTabForTheFile(string fileName)
        {
            this.data = await this.Get<FilePage>().CheckDownloadedFileContent(fileName, this.ScenarioContext, this.FeatureContext).ConfigureAwait(false);
        }

        [When(@"I go to downloads tab and read file (.*)")]
        public Task WhenIGoToDownloadsTabAndReadFile(int index)
        {
            return Task.CompletedTask;
        }

        [Then(@"the content of the file should be valid")]
        public void ThenTheContentOfTheFileShouldBeValid()
        {
            Assert.IsTrue(this.data.Count > 0);
        }
    }
}
