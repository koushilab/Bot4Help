// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonMethods.cs" company="Microsoft">
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
//    THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR
//    OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//    ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//    OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FLVSBotTest.Executors.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using Bdd.Core.Utils;
    using Bdd.Core.Web.Executors;
    using Bdd.Core.Web.Utils;
    using FLVSBotTest.Properties;
    using global::FLVSBotTest.Entities;
    using NUnit.Framework;
    using Ocaramba;
    using OpenQA.Selenium;

    public class CommonMethods : ProjectPageBase
    {
        private readonly DriverContext driverContext;
        private readonly NameValueCollection appSettings = Bdd.Core.ConfigManager.GetSection("appSettings");
        private readonly NameValueCollection missionControlCredentials = Bdd.Core.ConfigManager.GetSection("FlvsCredentials");
        private string url;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonMethods"/> class.
        /// This method is to set the driver.
        /// </summary>
        /// <param name="driverContext">this is driver parameter.</param>
        public CommonMethods(DriverContext driverContext)
           : base(driverContext)
        {
            try
            {
                this.driverContext = driverContext;
                this.driverContext.Get<ElementPage>().WaitUntilPageLoad();
            }
            catch (NoSuchWindowException)
            {
                this.driverContext = driverContext;
            }
        }

        /// <summary>
        /// This will launch the URL in the Browser based on the environment type.
        /// </summary>
        /// <param name="userNameformat">userNameformat.</param>
        public void LaunchApplication()
        {
            string appSettingValues = this.appSettings.Get("Env");
            this.url = this.missionControlCredentials.Get(appSettingValues);

            try
            {
                this.Get<UrlPage>().NavigateToUrl(this.url);
            }
            catch (Exception e)
            {
                this.Get<UrlPage>().NavigateToUrl(this.url);
                Logger.Info("In exception: " + e);
            }

            this.Get<ElementPage>().WaitUntilPageLoad(30);
            Logger.Info("Launching the application URL in browser");
        }

        /// <summary>
        /// It switches to the given window.
        /// </summary>
        /// <param name="id">Id of the window.</param>
        public void SwitchToWindow(string id)
        {
            this.Driver.SwitchTo().Window(id);
        }

        /// <summary>
        /// It switches to the given window.
        /// </summary>
        /// <returns>Returns current window handle.</returns>
        public string GetCurrentWindowHandle()
        {
            return this.Driver.CurrentWindowHandle;
        }

        /// <summary>
        /// It returns the current window url.
        /// </summary>
        /// <returns>Returns current window url.</returns>
        public string GetCurrentWindowUrl()
        {
            return this.Driver.Url;
        }

        /// <summary>
        /// It returns the current window title.
        /// </summary>
        /// <returns>Returns current window title.</returns>
        public string GetCurrentWindowTitle()
        {
            return this.Driver.Title;
        }

        /// <summary>
        /// It returns the list of window handles.
        /// </summary>
        /// <returns>IReadOnlyCollection.<string>.</returns>
        public IReadOnlyCollection<string> ListOfWindowHandles()
        {
            return this.Driver.WindowHandles;
        }

        /// <summary>
        /// To convert time from UTC to EST.
        /// </summary>
        /// <param name="timeUtc">Current time in UTC.</param>
        /// <returns>Task.</returns>
        public DateTime ConvertUTCtoEST(DateTime timeUtc)
        {
            var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern standard time");
            var timeEST = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
            return timeEST;
        }
    }
}