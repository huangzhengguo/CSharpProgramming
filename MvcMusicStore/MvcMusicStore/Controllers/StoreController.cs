using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            var albums = GetAlbums();
            return View(albums);
        }

        [Authorize]
        public ActionResult Buy(int id)
        {
            var album = GetAlbums().Single(a => a.AlbumId == id);
            return View(album);
        }

        private static List<Album> GetAlbums()
        {
            var albums = new List<Album>
            {
                new Album{AlbumId = 1, Title = "The Fall of Math", Price = 8.99M},
                new Album{AlbumId = 2, Title = "The Blue Notebooks", Price = 8.99M},
                new Album{AlbumId = 3, Title = "Lost in Transiation", Price = 9.99M},
                new Album{AlbumId = 4, Title ="Permutation", Price = 10.99M}
            };

            return albums;
        }

        // Get: /Store/Browse?genre=?Disco
        public string Browse(string genre)
        {
            // HtmlEncode预处理用户输入，这样就能阻止用户用链接向视图中注入JavaScript代码或者HTML标记
            string message = HttpUtility.HtmlEncode("Store.Browse, Genre = " + genre);
            return message;
        }

        // Get: /Store/Details/5
        public string Details(int id)
        {
            string message = "Store.Details, ID = " + id;
            return message;
        }
    }
}