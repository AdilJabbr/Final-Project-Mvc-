namespace Final_Project.Helpers.Extensions
{
    public static class ExtensionMethods
    {
        public static string GetReturnUrl(this HttpRequest request)
        {
            string? retunUrl = request.Headers["Referer"];

            if (retunUrl is null)
                retunUrl = "/";

            return retunUrl;
        }
    }
}
