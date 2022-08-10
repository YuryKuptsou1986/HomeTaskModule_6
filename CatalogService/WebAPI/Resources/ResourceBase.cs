using Extension.Url;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;
using ViewModel.Page;
using ViewModel.Query;

namespace WebAPI.Resources
{
    [DataContract(Name = "Resource", Namespace = "")]
    public class ResourceBase
    {
        private readonly IUrlHelper _urlHelper;
        private readonly List<ResourceLink> _links = new List<ResourceLink>();

        protected ResourceBase(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
        }

        [DataMember(Order = 999)]
        public IEnumerable<ResourceLink> Links => _links;

        public ResourceBase AddDelete(string relation, string routeName, object values)
        {
            _links.Add(CreateLink(
                HttpMethod.Delete.Method,
                relation,
                routeName,
                values));

            return this;
        }

        public ResourceBase AddGet(string relation, string routeName, object values)
        {
            _links.Add(CreateLink(
                HttpMethod.Get.Method,
                relation,
                routeName,
                values));

            return this;
        }

        public ResourceBase AddPost(string relation, string routeName)
        {
            _links.Add(CreateLink(
                HttpMethod.Post.Method,
                relation,
                routeName,
                new { }));

            return this;
        }

        public ResourceBase AddPut(string relation, string routeName, object values)
        {
            _links.Add(CreateLink(
                HttpMethod.Put.Method,
                relation,
                routeName,
                values));

            return this;
        }

        private ResourceLink CreateLink(string method, string relation, string routeName, object values)
        {
            return new ResourceLink(
                href: _urlHelper.Link(routeName, values),
                rel: relation,
                method: method);
        }

        public ResourceBase AddCurrentPage<T>(string routeName, IPagedCollection<T> items, PagedQueryParams query)
        {
            if (items is null)
                throw new ArgumentNullException(nameof(items));

            var link = _urlHelper.LinkCurrentPage(routeName, items.PageSize, items.CurrentPageNumber, query);
            _links.Add(new ResourceLink(link?.ToString() ?? string.Empty, "current-page", HttpMethod.Get.ToString()));
            return this;
        }

        public ResourceBase AddNextPage<T>(string routeName, IPagedCollection<T> items, PagedQueryParams query)
        {
            if (items is null)
                throw new ArgumentNullException(nameof(items));

            var link = _urlHelper.LinkNextPage(routeName, items.PageSize, items.NextPageNumber, query);
            _links.Add(new ResourceLink(link?.ToString() ?? string.Empty, "next-page", HttpMethod.Get.ToString()));
            return this;
        }

        public ResourceBase AddPreviousPage<T>(string routeName, IPagedCollection<T> items, PagedQueryParams query)
        {
            if (items is null)
                throw new ArgumentNullException(nameof(items));

            var link = _urlHelper.LinkPreviousPage(routeName, items.PageSize, items.PreviousPageNumber, query);
            _links.Add(new ResourceLink(link?.ToString() ?? string.Empty, "previous-page", HttpMethod.Get.ToString()));
            return this;
        }

        public ResourceBase AddFirstPage<T>(string routeName, IPagedCollection<T> items, PagedQueryParams query)
        {
            if (items is null)
                throw new ArgumentNullException(nameof(items));

            var link = _urlHelper.LinkFirstPage(routeName, items.PageSize, query);
            _links.Add(new ResourceLink(link?.ToString() ?? string.Empty, "first-page", HttpMethod.Get.ToString()));
            return this;
        }

        public ResourceBase AddLastPage<T>(string routeName, IPagedCollection<T> items, PagedQueryParams query)
        {
            if (items is null)
                throw new ArgumentNullException(nameof(items));

            var link = _urlHelper.LinkLastPage(routeName, items.PageSize, items.LastPageNumber, query);
            _links.Add(new ResourceLink(link?.ToString() ?? string.Empty, "last-page", HttpMethod.Get.ToString()));
            return this;
        }
    }
}
