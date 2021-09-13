using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISDB.Model;
using ISDB.Common;

namespace ISDBService.Common
{
    public interface IAwardService
    {
        Task<List<Award>> GetAwardsAsync(Sorter sorter, Pager pager, AwardFilter awardFilter);
        Task<Award> GetAwardByIdAsync(int id);
        Task<bool> CreateAwardAsync(Award award);
        Task<bool> UpdateAwardAsync(int id, Award award);
        Task<bool> DeleteAwardAsync(int id);
    }
}
