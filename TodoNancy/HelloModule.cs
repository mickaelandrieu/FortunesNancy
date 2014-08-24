namespace TodoNancy
{
    using Nancy;

    public class HelloModule : NancyModule
    {
        public HelloModule()
        {
            Get["/"] = _ => HttpStatusCode.OK;
        }
    }
}