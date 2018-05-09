using Core.Concrete;
using Core.Models;
using Newtonsoft.Json;
using System.Web.Http;

namespace Web.Controllers
{
    public class WebController : ApiController
    {
        [HttpGet]
        public string Get(int id)
        {
            return JsonConvert.SerializeObject(UserManagement.Get(id), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        [HttpGet]
        public string GetAll()
        {
            return JsonConvert.SerializeObject(UserManagement.GetAll(), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            //return UserManagement.GetAll();
        }

        [HttpPost]
        public void Add([FromBody]User user)
        {
            UserManagement.Add(user);
        }

        [HttpPut]
        public void Edit([FromBody]User user)
        {
            UserManagement.Update(user);
        }

        [HttpDelete]
        public void Delete([FromBody]User user)
        {
            UserManagement.Delete(user);
        }

    }
}