// <copyright file="CommonSteps.cs" company="Microsoft">
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
    using Bdd.Core.Web.Utils;
    using FLVSBotTest.Executors.Pages;
    using global::Bdd.Core.Web.StepDefinitions;

    using TechTalk.SpecFlow;

    [Binding]
    public class CommonSteps : WebStepDefinitionBase
    {
        [Given(@"I have initilaized FLVS Bot")]
        public void GivenIHaveInitilaizedFLVSBot()
        {
            Logger.Info("Launching the application");
            this.DriverContext.Driver.Manage().Cookies.DeleteAllCookies();
            this.Get<CommonMethods>().LaunchApplication();
        }
    }
}
