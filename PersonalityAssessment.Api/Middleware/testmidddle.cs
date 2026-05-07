namespace PersonalityAssessment.Api.Middleware
{
    public class testmidddle
    {

        private readonly RequestDelegate _next;

        public testmidddle(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Do something before the next middleware
            Console.WriteLine("Before next middleware");
            await _next(context);
            // Do something after the next middleware
            Console.WriteLine("After next middleware");
        }



    }
}
