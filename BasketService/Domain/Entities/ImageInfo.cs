using BasketService.Domain.Common;
using System;
using System.Collections.Generic;

namespace BasketService.Domain.Entities
{
    public class ImageInfo : ValueObject
    {
        public Uri Url { get; }
        public string AltText { get; }

        public ImageInfo(Uri url, string altText)
        {
            Url = url;
            AltText = altText;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Url;
            yield return AltText;
        }
    }
}