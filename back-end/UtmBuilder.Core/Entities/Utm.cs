using UtmBuilder.Core.Extensions;
using UtmBuilder.Core.ValueObjects;
using UtmBuilder.Core.ValueObjects.Exceptions;

namespace UtmBuilder.Core;

public class Utm
{
    public Utm(Url url, Campaign campaign)
    {
        Url = url;
        Campaign = campaign;
    }
    
    /// <summary>
    /// URL (Website Link)
    /// </summary>
    public Url Url { get; }
    
    /// <summary>
    /// Campaign Details
    /// </summary>
    public Campaign Campaign { get; }

    public static implicit operator string(Utm utm) 
        => utm.ToString();

    public static implicit operator Utm(string link)
    {
        if (string.IsNullOrEmpty(link))
            throw new InvalidUrlException();

        var url = new Url(link);
        var segments = url.Address.Split("?");
        if (segments.Length == 1)
            throw new InvalidUrlException("No segments were provided");

        var pars = segments[1].Split("&");
        var source = GetSource(pars, "utm_source");
        var medium = GetSource(pars, "utm_medium");
        var name = GetSource(pars, "utm_campaign");
        var id = GetSource(pars, "utm_id");
        var term = GetSource(pars, "utm_term");
        var content = GetSource(pars, "utm_content");

        var utm = new Utm(
            new Url(segments[0]),
            new Campaign(source, medium, name, id, term, content));
        return utm;
    }

    private static string GetSource(string[] pars, string parameter)
    {
        return pars.Where(x => x.StartsWith(parameter)).FirstOrDefault("").Split("=")[1];
    }

    public override string ToString()
    {
        var segments = new List<string>();
        segments.AddIfNotNull("utm_source", Campaign.Source);
        segments.AddIfNotNull("utm_medium", Campaign.Medium);
        segments.AddIfNotNull("utm_campaign", Campaign.Name);
        segments.AddIfNotNull("utm_id", Campaign.Id);
        segments.AddIfNotNull("utm_term", Campaign.Term);
        segments.AddIfNotNull("utm_content", Campaign.Content);
        return $"{Url.Address}?{string.Join("&", segments)}";
    }
}