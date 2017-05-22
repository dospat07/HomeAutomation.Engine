//using IdentityServer4.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace HomeAutomation.Engine.Identity
//{
//    public class Config
//    {
//        // scopes define the API resources in your system
//        public static IEnumerable<ApiResource> GetApiResources()
//        {
//            return new List<ApiResource>
//            {
//                new ApiResource("engine", "HomeAutomation Engine api")
//            };
//        }

//        // client want to access resources (aka scopes)
//        public static IEnumerable<Client> GetClients()
//        {

//            return new List<Client>
//            {
//                new Client
//                {
//                    ClientId = "home",
//                    AllowedGrantTypes = GrantTypes.ClientCredentials,

//                    ClientSecrets =
//                    {
//                        new Secret("test".Sha256())
//                    },
//                    AllowedScopes = { "engine" },
//                    AccessTokenLifetime = 30
//                }
//            };
//        }
//    }
//}
