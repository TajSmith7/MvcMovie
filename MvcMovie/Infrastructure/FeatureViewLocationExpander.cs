using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;


namespace MvcMovie.Infrastructure
{
    public class FeatureViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context) {}

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            yield return "/Features/{1}/Views/{0}.cshtml";
            yield return "/Features/Shared/{0}.cshtml";
            foreach (var location in viewLocations)
            {
                yield return location;
            }
        }
    }
}
