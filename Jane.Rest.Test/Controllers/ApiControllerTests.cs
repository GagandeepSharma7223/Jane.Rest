using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using Jane.API.Controllers.Public;
using Jane.Base.Tests;
using NUnit.Framework;

namespace Jane.Rest.Test.Controllers
{
    [TestFixture]
    public class ApiControllerTests : BaseDataContextTests
    {
        protected AuthenticationController _testController;

        protected override void DoSetup()
        {
            _testController = new AuthenticationController();
        }

        [Test]
        public void CheckApiAuthenticationWithoutKey()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/public/authenticate/checkAuthentication");
            request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            _testController.Request = request;
            var result = _testController.CheckAuthentication();
            string content = result.Content.ReadAsStringAsync().Result;
            Assert.AreEqual("false", content);
        }

        [Test]
        public void CheckApiAuthenticationWithKey()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/public/authenticate/checkAuthentication");
            request.Headers.Add("PrivateKey", "dsfdsf");
            request.Headers.Add("PublicKey", "dsfdsfsd");
            request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            _testController.Request = request;
            var result = _testController.CheckAuthentication();
            string content = result.Content.ReadAsStringAsync().Result;
            Assert.AreEqual("true", content);
        }
    }
}
