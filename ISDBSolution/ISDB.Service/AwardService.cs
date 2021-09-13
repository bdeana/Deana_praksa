using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISDB.Model;
using ISDB.Repository;
using ISDBRepository.Common;
using ISDBService.Common;
using ISDB.Common;
using System.Threading.Tasks;


namespace ISDB.Service
{
    public class AwardService : IAwardService
    {
        IAwardRepository _awardRepository;

        public AwardService(IAwardRepository awardRepository)
        {
            _awardRepository = awardRepository;
        }

        public async Task<List<Award>> GetAwardsAsync(Sorter sorter, Pager pager, AwardFilter awardFilter)
        {
            return await _awardRepository.GetAwardsAsync(sorter, pager, awardFilter);

        }

        public async Task<Award> GetAwardByIdAsync(int id)
        {
            return await _awardRepository.GetAwardByIdAsync(id);
        }

        public async Task<bool> CreateAwardAsync(Award award)
        {
            return await _awardRepository.CreateAwardAsync(award);
        }

        public async Task<bool> UpdateAwardAsync(int id, Award award)
        {
            return await _awardRepository.UpdateAwardAsync(id, award);
        }

        public async Task<bool> DeleteAwardAsync(int id)
        {
            return await _awardRepository.DeleteAwardAsync(id);
        }
    }
}
