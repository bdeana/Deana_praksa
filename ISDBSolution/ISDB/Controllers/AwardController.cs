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
using ISDBModel.Common;

namespace ISDB.Controllers
{
    public class AwardController : ApiController
    {
        IAwardService _awardService;

        public AwardController(IAwardService awardService)
        {
            _awardService = awardService;
        }

        // GET 
        [HttpGet]
        [Route("api/Award")]
        public async Task<HttpResponseMessage> GetAsync([FromUri] Sorter sorter, [FromUri] Pager pager, [FromUri] AwardFilter awardFilter)
        {
            List<AwardRest> award = (await _awardService.GetAwardsAsync(sorter, pager, awardFilter)).ConvertAll(ConvertFromDomainToRest);



            //if (award == null)
            //{
            //    return Request.CreateResponse(HttpStatusCode.NotFound, "There are no awards");
            //}
            return Request.CreateResponse(HttpStatusCode.OK, award);
        }

        // GET by id
        [HttpGet]
        [Route("api/Award/{Award_id}")]
        public async Task<HttpResponseMessage> GetByIdAsync(int award_id)
        {
            AwardRest award = ConvertFromDomainToRest (await _awardService.GetAwardByIdAsync(award_id));

            if (award == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no award with id " + Convert.ToString(award_id));
            }

            return Request.CreateResponse(HttpStatusCode.OK, award);
        }


        // POST 
        [HttpPost]
        [Route("api/Award")]
        public async Task<HttpResponseMessage> PostAsync([FromBody] AwardRest award)
        {
            Award awardDomain = ConvertFromRestToDomain(award);

            bool feedback = await _awardService.CreateAwardAsync(awardDomain);
            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Award with given id already exists.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Award is succesfully created");
        }




        // PUT for given id modifies award 
        [HttpPut]
        [Route("api/Award/{award_id}")]
        public async Task<HttpResponseMessage> PutAsync(int award_id, [FromBody] AwardRest award)
        {
            Award awardDomain = ConvertFromRestToDomain(award);
            bool feedback = await _awardService.UpdateAwardAsync(award_id, awardDomain);

            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no award with id " + Convert.ToString(award_id));
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Award is successfully modified");
        }


        // DELETE 
        [HttpDelete]
        [Route("api/Award/{award_id}")]
        public async Task<HttpResponseMessage> DeleteAsync(int award_id)
        {
            bool feedback = await _awardService.DeleteAwardAsync(award_id);
            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no award with id " + Convert.ToString(award_id) );
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Award with id " + award_id + " is deleted");
        }

        public class AwardRest
        {
            public int Award_id { get; set; }
            public string Award_name { get; set; }
            public int Reciving_year { get; set; }
            public int Song_fk { get; set; }
            public int Album_fk { get; set; }
            

            public AwardRest() { }

            public AwardRest(int award_id, string award_name, int reciving_year, int song_fk, int album_fk)
            {
                Award_id = award_id;
                Award_name = award_name;
                Reciving_year = reciving_year;
                Song_fk = song_fk;
                Album_fk = album_fk;
            }
        }


        public AwardRest ConvertFromDomainToRest(IAward domainAward)
        {
            AwardRest awardRest = new AwardRest(domainAward.Award_id, domainAward.Award_name, domainAward.Reciving_year, domainAward.Song_fk, domainAward.Album_fk);
            return awardRest;
        }

        public Award ConvertFromRestToDomain(AwardRest restAward)
        {
            Award awardDomain = new Award(restAward.Award_id, restAward.Award_name, restAward.Reciving_year, restAward.Song_fk, restAward.Album_fk);
            return awardDomain;
        }
    }
}
