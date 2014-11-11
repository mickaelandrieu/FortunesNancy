namespace FortunesNancy
{
    using Nancy;

    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => HttpStatusCode.OK;
        }
    }
}