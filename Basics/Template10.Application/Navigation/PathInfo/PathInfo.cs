﻿using Prism.Navigation;
using System;
using System.Linq;
using Windows.Foundation;

namespace Prism.Windows.Navigation
{
    public class PathInfo : IPathInfo
    {
        private string _originalString;

        public string QueryString { get; }

        public PathInfo(string originalString, INavigationParameters parameters)
        {
            _originalString = originalString;

            // parse name/key

            PageKey = originalString.Split('?').First();

            var queryString = originalString.Split('?').Last();
            if (queryString != PageKey)
            {
                QueryString = queryString;
            }

            // parse parameters

            var query = new WwwFormUrlDecoder(originalString.Split('?').Last());
            foreach (var item in query)
            {
                Parameters.Add(item.Name, item.Value);
            }

            // merge parameters

            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    Parameters.Add(item.Key, item.Value);
                }
            }
        }

        public int Index { get; set; }

        public NavigationParameters Parameters { get; } = new NavigationParameters();

        public string PageKey { get; set; }

        public Type PageType { get; internal set; }

        public Type ViewModelType { get; internal set; }

        public override string ToString()
        {
            return _originalString;
        }
    }
}
