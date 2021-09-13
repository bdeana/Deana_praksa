using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ISDB.Model;
using ISDBService.Common;
using ISDB.Common;
using ISDBModel.Common;


namespace ISDB.Controllers
{
    [RoutePrefix("api")]
    public class UserController : ApiController
    {

        IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        // GET 
        [HttpGet]
        [Route("User")]
        public async Task<HttpResponseMessage> GetAsync([FromUri] Pager pager, [FromUri] Sorter sorter, [FromUri] UserFilter userFilter)
        {
            List<UserRest> user = (await _userService.GetUserAsync(sorter, pager, userFilter)).ConvertAll(ConvertFromDomainToRest);
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There are no users");
            }
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        // GET by id
        [HttpGet]
        [Route("User/{User_id}")]
        public async Task<HttpResponseMessage> GetByIdAsync(int user_id)
        {
            UserRest user = ConvertFromDomainToRest(await _userService.GetUserByIdAsync(user_id));

            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no user with id " + Convert.ToString(user_id));
            }

            return Request.CreateResponse(HttpStatusCode.OK, user);
        }


        // POST 
        [HttpPost]
        [Route("User")]
        public async Task<HttpResponseMessage> PostAsync([FromBody] UserRest user)
        {
            UserModel userDomain = ConvertFromRestToDomain(user);
            bool feedback = await _userService.CreateUserAsync(userDomain);
            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There already exists user with id " + Convert.ToString(user.User_id));
            }
            return Request.CreateResponse(HttpStatusCode.OK, "User is succesfully created");
        }




        // PUT for given id modifies user info
        [HttpPut]
        [Route("User/{User_id}")]
        public async Task<HttpResponseMessage> PutAsync(int user_id, [FromBody] UserRest user)
        {
            UserModel userDomain = ConvertFromRestToDomain(user);
            bool feedback = await _userService.UpdateUserAsync(user_id, userDomain);

            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no user with id " + Convert.ToString(user_id));
            }

            return Request.CreateResponse(HttpStatusCode.OK, "User is successfully modified");
        }


        // DELETE 
        [HttpDelete]
        [Route("User/{User_id}")]
        public async Task<HttpResponseMessage> DeleteAsync(int user_id)
        {
            bool feedback = await _userService.DeleteUserAsync(user_id);
            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no user with id " + Convert.ToString(user_id));
            }
            return Request.CreateResponse(HttpStatusCode.OK, "User with id " + Convert.ToString(user_id) + " is deleted");
        }

        public class UserRest
        {
            public UserRest()
            { }

            public UserRest(int user_id, string first_name, string last_name, string email, string country)
            {
                User_id = user_id;
                First_name = first_name;
                Last_name = last_name;
                Email = email;
                Country = country;
            }

            public int User_id { get; set; }
            public string First_name { get; set; }
            public string Last_name { get; set; }
            public string Email { get; set; }
            public string Country { get; set; }
        }

        public UserRest ConvertFromDomainToRest(IUserModel domainUser)
        {
            UserRest userRest = new UserRest(domainUser.User_id, domainUser.First_name, domainUser.Last_name, domainUser.Email, domainUser.Country);
            return userRest;
        }

        public UserModel ConvertFromRestToDomain(UserRest restUser)
        {
            UserModel user = new UserModel(restUser.User_id, restUser.First_name, restUser.Last_name, restUser.Email, restUser.Country);
            return user;
        }
    }
}

