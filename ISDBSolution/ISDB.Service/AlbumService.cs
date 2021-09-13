using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISDB.Model;
using ISDB.Repository;
using ISDBRepository.Common;
using ISDBService.Common;
using ISDB.Common;

namespace ISDB.Service
{
    public class AlbumService :IAlbumService
    {

        IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public async Task<List<Album>> GetAlbumsAsync(Sorter sorter, Pager pager, AlbumFilter albumFilter)
        {
            return await _albumRepository.GetAlbumsAsync(sorter, pager, albumFilter);

        }

        public async Task<Album> GetAlbumByIdAsync(int id)
        {
            return await _albumRepository.GetAlbumByIdAsync(id);
        }

        public async Task<bool> CreateAlbumAsync(Album album)
        {
            return await _albumRepository.CreateAlbumAsync(album);
        }

        public async Task<bool> UpdateAlbumAsync(int id, Album album)
        {
            return await _albumRepository.UpdateAlbumAsync(id, album);
        }

        public async Task<bool> DeleteAlbumAsync(int id)
        {
            return await _albumRepository.DeleteAlbumAsync(id);
        }
    }
}
