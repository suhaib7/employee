using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Models;
using System.Diagnostics;

namespace mongoEmployee.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMemoryCache _cache;
        private readonly IProductRepository _db;
        private readonly string cacheKey = "productCacheKey";

        public ProductController(ILogger<ProductController> logger, IMemoryCache cache, IProductRepository db)
        {
            _logger = logger;
            _cache = cache;
            _db = db;
        }

        public IActionResult Index()
        {
            var stopwatchCached = new Stopwatch();
            stopwatchCached.Start();

            IEnumerable<Product> cachedProducts;

            if (_cache.TryGetValue(cacheKey, out cachedProducts))
            {
                _logger.Log(LogLevel.Information, "Products found in cache");
            }
            else
            {
                _logger.Log(LogLevel.Information, "Products not found in cache, loading from database");

                cachedProducts = _db.GetAllProducts().ToList();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetPriority(CacheItemPriority.Normal);

                _cache.Set(cacheKey, cachedProducts, cacheEntryOptions);
            }

            stopwatchCached.Stop();
            _logger.Log(LogLevel.Information, $"Cached data retrieval time: {stopwatchCached.ElapsedMilliseconds} ms");

            return View(cachedProducts);
        }

        public IActionResult ClearCache()
        {
            _cache.Remove(cacheKey);
            _logger.Log(LogLevel.Information, "Cache cleared");

            return RedirectToAction("Index");
        }
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll() {
            List<Product> products = _db.GetAllProducts().ToList();

            return Json(new { data = products });
        }

        #endregion

    }
}
