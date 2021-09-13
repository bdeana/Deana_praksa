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
    public class SongController : ApiController
    {
        ISongService _songService;

        public SongController(ISongService songService)
        {
            _songService = songService;
        }

        // GET 
        [HttpGet]
        [Route("Song")]
        public async Task<HttpResponseMessage> GetAsync([FromUri] Pager pager, [FromUri] Sorter sorter, [FromUri] SongFilter songFilter)
        {
            List<SongRest> song = (await _songService.GetSongAsync(sorter, pager, songFilter)).ConvertAll(ConvertFromDomainToRest);
            if (song == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There are no songs");
            }
            return Request.CreateResponse(HttpStatusCode.OK, song);
        }

        // GET by id
        [HttpGet]
        [Route("Song/{Song_id}")]
        public async Task<HttpResponseMessage> GetByIdAsync(int song_id)
        {
            SongRest song = ConvertFromDomainToRest(await _songService.GetSongByIdAsync(song_id));

            if (song == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no song with id " + Convert.ToString(song_id));
            }

            return Request.CreateResponse(HttpStatusCode.OK, song);
        }


        // POST 
        [HttpPost]
        [Route("Song")]
        public async Task<HttpResponseMessage> PostAsync([FromBody] SongRest song)
        {
            SongModel songDomain = ConvertFromRestToDomain(song);
            bool feedback = await _songService.CreateSongAsync(songDomain);
            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There already exists song with id " 
                                                                        + Convert.ToString(song.Song_id) +
                                                                        " or album, genre or production company are invalid");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Song is succesfully created");
        }




        // PUT for given id modifies song info
        [HttpPut]
        [Route("Song/{Song_id}")]
        public async Task<HttpResponseMessage> PutAsync(int song_id, [FromBody] SongRest song)
        {
            SongModel songDomain = ConvertFromRestToDomain(song);
            bool feedback = await _songService.UpdateSongAsync(song_id, songDomain);

            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no song with id "
                                                                        + Convert.ToString(song.Song_id) +
                                                                        " or album, genre or production company are invalid");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Song is successfully modified");
        }


        // DELETE 
        [HttpDelete]
        [Route("Song/{song_id}")]
        public async Task<HttpResponseMessage> DeleteAsync(int song_id)
        {
            bool feedback = await _songService.DeleteSongAsync(song_id);
            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no song with id " + Convert.ToString(song_id));
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Song with id " + Convert.ToString(song_id) + " is deleted");
        }
        public class SongRest
        {
            public SongRest()
            { }

            public SongRest(int song_id, string song_name, int release_year, string language, string studio_location, int duration, int genre_fk, int album_fk, int production_company_fk)
            {
                Song_id = song_id;
                Song_name = song_name;
                Release_year = release_year;
                Language = language;
                Studio_location = studio_location;
                Duration = duration;
                Genre_fk = genre_fk;
                Album_fk = album_fk;
                Production_Company_fk = production_company_fk;
            }

            public int Song_id { get; set; }
            public string Song_name { get; set; }
            public int Release_year { get; set; }
            public string Language { get; set; }
            public string Studio_location { get; set; }
            public int Duration { get; set; }
            public int Genre_fk {get; set; }
            public int Album_fk { get; set; }
            public int Production_Company_fk { get; set; }
            
        }

        public SongRest ConvertFromDomainToRest(ISongModel domainSong)
        {
            SongRest songRest = new SongRest(domainSong.Song_id, domainSong.Song_name, domainSong.Release_year, domainSong.Language, domainSong.Studio_location,
                                                domainSong.Duration, domainSong.Genre_fk, domainSong.Album_fk, domainSong.Production_Company_fk);
            return songRest;
        }

        public SongModel ConvertFromRestToDomain(SongRest restSong)
        {
            SongModel song = new SongModel(restSong.Song_id, restSong.Song_name, restSong.Release_year, restSong.Language, restSong.Studio_location,
                                            restSong.Duration, restSong.Genre_fk, restSong.Album_fk, restSong.Production_Company_fk);
            return song;
        }
    }
}
