using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Library.Data;
using Microsoft.AspNetCore.WebUtilities;

namespace Library.Desktop.Model
{
    public class LibraryApiService
    {
        private readonly HttpClient _client;

        public LibraryApiService(string baseAddress)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        #region Authentication

        public async Task<bool> LoginAsync(string username, string password)
        {
            LoginDTO user = new LoginDTO()
            {
                UserName = username,
                Password = password
            };

            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Account/Login", user);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return false;
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task LogoutAsync()
        {
            HttpResponseMessage response = await _client.PostAsync("api/Account/Logout", null);

            if (response.IsSuccessStatusCode)
            {
                return;
            }

            throw new NetworkException("Service returned response: " + response);
        }

        #endregion

        #region Books

        public async Task<IEnumerable<BookDTO>> LoadBooksAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("api/Books/");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<BookDTO>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task CreateBookAsync(BookDTO book)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Books/", book);
            if(response.IsSuccessStatusCode)
                book.Id = (await response.Content.ReadAsAsync<BookDTO>()).Id;

            if (response.StatusCode is HttpStatusCode.Conflict)
                throw new NetworkException("A könyv adatai már szerepelnek az adatbázisban!");

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response " + response);
            }
        }

        #endregion

        #region Volumes

        public async Task<IEnumerable<VolumeDTO>> LoadVolumesAsync(int bookId)
        {
            HttpResponseMessage response = await _client.GetAsync(
                QueryHelpers.AddQueryString("api/Volumes", "bookId", bookId.ToString()));

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<VolumeDTO>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task CreateVolumeAsync(VolumeDTO volume)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Volumes/", volume);
            
            if(response.IsSuccessStatusCode)
                volume.Id = (await response.Content.ReadAsAsync<VolumeDTO>()).Id;

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task DeleteVolumeAsync(int volumeId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/Volumes/{volumeId}");

            if (response.StatusCode is HttpStatusCode.MethodNotAllowed)
            {
                throw new NetworkException("A kötethez valőszínűleg még létezik aktív kölcsönzés!\n" +
                                           "A törléséhez állítson minden kölcsönzést inakítvvá!");
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        #endregion

        #region Rents

        public async Task<IEnumerable<RentDTO>> LoadRentsAsync(int volumeId)
        {
            HttpResponseMessage response = await _client.GetAsync(
                QueryHelpers.AddQueryString("api/Rents/", "volumeId", volumeId.ToString()));

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<RentDTO>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task UpdateRentAsync(RentDTO rent)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/Rents/{rent.Id}", rent);

            if (response.StatusCode is HttpStatusCode.MethodNotAllowed)
                throw new NetworkException("Egy kötethez egyszerre csak 1 aktív kölcsönzés tartohat!");

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        #endregion
    }
}
