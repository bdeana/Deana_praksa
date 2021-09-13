using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISDB.Model;
using ISDB.Common;

namespace ISDBRepository.Common
{
    public interface IReviewRepository
    {
        Task<List<ReviewModel>> GetReviewAsync(Sorter sorter, Pager pager, ReviewFilter reviewFilter);
        Task<ReviewModel> GetReviewByIdAsync(int user_fk, int song_fk);
        Task<bool> CreateReviewAsync(ReviewModel review);
        Task<bool> UpdateReviewAsync(int user_fk, int song_fk, ReviewModel review);
        Task<bool> DeleteReviewAsync(int user_fk, int song_fk);
    }
}
