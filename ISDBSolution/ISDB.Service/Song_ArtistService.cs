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
    public class Song_ArtistService : ISong_ArtistService
    {
        ISong_ArtistRepository _song_artistRepository;

        public Song_ArtistService(ISong_ArtistRepository song_artistRepository)
        {
            _song_artistRepository = song_artistRepository;
        }

        public async Task<List<Song_ArtistModel>> GetSong_ArtistAsync(Sorter sorter, Pager pager, Song_ArtistFilter song_artistFilter)
        {
            return await _song_artistRepository.GetSong_ArtistAsync(sorter, pager, song_artistFilter);

        }

        public async Task<Song_ArtistModel> GetSong_ArtistByIdAsync( int song_fk, int artist_fk)
        {
            return await _song_artistRepository.GetSong_ArtistByIdAsync(song_fk, artist_fk);
        }

        public async Task<bool> CreateSong_ArtistAsync(Song_ArtistModel song_artist)
        {
            return await _song_artistRepository.CreateSong_ArtistAsync(song_artist);
        }

        public async Task<bool> UpdateSong_ArtistAsync(int song_fk, int artist_fk, Song_ArtistModel song_artist)
        {
            return await _song_artistRepository.UpdateSong_ArtistAsync(song_fk, artist_fk, song_artist);
        }

        public async Task<bool> DeleteSong_ArtistAsync(int song_fk, int artist_fk)
        {
            return await _song_artistRepository.DeleteSong_ArtistAsync(song_fk, artist_fk);
        }
    }
}
