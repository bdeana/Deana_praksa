using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISDB.Model;
using ISDB.Common;

namespace ISDBRepository.Common
{
    public interface IArtistRepository
    {
        Task<List<ArtistModel>> GetArtistAsync(Sorter sorter, Pager pager, ArtistFilter artistFilter);
        Task<ArtistModel> GetArtistByIdAsync(int id);
        Task<bool> CreateArtistAsync(ArtistModel artist);
        Task<bool> UpdateArtistAsync(int id, ArtistModel artist);
        Task<bool> DeleteArtistAsync(int id);
    }
}
