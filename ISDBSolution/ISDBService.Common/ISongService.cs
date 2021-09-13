using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISDB.Model;
using ISDB.Common;

namespace ISDBService.Common
{
    public interface ISongService
    {
        Task<List<SongModel>> GetSongAsync(Sorter sorter, Pager pager, SongFilter songFilter);
        Task<SongModel> GetSongByIdAsync(int id);
        Task<bool> CreateSongAsync(SongModel song);
        Task<bool> UpdateSongAsync(int id, SongModel song);
        Task<bool> DeleteSongAsync(int id);
    }
}
