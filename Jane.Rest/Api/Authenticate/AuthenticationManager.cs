using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jane.Rest.Api.Authenticate
{
    public class AuthenticationManager : Request
    {

        public AuthenticationManager(string baseUrl, string publicKey, string privateKey)
            : base(baseUrl, publicKey, privateKey)
        {
        }
        public Task<bool> IsAuthenticatedAsync()
        {
            return ExecuteAuthenticatedJsonRequestAsync<bool>(ApiUrlSettings.Authenticate, HttpMethod.Post);
        }


        //EXAMPLES
        //public Task<TraQDigitalItem> GetDigitalItemByIdAsync(long id)
        //{
        //    return ExecuteAuthenticatedJsonRequestAsync<TraQDigitalItem>(ApiUrlSettings.GetDigitalItemById + "/" + id, HttpMethod.Post);
        //}

        //public Task<bool> UploadDigitalItemAsync(TraQDigitalItem digitalItem, Stream fileStream)
        //{
        //    return ExecuteAuthenticatedJsonRequestAsync<bool>(ApiUrlSettings.UploadDigitalItem, HttpMethod.Post, fileStream, digitalItem);
        //}

    }
}
