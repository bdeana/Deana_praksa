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

    public class SongService : ISongService
    {
        ISongRepository _songRepository;

        public SongService(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public async Task<List<SongModel>> GetSongAsync(Sorter sorter, Pager pager, SongFilter songFilter)
        {
            return await _songRepository.GetSongAsync(sorter, pager, songFilter);

        }

        public async Task<SongModel> GetSongByIdAsync(int id)
        {
            return await _songRepository.GetSongByIdAsync(id);
        }

        public async Task<bool> CreateSongAsync(SongModel song)
        {
            return await _songRepository.CreateSongAsync(song);
        }

        public async Task<bool> UpdateSongAsync(int id, SongModel song)
        {
            return await _songRepository.UpdateSongAsync(id, song);
        }

        public async Task<bool> DeleteSongAsync(int id)
        {
            return await _songRepository.DeleteSongAsync(id);
        }
    }
}
