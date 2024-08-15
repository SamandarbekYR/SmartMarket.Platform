namespace SmartMarket.WebApi.Middlewares
{
    public class CrosOriginAccessMiddleware
    {
        private readonly RequestDelegate _next;

        public CrosOriginAccessMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context, IWebHostEnvironment env)
        {
            context.Response.Headers.Append("Access-Control-Expose-Headers", "X-Pagination");
            await _next(context);
        }
    }
}
