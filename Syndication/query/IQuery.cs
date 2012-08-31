using System;
namespace versomas.net.services.syndication.query
{
    public interface IQuery
    {
        string GetSingleNodeValue(string query);
    }
}
