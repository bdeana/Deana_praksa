using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISDB.Model;
using ISDB.Common;

namespace ISDBService.Common
{
    public interface ISong_ArtistService
    {
        Task<List<Song_ArtistModel>> GetSong_ArtistAsync(Sorter sorter, Pager pager, Song_ArtistFilter song_artistFilter);
        Task<Song_ArtistModel> GetSong_ArtistByIdAsync(int song_fk, int artist_fk );
        Task<bool> CreateSong_ArtistAsync(Song_ArtistModel song_artist);
        Task<bool> UpdateSong_ArtistAsync(int song_fk, int artist_fk, Song_ArtistModel song_artist);
        Task<bool> DeleteSong_ArtistAsync(int song_fk, int artist_fk);
    }
}
