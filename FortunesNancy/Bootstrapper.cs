namespace FortunesNancy
{
    using Nancy;
    using Nancy.TinyIoc;
    using Nancy.ViewEngines.Razor;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private RazorViewEngine ensureRazorIsLoaded;

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            var mongoDataStore = new MongoDataStore("mongodb://localhost:27017/fortunes");
            container.Register<IDataStore>(mongoDataStore);
        }
    }
}