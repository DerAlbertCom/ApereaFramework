using System;
using Aperea.EntityModels;
using Aperea.MVC.Controllers;
using Aperea.MVC.RemoteActions;
using Aperea.UrlBuilder;

namespace Aperea.MVC.UrlBuilders
{
    public class RemoteActionUrlBuilder : IRemoteActionUrlBuilder
    {
        readonly IActionUrlBuilder _urlBuider;
        readonly IRemoteActionHashing _hashing;

        public RemoteActionUrlBuilder(IRemoteActionHashing hashing, IActionUrlBuilder urlBuider)
        {
            _hashing = hashing;
            _urlBuider = urlBuider;
        }

        public string GetUrl(RemoteAction webAction)
        {
            return _urlBuider.BuildActionUrl<RemoteActionController>(
                c => c.Validate(_hashing.GetHash(webAction), webAction.ConfirmationKey));
        }
    }
}