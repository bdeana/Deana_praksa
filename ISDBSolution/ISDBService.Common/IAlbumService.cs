using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISDB.Model;
using ISDB.Common;


namespace ISDBService.Common
{
    public interface IAlbumService
    {
        Task<List<Album>> GetAlbumsAsync(Sorter sorter, Pager pager, AlbumFilter albumFilter);
        Task<Album> GetAlbumByIdAsync(int id);
        Task<bool> CreateAlbumAsync(Album album);
        Task<bool> UpdateAlbumAsync(int id, Album album);
        Task<bool> DeleteAlbumAsync(int id);
    }
}
