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
using ISDBRepository;
using ISDBModel.Common;

namespace ISDB.Controllers
{
    [RoutePrefix("api")]
    public class ReviewController : ApiController
    {
        IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET 
        [HttpGet]
        [Route("Review")]
        public async Task<HttpResponseMessage> GetAsync([FromUri] Pager pager, [FromUri] Sorter sorter, [FromUri] ReviewFilter reviewFilter)
        {
            List<ReviewRest> review = (await _reviewService.GetReviewAsync(sorter, pager, reviewFilter)).ConvertAll(ConvertFromDomainToRest);
            if (review == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There are no reviews");
            }
            return Request.CreateResponse(HttpStatusCode.OK, review);
        }

        // GET by id
        [HttpGet]
        [Route("Review/{User_fk}/{Song_fk}")]           
        public async Task<HttpResponseMessage> GetByIdAsync(int user_fk, int song_fk)
        {
            
            ReviewRest review = ConvertFromDomainToRest(await _reviewService.GetReviewByIdAsync(user_fk, song_fk));

            if (review == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no review of song with id " + Convert.ToString(song_fk) +
                                                                        " by user with id " + Convert.ToString(user_fk));
            }

            return Request.CreateResponse(HttpStatusCode.OK, review);
        }


        // POST 
        [HttpPost]
        [Route("Review")]
        public async Task<HttpResponseMessage> PostAsync([FromBody] ReviewRest review)
        {
            ReviewModel reviewDomain = ConvertFromRestToDomain(review);
            bool feedback = await _reviewService.CreateReviewAsync(reviewDomain);
            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There already exists review of song " + Convert.ToString(review.Song_fk) +
                                                                         " by user " + Convert.ToString(review.User_fk) +
                                                                         " or user or song are invalid");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Review is succesfully created");
        }




        // PUT for given ids modifies review 
        [HttpPut]
        [Route("Review/{User_fk}/{Song_fk}")]     
        public async Task<HttpResponseMessage> PutAsync(int user_fk, int song_fk, [FromBody] ReviewRest review)
        {
            ReviewModel reviewDomain = ConvertFromRestToDomain(review);
            bool feedback = await _reviewService.UpdateReviewAsync(user_fk, song_fk, reviewDomain);

            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no review of song with id " + Convert.ToString(song_fk) +
                                                                        " by user with id " + Convert.ToString(user_fk));
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Review is successfully modified");
        }


        // DELETE 
        [HttpDelete]
        [Route("Review/{user_fk}/{song_fk}")]
        public async Task<HttpResponseMessage> DeleteAsync(int user_fk, int song_fk)       
        {
            bool feedback = await _reviewService.DeleteReviewAsync(user_fk, song_fk);
            if (feedback != true)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no review of song with id " + Convert.ToString(song_fk) +
                                                                        " by user with id " + Convert.ToString(user_fk));
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Review of song with id " + Convert.ToString(song_fk) +
                                                                        " by user with id " + Convert.ToString(user_fk) + " successfully deleted");
        }
        public class ReviewRest
        {
            public ReviewRest()
            { }

            public ReviewRest(string comment, int grade, int user_fk, int song_fk)
            {
                Comment_ = comment;
                Grade = grade;
                User_fk = user_fk;
                Song_fk = song_fk;
            }

            public string Comment_ { get; set; }
            public int Grade { get; set; }
            public int User_fk { get; set; }
            public int Song_fk { get; set; }
        }

        public ReviewRest ConvertFromDomainToRest(IReviewModel domainReview)
        {
            ReviewRest reviewRest = new ReviewRest(domainReview.Comment_, domainReview.Grade, domainReview.User_fk, domainReview.Song_fk);
            return reviewRest;
        }

        public ReviewModel ConvertFromRestToDomain(ReviewRest restReview)
        {
            ReviewModel review = new ReviewModel(restReview.Comment_, restReview.Grade, restReview.User_fk, restReview.Song_fk);
            return review;
        }
    }
}
