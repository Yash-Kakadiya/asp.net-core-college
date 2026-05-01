namespace MiddlewareDemo
{
    public class MyCustomMiddleware
    {
        private readonly RequestDelegate _next;
        public MyCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Before invoking next middleware");
            Console.WriteLine("Request Path: " + context.Request.Path);
            await _next(context);
            Console.WriteLine("Response Status Code: " + context.Response.StatusCode);
            Console.WriteLine("After invoking next middleware");
        }
    }
}
