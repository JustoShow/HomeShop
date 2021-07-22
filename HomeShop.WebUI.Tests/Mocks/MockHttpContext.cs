using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HomeShop.WebUI.Tests.Mocks
{
    public class MockHttpContext : HttpContextBase
    {
        private readonly MockRequest request;
        private readonly MockResponse response;
        private readonly HttpCookieCollection cookies;

        public MockHttpContext()
        {
            cookies = new HttpCookieCollection();
            this.request = new MockRequest(cookies);
            this.response = new MockResponse(cookies);
        }

        public override HttpRequestBase Request => request;
        public override HttpResponseBase Response => response;
    }

    public class MockResponse : HttpResponseBase
    {
        private readonly HttpCookieCollection _cookies;

        public MockResponse(HttpCookieCollection cookies)
        {
            _cookies = cookies;
        }

        public override HttpCookieCollection Cookies
        {
            get
            {
                return _cookies;
            }
        }
    }

    public class MockRequest : HttpRequestBase
    {
        private readonly HttpCookieCollection _cookies;

        public MockRequest(HttpCookieCollection cookies)
        {
            _cookies = cookies;
        }

        public override HttpCookieCollection Cookies
        {
            get
            {
                return _cookies;
            }
        }
    }
}
