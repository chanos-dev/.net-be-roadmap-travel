using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FiltersAndAttributes.Filters
{
    public class CacheResourceFilter : Attribute, IResourceFilter
    {
        private static readonly Dictionary<string, object> _cache
                = new Dictionary<string, object>();
        private string _cacheKey;

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("CacheResourceFilter OnResourceExecuting");
            _cacheKey = context.HttpContext.Request.Path.ToString();
            if (_cache.ContainsKey(_cacheKey))
            {
                var cachedValue = _cache[_cacheKey] as string;
                if (cachedValue != null)
                {
                    context.Result = new ContentResult()
                    { Content = $"caching! {cachedValue}" };
                }
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("CacheResourceFilter OnResourceExecuted");
            if (!string.IsNullOrEmpty(_cacheKey) && !_cache.ContainsKey(_cacheKey))
            {
                var result = context.Result as OkObjectResult;
                if (result != null)
                {
                    _cache.Add(_cacheKey, result.Value!);
                }
            }
        }
    }
}
