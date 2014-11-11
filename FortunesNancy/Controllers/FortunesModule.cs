namespace FortunesNancy
{
    using System.Collections.Generic;
    using Nancy;
    using Nancy.ModelBinding;

    public class FortunesModule : NancyModule
    {
        public FortunesModule(IDataStore fortuneStore): base("fortunes")
        {
            Get["/"] = _ =>
            {
                return Negotiate
                    .WithModel(fortuneStore.GetAll())
                    .WithView("list");
            };
                
            Post["/"] = _ =>
            {
                var newFortune = this.Bind<Fortune>();
                if (newFortune.id == 0)
                    newFortune.id = fortuneStore.Count + 1;

                if (!fortuneStore.TryAdd(newFortune))
                    return HttpStatusCode.NotAcceptable;

                return Negotiate.WithModel(newFortune)
                               .WithStatusCode(HttpStatusCode.Created)
                               .WithView("created");
            };

            Put["/{id}"] = p =>
            {
                var updatedFortune = this.Bind<Fortune>();

                if (!fortuneStore.TryUpdate(updatedFortune))
                    return HttpStatusCode.NotFound;

                return Response.AsJson(updatedFortune);
            };

            Delete["/{id}"] = p =>
            {
                if (!fortuneStore.TryRemove(p.id))
                    return HttpStatusCode.NotFound;

                return HttpStatusCode.OK;
            };
        }
    }
}