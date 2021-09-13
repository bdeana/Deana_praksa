using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISDB.Model;
using ISDBModel.Common;
using ISDB.Common;

namespace ISDBRepository.Common
{
    public interface IAlbumRepository
    {
        Task<List<Album>> GetAlbumsAsync(Sorter sorter, Pager pager, AlbumFilter albumFilter);
        Task<Album> GetAlbumByIdAsync(int id);
        Task<bool> CreateAlbumAsync(Album album);
        Task<bool> UpdateAlbumAsync(int id, Album album);
        Task<bool> DeleteAlbumAsync(int id);
    }
}
