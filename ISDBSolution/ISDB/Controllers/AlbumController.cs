using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ISDB.Model;
using ISDB.Service;
using System.Threading.Tasks;
using ISDBService.Common;
using ISDB.Common;

namespace ISDB.Controllers
{
    public class AlbumController : ApiController
    {
        IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        // GET 
        [HttpGet]
        [Route("api/Album")]
        public async Task<HttpResponseMessage> GetAsync([FromUri] Sorter sorter, [FromUri] Pager pager, [FromUri] AlbumFilter albumFilter)
        {
            List<Album> album = await _albumService.GetAlbumsAsync(sorter, pager, albumFilter);

            if (album == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There are no albums");
            }
            return Request.CreateResponse(HttpStatusCode.OK, album);
        }

        // GET by id
        [HttpGet]
        [Route("api/Album/{Album_id}")]
        public async Task<HttpResponseMessage> GetByIdAsync(int album_id)
        {
            Album album = await _albumService.GetAlbumByIdAsync(album_id);

            if (album == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no album with id " + Convert.ToString(album_id));
            }

            return Request.CreateResponse(HttpStatusCode.OK, album);
        }


        // POST 
        [HttpPost]
        [Route("api/Album")]
        public async Task<HttpResponseMessage> PostAsync([FromBody] Album album)
        {
            bool feedback = await _albumService.CreateAlbumAsync(album);
            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Album with given id already exists.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Album is succesfully created");
        }




        // PUT for given id modifies album 
        [HttpPut]
        [Route("api/Albums/{album_id}")]
        public async Task<HttpResponseMessage> PutAsync(int album_id, [FromBody] Album album)
        {
            bool feedback = await _albumService.UpdateAlbumAsync(album_id, album);

            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no album with id " + Convert.ToString(album_id));
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Album is successfully modified");
        }


        // DELETE 
        [HttpDelete]
        [Route("api/Album/{album_id}")]
        public async Task<HttpResponseMessage> DeleteAsync(int album_id)
        {
            bool feedback = await _albumService.DeleteAlbumAsync(album_id);
            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no album with id " + Convert.ToString(album_id) );
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Album with id " + album_id + " is deleted");
        }
    }
}
