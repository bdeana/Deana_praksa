using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISDB.Model;
using ISDB.Repository;
using ISDBService.Common;
using ISDBRepository.Common;
using ISDB.Common;

namespace ISDB.Service
{
    public class ReviewService: IReviewService
    {
        IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<List<ReviewModel>> GetReviewAsync(Sorter sorter, Pager pager, ReviewFilter reviewFilter)
        {
            return await _reviewRepository.GetReviewAsync(sorter, pager, reviewFilter);

        }

        public async Task<ReviewModel> GetReviewByIdAsync(int user_fk, int song_fk)
        {
            return await _reviewRepository.GetReviewByIdAsync(user_fk, song_fk);
        }

        public async Task<bool> CreateReviewAsync(ReviewModel review)
        {
            return await _reviewRepository.CreateReviewAsync(review);
        }

        public async Task<bool> UpdateReviewAsync(int user_fk, int song_fk, ReviewModel review)
        {
            return await _reviewRepository.UpdateReviewAsync(user_fk, song_fk , review);
        }

        public async Task<bool> DeleteReviewAsync(int user_fk, int song_fk)
        {
            return await _reviewRepository.DeleteReviewAsync(user_fk, song_fk);
        }
    }
}
