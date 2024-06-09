public class ProfileInfo
{
    public string DisplayName { get; set; }
    public string ThumbnailUrl { get; set; }

    public ProfileInfo(string displayName, string thumbnailUrl)
    {
        DisplayName = displayName;
        ThumbnailUrl = thumbnailUrl;
    }
}
