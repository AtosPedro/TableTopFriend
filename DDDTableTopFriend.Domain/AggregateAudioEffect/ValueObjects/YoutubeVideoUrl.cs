using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;

namespace DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;

public sealed class YoutubeVideoUrl : ValueObject
{
    private const string YoutubeBaseUrl = "https://www.youtube.com";
    public string Value { get; set; }

    private YoutubeVideoUrl(string value) => Value = value;

    public static ErrorOr<YoutubeVideoUrl> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Errors.YoutubeUrl.InvalidUrl;

        if (!value.StartsWith(YoutubeBaseUrl))
            return Errors.YoutubeUrl.InvalidUrl;

        return new YoutubeVideoUrl(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return YoutubeBaseUrl;
    }
}