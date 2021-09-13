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
    public class ArtistService : IArtistService
    {
        IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<List<ArtistModel>> GetArtistAsync(Sorter sorter, Pager pager, ArtistFilter artistFilter)
        {
            return await _artistRepository.GetArtistAsync(sorter, pager, artistFilter);

        }

        public async Task<ArtistModel> GetArtistByIdAsync(int id)
        {
            return await _artistRepository.GetArtistByIdAsync(id);
        }

        public async Task<bool> CreateArtistAsync(ArtistModel artist)
        {
            return await _artistRepository.CreateArtistAsync(artist);
        }

        public async Task<bool> UpdateArtistAsync(int id, ArtistModel artist)
        {
            return await _artistRepository.UpdateArtistAsync(id, artist);
        }

        public async Task<bool> DeleteArtistAsync(int id)
        {
            return await _artistRepository.DeleteArtistAsync(id);
        }
    }
}
